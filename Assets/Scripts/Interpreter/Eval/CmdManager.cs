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

        private object CmdInstance;
        private MethodInfo CmdExec;
        // We must keep track of argument order to not rely on a specific dictionary impl.
        private readonly IReadOnlyList<string> ImplArgNames;

        private const string MustBeValue = "must derive from 'Value' (i.e., be a 'String', 'Number' or 'Nil')";

        public RegisteredCmd(object cmdInstance)
        {
            Name = "TODO";

            CmdInstance = cmdInstance;
            var cmd = cmdInstance.GetType();
            
            // Tries to get the `Exec` method
            CmdExec = cmd.GetMethod("Exec", BindingFlags.Public | BindingFlags.Instance);
            if (CmdExec == null)
                throw new Exception($"Missing 'Exec' method in command class '{cmd.Name}'");
            
            // Initialize ArgTypes
            var execParams = CmdExec.GetParameters();
            var argTypes = new Dictionary<string, Type>();
            var argNames = new List<string>();
            foreach (var param in execParams)
            {
                if (!Value.IsValueType(param.ParameterType))
                    throw new Exception($"Param '{param.Name}' of command class '{cmd.Name}' {MustBeValue}");
                argTypes.Add(param.Name, param.ParameterType);
                argNames.Add(param.Name);
            }
            ArgTypes = argTypes;
            ImplArgNames = argNames;
            
            // Return type
            if (!Value.IsValueType(CmdExec.ReturnType))
                throw new Exception($"Return type of 'Exec' from command class '{cmd.Name}' {MustBeValue}");
            ReturnType = CmdExec.ReturnType;
        }

        public Value Exec(IReadOnlyDictionary<string, Value> args)
        {
            // TODO: Type check arguments.
            var argValues = ImplArgNames
                .Select(argName => (object)args[argName])
                .ToArray();
            // This cast is infallible since the constructor ensures that `CmdExec`'s return type is a value type.
            return (Value)CmdExec.Invoke(CmdInstance, argValues);
        }
    }
}