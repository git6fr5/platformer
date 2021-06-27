﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Arena : Map2D
{
    /* --- ENUMS --- */
    public enum Tiles {
        empty, center,
        rightEdge,
        ceiling, ceilingRightCorner,
        leftEdge, centerSpike, ceilingLeftCorner, ceilingSpike,
        floor, floorRightEdge, platformCenter, platformRightEdge, floorLeftEdge, floorSpike, platformLeftEdge,
        hangingBlock
    };

    /* --- VARIABLES --- */
    public bool cleanGrid = true;
    [Range(64, 512)] public int density = 256;
    public bool grow = true;
    [Range(0.25f, 5f)] public float growthInterval = 1f;
    [Range(0.05f, 0.999f)] public float growThreshold = 0.95f;
    int[] reOrder = new int[] { -1, 7, 8, 12, 13, 6, 10, 11, 15, 2, 3, 17, 18, 1, 0, 16, 19, 0, 0, 0, 0, 0, 0, 0, 0 };

    /* --- OVERRIDE --- */
    public override void Generate() {
        ConstructGrid();
        if (grow) { StartCoroutine(GrowArena(growthInterval)); }
        PrintMap();
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
            for (int j = 0; j < grid[i].Length; j++) {
                grid[i][j] = (int)Tiles.center;
            }
        }
        for (int i = 0; i < density; i++) {
            AddShape(Geometry2D.Shape.ellipse, RandomAnchor());
        }
        if (cleanGrid) { CleanGrid(); }
    }

    // itterate through the grid and clean each cell
    public void CleanGrid() {
        for (int i = 0; i < sizeVertical; i++) {
            for (int j = 0; j < sizeHorizontal; j++) {
                CleanCell(i, j);
            }
        }
    }

    // check what type of cell it is based on its immediate surroundings
    public void CleanCell(int i, int j) {
        // check only the non-empty tiles
        int code = 1; // starting from one to account for the 0th null tile
        if (grid[i][j] != (int)Tiles.empty) {
            // is top empty
            if (CellEmpty(i-1, j)) {
                code += 8;
            }
            // is right empty
            if (CellEmpty(i, j - 1)) {
                code += 4;
            }
            // is bottom empty
            if (CellEmpty(i+1, j)) {
                code += 2;
            }
            // is left empty (i think this might be backwards but it just started working and im scared to mess with it)
            if (CellEmpty(i, j + 1)) {
                code += 1;
            }
            grid[i][j] = code;
        } 
    }

    // check if the cell at the given coordinates is empty
    bool CellEmpty(int i, int j) {
        if (i < 0 || i > grid.Length - 1 || j < 0 || j > grid[0].Length - 1) { return true; }
        if (grid[i][j] == (int)Tiles.empty) {
            return true;
        }
        return false;
    }

    IEnumerator GrowArena(float delay)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < grid.Length; i++) {
            for (int j = 0; j < grid[0].Length; j++) {
                if (grid[i][j] == (int)Tiles.empty) {
                    if (Random.Range(0f, 1f)> growThreshold) {
                        GrowCell(i, j);
                    }
                }
            }
        }
        CleanGrid();
        PrintMap();
        if (grow) { StartCoroutine(GrowArena(growthInterval)); };
        yield return null;
    }

    void GrowCell(int i, int j)
    {
        int code = 0;
        // is top empty
        if (CellEmpty(i-1, j)) {
            code += 8;
        }
        // is right empty
        if (CellEmpty(i, j - 1)) {
            code += 4;
        }
        // is bottom empty
        if (CellEmpty(i+1, j)) {
            code += 2;
        }
        // is left empty (i think this might be backwards but it just started working and im scared to mess with it)
        if (CellEmpty(i, j + 1)) {
            code += 1;
        }
        // if all of them are empty
        if (code != 15) {
            grid[i][j] = (int)Tiles.center;
        }
    }

    // reorder the layouts to be able to tell what type they are
    void ReorderLayout(Layout2D tileLayout) {

        TileBase[] _tileLayout = tileLayout.tiles;
        tileLayout.tiles = new TileBase[tileLayout.tiles.Length];

        for (int i = 0; i < _tileLayout.Length; i++) {
            if (reOrder[i] == -1) { tileLayout.tiles[i] = null; }
            else {
                tileLayout.tiles[i] = _tileLayout[reOrder[i]];
            }
        }
    }

    /* --- BRUSHES --- */
    // adds a point at the given coordinates
    public void AddPoint(int i, int j)
    {
        int[] point = new int[] { i, j };
        if (PointInGrid(point))
        {
            grid[point[0]][point[1]] = (int)Tiles.center;
        }
    }

    // add a shape subgrid
    public void AddShape(Geometry2D.Shape shape, Anchor2D anchor)
    {
        // get the scale of the shape
        float scaleVert = Random.Range(sizeMin, sizeMax);
        float scaleHor = Random.Range(sizeMin, sizeMax);
        // create the shape subgrid
        int[][] subGrid = Geometry2D.ConstructShape(shape, (int)(sizeVertical * scaleVert), (int)(sizeHorizontal * scaleHor));
        // add the shape subgrid to the grid
        AttachToGrid(subGrid, anchor);
    }

    // attach a subgrid to the grid
    public void AttachToGrid(int[][] subGrid, Anchor2D _anchor) {
        int[] anchor = _anchor.AnchorToGrid(grid, subGrid);
        for (int i = 0; i < subGrid.Length; i++) {
            for (int j = 0; j < subGrid[0].Length; j++) {
                if (subGrid[i][j] != Geometry2D.background) {
                    int[] point = new int[] { i + anchor[0], j + anchor[1] };
                    if (PointInGrid(point)) {
                       grid[i + anchor[0]][j + anchor[1]] = subGrid[i][j];

                    }
                }
            }
        }
    }
}