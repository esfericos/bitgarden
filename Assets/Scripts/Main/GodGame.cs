using Interpreter.Eval;
using Lib = Interpreter.Lib;
using UnityEngine;

namespace Main
{
    public class GodGame : MonoBehaviour
    {
        public Evaluator Evaluator;
        
        public void Start()
        {
            Debug.Log("Initializing GodGame");
            
            Evaluator = SetupEvaluator();
        }

        private Evaluator SetupEvaluator()
        {
            return new Evaluator()
                .AddCmd(new Lib.Math.Add())
                .AddCmd(new Lib.Math.Sub())
                .AddCmd(new Lib.Math.Mul())
                .AddCmd(new Lib.Math.Div());
        }
    }
}