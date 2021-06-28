using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Lava : Map2D
{

    public enum Tiles {
        empty, center,
        rightEdge,
        ceiling, ceilingRightCorner,
        leftEdge, centerSpike, ceilingLeftCorner, ceilingSpike,
        floor, floorRightEdge, platformCenter, platformRightEdge, floorLeftEdge, floorSpike, platformLeftEdge,
        hangingBlock
    };

    /* --- VARIABLES --- */
    public Arena arena;
    public int height;
    public bool grow = true;
    [Range(0.25f, 10f)] public float growthInterval = 10f;
    [Range(1, 10)] public int growthHeight = 1;
    int[] reOrder = { -1, 0 };

    /* --- UNITY --- */

    /* --- OVERRIDE --- */
    public override void Generate()
    {
        height = sizeVertical - height;
        ConstructGrid();
        PrintMap();
        if (grow) { StartCoroutine(GrowLava(growthInterval)); }
    }

    // organizes the input tiles
    public override void OrganizeLayouts() {
        for (int i = 0; i < tileLayouts.Length; i++) {
            ReorderLayout(tileLayouts[i]);
        }
    }

    /* --- METHODS --- */
    public void ConstructGrid() {
        for (int i = 0; i < sizeVertical; i++) {
            for (int j = 0; j < sizeHorizontal; j++) {
                if (i > height) {
                    print(i.ToString() + ", " + j.ToString());
                    grid[i][j] = (int)Tiles.center;
                }
            }
        }
    }

    // reorder the layouts to be able to tell what type they are
    void ReorderLayout(Layout2D tileLayout) {
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

    /* --- COROUTINES --- */
    IEnumerator GrowLava(float delay) {
        yield return new WaitForSeconds(delay);
        height -= growthHeight;
        for (int j = 0; j < sizeHorizontal; j++) {
            //print(i.ToString() + ", " + j.ToString());
            grid[height][j] = (int)Tiles.center;
            arena.CutTile(height-1, j);
        }
        PrintMap();
        if (grow) { StartCoroutine(GrowLava(growthInterval)); };
        yield return null;
    }
}
