using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Interpreter.Eval
{
    public class CmdManager
    {
        /// <summary>
        /// Map from function names to an action that executes it.
        /// </summary>
        private IDictionary<string, RegisteredCmd> RegisteredCmds;
    }

    public class RegisteredCmd
    {
        public readonly string Name;
        public readonly IReadOnlyDictionary<string, Type> ArgTypes;
        public readonly Type ReturnType;

        private readonly object _cmdInstance;
        private readonly MethodInfo _cmdExec;
        // We must keep track of argument order to not rely on a specific dictionary impl.
        private readonly IReadOnlyList<string> _implArgNames;

        private const string MustBeValue = "must derive from 'Value' (i.e., be a 'String', 'Number' or 'Nil')";

        public RegisteredCmd(object cmdInstance)
        {
            _cmdInstance = cmdInstance;
            var cmd = cmdInstance.GetType();

            // Function name is the same as the class, but the first character is lowercased (camelCase).
            Name = char.ToLower(cmd.Name[0]) + cmd.Name[1..];
            
            // Tries to get the `Exec` method
            _cmdExec = cmd.GetMethod("Exec", BindingFlags.Public | BindingFlags.Instance);
            if (_cmdExec == null)
                throw new Exception($"Missing 'Exec' method in command class '{cmd.Name}'");
            
            // Initialize ArgTypes
            var execParams = _cmdExec.GetParameters();
            var argTypes = new Dictionary<string, Type>();
            var argNames = new List<string>();
            foreach (var param in execParams)
            {
                if (!Value.IsValueType(param.ParameterType))
                    throw new Exception($"Argument '{param.Name}' of command class '{cmd.Name}' {MustBeValue}");
                argTypes.Add(param.Name, param.ParameterType);
                argNames.Add(param.Name);
            }
            ArgTypes = argTypes;
            _implArgNames = argNames;
            
            // Return type
            if (!Value.IsValueType(_cmdExec.ReturnType))
                throw new Exception($"Return type of 'Exec' from command class '{cmd.Name}' {MustBeValue}");
            ReturnType = _cmdExec.ReturnType;
        }

        public Value Exec(IReadOnlyDictionary<string, Value> args)
        {
            var argValues = _implArgNames
                .Select(argName =>
                {
                    if (!args.TryGetValue(argName, out var value))
                        throw new EvalException($"Missing argument '{argName}' in call to '{Name}'");
                    var actualTy = value.GetType();
                    var expectedTy = ArgTypes[argName];
                    // Checks whether type `actualTy` is assignable to `expectedTy`
                    if (!expectedTy.IsAssignableFrom(actualTy))
                        throw new EvalException($"Type mismatch in argument '{argName}' in call to '{Name}': "
                            + $"expected type '{expectedTy.Name}', but got type '{actualTy.Name}'");
                    return (object)value;
                })
                .ToArray();
            // Ensure the user didn't pass any unnecessary arguments.
            // (one's using foreach so that it doesn't execute if there are no extra arguments)
            foreach (var invalidArg in args.Keys.Except(_implArgNames))
                throw new EvalException($"Argument '{invalidArg}' is not needed in call to '{Name}'");
            // This cast is infallible since the constructor ensures that `CmdExec`'s return type is a value type.
            return (Value)_cmdExec.Invoke(_cmdInstance, argValues);
        }
    }
}