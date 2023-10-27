using Interpreter.Eval.Type;
using UnityEngine;

namespace Interpreter.Lib.Tower
{
    public class CreateTower : MonoBehaviour
    {
        public GameObject obj;
        private GraphManager GraphHandler;

        public Nil Exec(Number x, Number y)
        {
            //@TODO: check for resources (waiting @ClaraOMello)

            GraphHandler = GameObject.FindGameObjectWithTag("GraphData").GetComponent<GraphManager>();
            ushort xPosition = (ushort) x.AsNumber();
            ushort yPosition = (ushort)y.AsNumber();

            Debug.Log("Created!");
            GraphHandler.AddTower2Gambiarra(xPosition, yPosition);

            return new Nil();

            GameObject tower = Instantiate(obj);
            tower.transform.position = new Vector3(xPosition, yPosition);

            return new Nil();
        }
    }
}