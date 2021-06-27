using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Boundary : Map2D
{

    public enum Tiles
    {
        empty, center,
        rightEdge,
        ceiling, ceilingRightCorner,
        leftEdge, centerSpike, ceilingLeftCorner, ceilingSpike,
        floor, floorRightEdge, platformCenter, platformRightEdge, floorLeftEdge, floorSpike, platformLeftEdge,
        hangingBlock
    };

    /* --- VARIABLES --- */
    public int width;
    int[] reOrder = { -1, 0 };

    /* --- OVERRIDE --- */
    public override void Generate()
    {
        ConstructGrid();
        PrintMap();
    }

    // organizes the input tiles
    public override void OrganizeLayouts() {
        for (int i = 0; i < tileLayouts.Length; i++) {
            ReorderLayout(tileLayouts[i]);
        }
    }

    /* --- METHODS --- */
    public void ConstructGrid()
    {
        for (int i = 0; i < sizeVertical; i++)
        {
            for (int j = 0; j < sizeHorizontal; j++)
            {
                if ((i < width || i >= sizeVertical - width) || ((j < width || j >= sizeHorizontal - width))) {
                    print(i.ToString() + ", " + j.ToString());
                    grid[i][j] = (int)Tiles.center;
                }
            }
        }
    }



    // reorder the layouts to be able to tell what type they are
    void ReorderLayout(Layout2D tileLayout)
    {
        TileBase[] _tileLayout = tileLayout.tiles;
        tileLayout.tiles = new TileBase[tileLayout.tiles.Length];

        for (int i = 0; i < _tileLayout.Length; i++)
        {
            if (reOrder[i] == -1) { tileLayout.tiles[i] = null; }
            else
            {
                tileLayout.tiles[i] = _tileLayout[reOrder[i]];
            }
        }
    }
}
