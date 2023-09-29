using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapPainter : MonoBehaviour
{
    public Vector3Int position;
    public Tilemap tilemap;

    public enum Tiles
    {
        Grass,
        Gbt,
        Gbb,
        Gbl,
        Gblb,
        Gblt,
        Gbr,
        Gbrb,
        Gbrt
    }
    public Tiles TileToPaint;

    public Tile grass;
    public Tile gbt;
    public Tile gbb;
    public Tile gbl;
    public Tile gblb;
    public Tile gblt;
    public Tile gbr;
    public Tile gbrb;
    public Tile gbrt;

    [ContextMenu("Paint")]
    void Paint()
    {
        Tile selected = grass;
        switch (TileToPaint)
        {
            case Tiles.Gbt:
                selected = gbt; 
                break;
            case Tiles.Gbb:
                 selected = gbb;
                break;
             case Tiles.Gbl:    
                selected = gbl;
                break;
             case Tiles.Gblb:
                selected = gbrb;
                break;
            case Tiles.Gblt:
                selected = gblb;
                break;
            case Tiles.Gbr:
                selected = gbr;
                break;
            case Tiles.Gbrb:
                selected = gbrb;
                break;
            case Tiles.Gbrt:
                selected = gbrb;
                break;
            default:
                selected = grass;
                break;
        }
        tilemap.SetTile(position, selected);
    }
}
