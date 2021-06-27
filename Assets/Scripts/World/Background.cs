using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Background : Map2D
{

    public void Generate() {
        RandomizeGrid();
        PrintMap();
    }

    public void RandomizeGrid() {
        for (int i = 0; i < sizeVertical; i++) {
            for (int j = 0; j < grid[i].Length; j++) {
                grid[i][j] = Random.Range(1, 9);
            }
        }
    } 
}
