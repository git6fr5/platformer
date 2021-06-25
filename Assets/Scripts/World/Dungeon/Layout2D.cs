using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Layout2D : MonoBehaviour
{
    /* --- COMPONENTS --- */
    public TileBase[] layoutTiles;

    /* --- VARIABLES --- */
    public static int layoutSize = 17;
    [HideInInspector] public TileBase[] tileArray;
    [HideInInspector] public TileBase empty; // 0 
    [HideInInspector] public TileBase center; // 1 => 0000, no empties ^^ need to swap empty and center around
    [HideInInspector] public TileBase rightEdge; // 2 => 0001, 1 right empty
    [HideInInspector] public TileBase ceiling; // 3 => 0010, 2 bottom empty
    [HideInInspector] public TileBase ceilingRightCorner; // 4 => 0011, 3 bottom + right empty
    [HideInInspector] public TileBase leftEdge; // 5 => 0100, 4 left empty
    [HideInInspector] public TileBase centerSpike; // 6 => 0101, 5 left + right empty
    [HideInInspector] public TileBase ceilingLeftCorner; // 7 => 0110, 6 left + bottom empty
    [HideInInspector] public TileBase ceilingSpike; // 8 => 0111, 7 left + bottom + right empty
    [HideInInspector] public TileBase floor; // 9 => 1000, 8 top empty
    [HideInInspector] public TileBase floorRightEdge; // 10 => 1001, 9 top + right empty
    [HideInInspector] public TileBase platformCenter; // 11 => 1010, 10 top + bottom empty
    [HideInInspector] public TileBase platformRightEdge; // 12 => 1011, 11 top + bottom + right empty
    [HideInInspector] public TileBase floorLeftEdge; // 13 => 1100, 12 top + left empty
    [HideInInspector] public TileBase floorSpike; // 14 => 1101, 13 top + left + right empty
    [HideInInspector] public TileBase platformLeftEdge; // 15 => 1110, 14 top + left + bottom empty
    [HideInInspector] public TileBase hangingBlock; // 16 => 1111, 15 top + left + bottom + right empty

    /* --- UNITY --- */
    void Start() {
        InitializeLayout();
    }

    /* --- METHODS --- */
    void InitializeLayout() {
        empty = null;
        center = layoutTiles[7];
        rightEdge = layoutTiles[8];
        ceiling = layoutTiles[12];
        ceilingRightCorner = layoutTiles[13];
        leftEdge = layoutTiles[6];
        centerSpike = layoutTiles[10];
        ceilingLeftCorner = layoutTiles[11];
        ceilingSpike = layoutTiles[15];
        floor = layoutTiles[2];
        floorRightEdge = layoutTiles[3];
        platformCenter = layoutTiles[17];
        platformRightEdge = layoutTiles[18];
        floorLeftEdge = layoutTiles[1];
        floorSpike = layoutTiles[0];
        platformLeftEdge = layoutTiles[16];
        hangingBlock = layoutTiles[19];
    }

    // initialize the tile array
    public void InitializeTileArray()
    {
        tileArray = new TileBase[layoutSize];
        tileArray[0] = empty;
        tileArray[1] = center; // 1 => 0000, 0 no empties ^^ need to swap empty and center around
        tileArray[2] = rightEdge; // 2 => 0001, 1 right empty
        tileArray[3] = ceiling; // 3 => 0010, 2 bottom empty
        tileArray[4] = ceilingRightCorner; // 4 => 0011, 3 bottom + right empty
        tileArray[5] = leftEdge; // 5 => 0100, 4 left empty
        tileArray[6] = centerSpike; // 6 => 0101, 5 left + right empty
        tileArray[7] = ceilingLeftCorner; // 7 => 0110, 6 left + bottom empty
        tileArray[8] = ceilingSpike; // 8 => 0111, 7 left + bottom + right empty
        tileArray[9] = floor; // 9 => 1000, 8 top empty
        tileArray[10] = floorRightEdge; // 10 => 1001, 9 top + right empty
        tileArray[11] = platformCenter; // 11 => 1010, 10 top + bottom empty
        tileArray[12] = platformRightEdge; // 12 => 1011, 11 top + bottom + right empty
        tileArray[13] = floorLeftEdge; // 13 => 1100, 12 top + left empty
        tileArray[14] = floorSpike; // 14 => 1101, 13 top + left + right empty
        tileArray[15] = platformLeftEdge; // 15 => 1110, 14 top + left + bottom empty
        tileArray[16] = hangingBlock; // 16 => 1111, 15 top + left + bottom + right empty
    }

}
