using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapPainter : MonoBehaviour
{
    public Tilemap tilemap;

    public Tile grass;
    public Tile gbt;
    public Tile gbb;
    public Tile gbl;
    public Tile gblb;
    public Tile gblt;
    public Tile gbr;
    public Tile gbrb;
    public Tile gbrt;


    public void Paint(string type, int x, int y)
    {
        Vector3Int position = new Vector3Int(x, y);
        Tile selected = grass;

        if (type == "none") return;

        switch (type)
        {
            case "gbt":
                selected = gbt;
                break;
            case "gbb":
                selected = gbb;
                break;
            case "gbl":
                selected = gbl;
                break;
            case "gbrb":
                selected = gbrb;
                break;
            case "gblt":
                selected = gblt;
                break;
            case "gbr":
                selected = gbr;
                break;
            case "gblb":
                selected = gblb;
                break;
            case "gbrt":
                selected = gbrt;
                break;
            case "grass":
                selected = grass;
                break;
            default:
                selected = grass;
                break;
        }
        tilemap.SetTile(position, selected);
    }
}