using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Background2D : Dungeon2D
{
    public void RandomizeGrid()
    {
        grid = new int[sizeVertical][];
        for (int i = 0; i < sizeVertical; i++)
        {
            grid[i] = new int[sizeHorizontal];
            for (int j = 0; j < grid[i].Length; j++)
            {
                grid[i][j] = Random.Range(1, Layout2D.layoutSize);
            }
        }
    }
}
