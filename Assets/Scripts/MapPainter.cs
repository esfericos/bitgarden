using System;
using System.Data;
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
    public Tile gbilt;
    public Tile gbirt;
    public Tile gbilb;
    public Tile gbirb;

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
            BgTileKind.Grass => grass,
            BgTileKind.Gbilt => gbilt,
            BgTileKind.Gbirt => gbirt,
            BgTileKind.Gbilb => gbilb,
            BgTileKind.Gbirb => gbirb,
            // Unreachable since the `None` case was already early handled above.
            BgTileKind.None => throw new Exception("Unreachable"),
            _ => throw new DataException("Unhandled BgTileKind in MapPainter")
        };
        tilemap.SetTile(position, selected);
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