using System;
using System.Data;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapPainter : MonoBehaviour
{
    public Tilemap tilemap;

    public Tile grass;
    public Tile grasseffecta;
    public Tile grasseffectb;
    public Tile gbt;
    public Tile gbb;
    public Tile gbl;
    public Tile gblb;
    public Tile gblt;
    public Tile gbr;
    public Tile gbrb;
    public Tile gbrt;
    public Tile gbilt;
    public Tile gbirt;
    public Tile gbilb;
    public Tile gbirb;
    public Tile road1;
    public Tile road2;
    public Tile road3;
    public Tile road4;
    public Tile road5;
    public Tile road6;
    public Tile road7;
    public Tile road8;
    public Tile road9;
    public Tile road10;
    public Tile snow;
    public Tile sbt;
    public Tile sbb;
    public Tile sbl;
    public Tile sblb;
    public Tile sblt;
    public Tile sbr;
    public Tile sbrb;
    public Tile sbrt;
    public Tile sbilt;
    public Tile sbirt;
    public Tile sbilb;
    public Tile sbirb;
    public Tile tree;
    public Tile tree2;
    public Tile tree3;
    public Tile tree4;


    public void Paint(BgTile tile)
    {
        if (tile.Kind == BgTileKind.None) return;
        var pos = tile.Position;
        Vector3Int position = new Vector3Int(pos.X, pos.Y);
        Tile selected = tile.Kind switch
        {
            BgTileKind.Gbt => gbt,
            BgTileKind.Gbb => gbb,
            BgTileKind.Gbl => gbl,
            BgTileKind.Gbrb => gbrb,
            BgTileKind.Gblt => gblt,
            BgTileKind.Gbr => gbr,
            BgTileKind.Gblb => gblb,
            BgTileKind.Gbrt => gbrt,
            BgTileKind.Grass => GetGrassType(),
            BgTileKind.Gbilt => gbilt,
            BgTileKind.Gbirt => gbirt,
            BgTileKind.Gbilb => gbilb,
            BgTileKind.Gbirb => gbirb,
            BgTileKind.Road1 => road1,
            BgTileKind.Road2 => road2,
            BgTileKind.Road3 => road3,
            BgTileKind.Road4 => road4,
            BgTileKind.Road5 => road5,
            BgTileKind.Road6 => road6,
            BgTileKind.Road7 => road7,
            BgTileKind.Road8 => road8,
            BgTileKind.Road9 => road9,
            BgTileKind.Road10 => road10,
            BgTileKind.Sbt => sbt,
            BgTileKind.Sbb => sbb,
            BgTileKind.Sbl => sbl,
            BgTileKind.Sbrb => sbrb,
            BgTileKind.Sblt => sblt,
            BgTileKind.Sbr => sbr,
            BgTileKind.Sblb => sblb,
            BgTileKind.Sbrt => sbrt,
            BgTileKind.Snow => snow,
            BgTileKind.Sbilt => sbilt,
            BgTileKind.Sbirt => sbirt,
            BgTileKind.Sbilb => sbilb,
            BgTileKind.Sbirb => sbirb,
            BgTileKind.Tree => tree,
            BgTileKind.Tree2 => tree2,
            BgTileKind.Tree3 => tree3,
            BgTileKind.Tree4 => tree4,
            // Unreachable since the `None` case was already early handled above.
            BgTileKind.None => throw new Exception("Unreachable"),
            _ => throw new DataException("Unhandled BgTileKind in MapPainter")
        };
        tilemap.SetTile(position, selected);
    }


    private Tile GetGrassType()
{
        float randomNumber = Random.Range(0, 10);
        if(randomNumber == 1 )
        {
            return grasseffecta;
        } else
        {
            return grass;
        }


}
    /// <summary>
    /// Renders Entity in the map.
    /// </summary>
    public void PaintEntity(Entity entity, Position pos)
    {
        Vector3 posVec = new Vector3(pos.X + 1, pos.Y + 1);
        entity.Render(posVec);
    }
}