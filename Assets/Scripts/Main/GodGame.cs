using Interpreter.Eval;
using Interpreter.Lib.Wall;
using Lib = Interpreter.Lib;
using UnityEngine;

namespace Main
{
    public class GodGame : MonoBehaviour
    {
        public Evaluator Evaluator;
        public Inventory Inventory;
        public Store Store;

        public void Start()
        {
            Debug.Log("Initializing GodGame");

            Evaluator = SetupEvaluator();
            Store = GameObject.FindGameObjectWithTag("StoreHandler").GetComponent<Store>();
            Inventory = GameObject.FindGameObjectWithTag("StoreHandler").GetComponent<Inventory>();
        }

        private Evaluator SetupEvaluator()
        {
            return new Evaluator()
                .AddCmd(new Lib.Math.Add())
                .AddCmd(new Lib.Math.Sub())
                .AddCmd(new Lib.Math.Mul())
                .AddCmd(new Lib.Math.Div())
                .AddCmd(new Lib.Wall.CreateWall())
                .AddCmd(new Lib.Wall.CreateManyWalls())
                .AddCmd(new Lib.Tower.CreateTower());
        }
    }
}