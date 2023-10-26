using Interpreter.Eval.Type;
using UnityEngine;

namespace Interpreter.Lib.Tower
{
    public class CreateTower : MonoBehaviour
    {
        public GameObject obj;

        public Nil Exec(Number x, Number y)
        {
            //@TODO: check for resources (waiting @ClaraOMello)

            float xPosition = (float) x.AsNumber();
            float yPosition = (float)y.AsNumber();

            GameObject tower = Instantiate(obj);
            tower.transform.position = new Vector3(xPosition, yPosition);

            return new Nil();
        }
    }
}