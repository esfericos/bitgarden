using Interpreter.Eval.Type;
using Interpreter.Eval;
using UnityEngine;

namespace Interpreter.Lib.Tower
{
    public class CreateTower : MonoBehaviour
    {
        private Store store = GameObject.FindGameObjectWithTag("StoreHandler").GetComponent<Store>();
        [SerializeField] private Price price = new Price(10);
        public GameObject obj;
        private GraphManager GraphHandler;

        public Nil Exec(Number x, Number y)
        {
            GraphHandler = GameObject.FindGameObjectWithTag("GraphData").GetComponent<GraphManager>();

            Graph grafo = GameObject.FindGameObjectWithTag("GraphHandle").GetComponent<Graph>();
            if (!grafo.IsAvailableToBuild(new Position((ushort)x.Value, (ushort)y.Value)))
            {
                throw new EvalException("Você não pode construir ai!");
            }

            //@TODO: check for resources (waiting @ClaraOMello)

            ushort xf = (ushort)x.AsNumber();
            ushort yf = (ushort)y.AsNumber();

            if (!store.Buy(price))
            {
                //TODO terminal warning
                throw new EvalException("Você não tem dinheiro para construir!");
                Console.print("ta pobre hein");
            }
            else
            {
                GraphHandler.AddTowerGambiarra2(xf, yf);
            }
            
            return new Nil();
        }
    }
}