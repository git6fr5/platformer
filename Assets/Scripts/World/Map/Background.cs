using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Background : Map2D
{
    /* --- OVERRIDE --- */
    public override void Generate() {
        RandomizeGrid();
        PrintMap();
    }

    /* --- METHODS --- */
    public void RandomizeGrid() {
        for (int i = 0; i < sizeVertical; i++) {
            for (int j = 0; j < sizeHorizontal; j++) {
                grid[i][j] = Random.Range(1, 9);
            }
        }
    } 
}
