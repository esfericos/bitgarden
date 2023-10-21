using UnityEngine;

namespace Interpreter.Cmd
{
    // [UserFunction("console_log")]
    public class Log
    {
        public void Exec(Eval.String msg)
        {
            Debug.Log($"user msg: {msg}");
        }
    }
}