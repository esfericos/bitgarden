using Interpreter.Eval.Type;
using UnityEngine;

namespace Interpreter.Lib.Wall
{
    public class CreateWall : MonoBehaviour
    {
        public GameObject obj;
        public Nil Exec(Number x1, Number y1, Number x2 = null, Number y2 = null)
        {
            //@TODO: check for resources
            
            float x1f = (float) x1.AsNumber();
            float y1f = (float) y1.AsNumber();
            
            if (x2 == null || y2 == null)
            {
                GameObject wall = Instantiate(obj);
                wall.transform.position = new Vector3(x1f, y1f);
            }
            else
            {
                float x2f = (float) x2.AsNumber();
                float y2f = (float) y2.AsNumber();

                if (y1f == y2f)
                {
                    if (x1f < x2f)
                    {
                        while (x1f != x2f)
                        {
                            GameObject wall = Instantiate(obj);
                            wall.transform.position = new Vector3(x1f, y1f);
                            x1f++;
                        }
                    }
                    else
                    {
                        while (x2f != x1f)
                        {
                            GameObject wall = Instantiate(obj);
                            wall.transform.position = new Vector3(x2f, y1f);
                            x2f++;
                        }
                    }
                } else if (x1f == x2f)
                {
                    if (y1f < y2f)
                    {
                        while (y1f != y2f)
                        {
                            GameObject wall = Instantiate(obj);
                            wall.transform.position = new Vector3(x1f, y1f);
                            y1f++;
                        }
                    }
                    else
                    {
                        while (y2f != y1f)
                        {
                            GameObject wall = Instantiate(obj);
                            wall.transform.position = new Vector3(x1f, y2f);
                            y2f++;
                        }
                    }
                }
                
            }
            return new Nil();
        }
    }
}