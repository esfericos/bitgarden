using System;
using Interpreter.Eval.Type;
using UnityEngine;

namespace Interpreter.Lib.Wall
{
    public class CreateWall : MonoBehaviour
    {
        
        public Nil Exec(Number x, Number y)
        {
            //@TODO: check for resources

            int typeNum = 0;
            string type = "wall_tower";
            string path = "Prefabs/Buildings/Walls/" + type;
            Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/wall_basic");
            
            ushort xf = (ushort)x.AsNumber();
            ushort yf = (ushort)y.AsNumber();
            
            Collider2D[] plot = Physics2D.OverlapAreaAll(new Vector2(xf + 0.05f, yf + 0.03f), new Vector2(xf + 0.05f, yf + 0.03f));

            if (plot.Length > 0)
            {
                Console.print("nao pode");
            }
            else
            {
                Collider2D[] surroundings = Physics2D.OverlapAreaAll(new Vector2(xf - 0.95f, yf - 0.9f), new Vector2(xf + 1.05f, yf + 1.1f));
                
                foreach (var obj in surroundings)
                {
                    int objType = obj.GetComponent<global::Wall>().Type;
                    int otherType = 0;

                    // get rid of corners
                    if ((obj.transform.position.x > (float)(xf + 0.05) &&
                         obj.transform.position.y > (float)(yf + 0.1)) ||
                        (obj.transform.position.x > (float)(xf + 0.05) &&
                         obj.transform.position.y < (float)(yf + 0.1)) ||
                        (obj.transform.position.x < (float)(xf + 0.05) &&
                         obj.transform.position.y > (float)(yf + 0.1)) ||
                        (obj.transform.position.x < (float)(xf + 0.05) && obj.transform.position.y < (float)(yf + 0.1)))
                    {
                        continue; }
                    
                    if (obj.transform.position.x > (float)(xf + 0.05)) { typeNum += 2; otherType = 8; }
                    if (obj.transform.position.x < (float)(xf + 0.05)) { typeNum += 8; otherType = 2; }
                    if (obj.transform.position.y > (float)(yf + 0.1)) { typeNum += 1; otherType = 4; }
                    if (obj.transform.position.y < (float)(yf + 0.1)) { typeNum += 4; otherType = 1; }
                    
                    objType += otherType;
                    obj.GetComponent<global::Wall>().Type = objType;
                    
                    if (objType == 1) { obj.GetComponent<SpriteRenderer>().sprite = sprites[7]; }
                    if (objType == 2 || objType == 3) { obj.GetComponent<SpriteRenderer>().sprite = sprites[0]; }
                    if (objType == 4) { obj.GetComponent<SpriteRenderer>().sprite = sprites[10]; }
                    if (objType == 5) { obj.GetComponent<SpriteRenderer>().sprite = sprites[9]; }
                    if (objType == 6 || objType == 7) { obj.GetComponent<SpriteRenderer>().sprite = sprites[2]; }
                    if (objType == 8 || objType == 9) { obj.GetComponent<SpriteRenderer>().sprite = sprites[1]; }
                    if (objType == 10) { obj.GetComponent<SpriteRenderer>().sprite = sprites[4]; }
                    if (objType == 11) { obj.GetComponent<SpriteRenderer>().sprite = sprites[5]; }
                    if (objType == 12 || objType == 13) { obj.GetComponent<SpriteRenderer>().sprite = sprites[3]; }
                    if (objType == 14 || objType == 15) { obj.GetComponent<SpriteRenderer>().sprite = sprites[6]; }
                }
                
                GameObject wall = Instantiate(Resources.Load(path), new Vector3(xf + 0.05f, yf + 0.1f, 1), Quaternion.identity) as GameObject;
                
                if (typeNum == 0 || typeNum == 1) { wall.GetComponent<SpriteRenderer>().sprite = sprites[7]; }
                if (typeNum == 2 || typeNum == 3) { wall.GetComponent<SpriteRenderer>().sprite = sprites[0]; }
                if (typeNum == 4) { wall.GetComponent<SpriteRenderer>().sprite = sprites[10]; }
                if (typeNum == 5) { wall.GetComponent<SpriteRenderer>().sprite = sprites[9]; }
                if (typeNum == 6 || typeNum == 7) { wall.GetComponent<SpriteRenderer>().sprite = sprites[2]; }
                if (typeNum == 8 || typeNum == 9) { wall.GetComponent<SpriteRenderer>().sprite = sprites[1]; }
                if (typeNum == 10) { wall.GetComponent<SpriteRenderer>().sprite = sprites[4]; }
                if (typeNum == 11) { wall.GetComponent<SpriteRenderer>().sprite = sprites[5]; }
                if (typeNum == 12 || typeNum == 13) { wall.GetComponent<SpriteRenderer>().sprite = sprites[3]; }
                if (typeNum == 14 || typeNum == 15) { wall.GetComponent<SpriteRenderer>().sprite = sprites[6]; }
                
                wall.GetComponent<global::Wall>().Type = typeNum;
            }
            
            return new Nil();
        }
    }
}