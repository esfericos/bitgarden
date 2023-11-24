using System;
using System.Collections.Generic;
using Interpreter.Eval;
using Interpreter.Eval.Type;
using Main;
using TMPro;
using UnityEngine;

namespace Interpreter.Lib.Wall
{
    public class CreateWall : MonoBehaviour
    {
        private Store store = GameObject.FindGameObjectWithTag("StoreHandler").GetComponent<Store>();
        [SerializeField] private Price price = new Price(5);
        public Nil Exec(Number x, Number y)
        {
            Graph grafo = GameObject.FindGameObjectWithTag("GraphHandle").GetComponent<Graph>();
            if (!grafo.IsAvailableToBuild(new Position((ushort)x.Value, (ushort)y.Value)))
            {
                throw new EvalException("Você não pode construir ai!");
            }
            
            if (!store.Buy(price))
            {
                //TODO terminal warning
                throw new EvalException("Você não tem dinheiro para construir!");
                Console.print("ta pobre hein");
            }
            else
            {
                int type = 0;
                Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/wall_basic");
                
                ushort xf = (ushort)x.AsNumber();
                ushort yf = (ushort)y.AsNumber();
                
                Collider2D[] plot = Physics2D.OverlapAreaAll(new Vector2(xf + 0.05f, yf + 0.03f), new Vector2(xf + 0.05f, yf + 0.03f));

                if (plot.Length > 0)
                {
                    // Console.print("nao pode");
                    // throw new EvalException("Já existe uma estrutura ai!");
                    //@TODO: terminal warning
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
                        
                        if (obj.transform.position.x > (float)(xf + 0.05)) { type += 2; otherType = 8; }
                        if (obj.transform.position.x < (float)(xf + 0.05)) { type += 8; otherType = 2; }
                        if (obj.transform.position.y > (float)(yf + 0.1)) { type += 1; otherType = 4; }
                        if (obj.transform.position.y < (float)(yf + 0.1)) { type += 4; otherType = 1; }
                        
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
                    
                    GameObject wall = Instantiate(Resources.Load("Prefabs/Buildings/Wall"), new Vector3(xf + 0.05f, yf + 0.1f, 1), Quaternion.identity) as GameObject;
                    
                    if (type == 0 || type == 1) { wall.GetComponent<SpriteRenderer>().sprite = sprites[7]; }
                    if (type == 2 || type == 3) { wall.GetComponent<SpriteRenderer>().sprite = sprites[0]; }
                    if (type == 4) { wall.GetComponent<SpriteRenderer>().sprite = sprites[10]; }
                    if (type == 5) { wall.GetComponent<SpriteRenderer>().sprite = sprites[9]; }
                    if (type == 6 || type == 7) { wall.GetComponent<SpriteRenderer>().sprite = sprites[2]; }
                    if (type == 8 || type == 9) { wall.GetComponent<SpriteRenderer>().sprite = sprites[1]; }
                    if (type == 10) { wall.GetComponent<SpriteRenderer>().sprite = sprites[4]; }
                    if (type == 11) { wall.GetComponent<SpriteRenderer>().sprite = sprites[5]; }
                    if (type == 12 || type == 13) { wall.GetComponent<SpriteRenderer>().sprite = sprites[3]; }
                    if (type == 14 || type == 15) { wall.GetComponent<SpriteRenderer>().sprite = sprites[6]; }
                    
                    wall.GetComponent<global::Wall>().Type = type;
                }
            }
            return new Nil();
        }
    }
    public class CreateManyWalls
    {
        public Nil Exec(Number x1, Number y1, Number x2, Number y2)
        {
            //@TODO: check for resources
            
            GodGame controller = GameObject.FindGameObjectWithTag("GodGame").GetComponent<GodGame>();

            int dir = 0;

            ushort xf1 = (ushort)x1.AsNumber();
            ushort yf1 = (ushort)y1.AsNumber();
            ushort xf2 = (ushort)x2.AsNumber();
            ushort yf2 = (ushort)y2.AsNumber();

            if (xf1 == xf2 && yf1 == yf2)
            {
                //@TODO: terminal error
                Console.print("nao pode");
                throw new EvalException("Já existe uma estrutura ai!");
            }
            else
            {
                if (xf1 > xf2)
                {
                    for (int i = xf2; i <= xf1; i++)
                    {
                        controller.Evaluator.ExecProgram("createWall(x:" + i + ", y: " + yf1 + ")");
                    }
                }
                else
                {
                    for (int i = xf1; i <= xf2; i++)
                    {
                        controller.Evaluator.ExecProgram("createWall(x:" + i + ", y: " + yf1 + ")");
                    }
                }
                
                if (yf1 > yf2)
                {
                    for (int i = yf2; i <= yf1; i++)
                    {
                        controller.Evaluator.ExecProgram("createWall(x:" + xf2 + ", y: " + i + ")");
                    }
                }
                else
                {
                    for (int i = yf1; i <= yf2; i++)
                    {
                        controller.Evaluator.ExecProgram("createWall(x:" + xf2 + ", y: " + i + ")");
                    }
                }
            }
            
            return new Nil();
        }
    }
}