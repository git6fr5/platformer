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
    public bool grow = true;
    [Range(0.25f, 10f)] public float growthInterval = 10f;
    [Range(1, 10)] public int growthWidth = 1;
    [Range(0f, 1f)] public float growThreshold = 0.25f;
    int[] reOrder = { -1, 0 };

    /* --- UNITY --- */
    
    /* --- OVERRIDE --- */
    public override void Generate()
    {
        ConstructGrid();
        PrintMap();
        if (grow) { StartCoroutine(GrowBoundary(growthInterval)); }
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
                    //print(i.ToString() + ", " + j.ToString());
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

    /* --- COROUTINES --- */
    IEnumerator GrowBoundary(float delay)
    {
        yield return new WaitForSeconds(delay);
        width += growthWidth;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < sizeHorizontal; j++)
            {
                if (Random.Range(0f, 1f)> growThreshold) {
                    grid[i][j] = (int)Tiles.center;
                    grid[sizeVertical - (i + 1)][j] = (int)Tiles.center;
                }           
            }
        }
        for (int i = 0; i < sizeVertical; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (Random.Range(0f, 1f) > growThreshold) {
                    grid[i][j] = (int)Tiles.center;
                    grid[i][sizeHorizontal - (j+1)] = (int)Tiles.center;
                }
            }
        }
        PrintMap();
        if (grow) { StartCoroutine(GrowBoundary(growthInterval)); };
        yield return null;
    }
}
