using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : BulletContainer
{

    public override void DoThing() {
        int[] gridPoint = dungeon.PointToGrid((Vector2)transform.position);
        print(gridPoint[0].ToString() + ", " + gridPoint[1].ToString());
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                dungeon.grid[gridPoint[0] + i][gridPoint[1] + j] = 0;
                dungeon.grid[gridPoint[0] + i][gridPoint[1] - j] = 0;
                dungeon.grid[gridPoint[0] - i][gridPoint[1] + j] = 0;
                dungeon.grid[gridPoint[0] - i][gridPoint[1] - j] = 0;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                dungeon.CleanCell(gridPoint[0] + i, gridPoint[1] + j);
                dungeon.CleanCell(gridPoint[0] + i, gridPoint[1] - j);
                dungeon.CleanCell(gridPoint[0] - i, gridPoint[1] + j);
                dungeon.CleanCell(gridPoint[0] - i, gridPoint[1] - j);
                dungeon.SetTile(gridPoint[0] + i, gridPoint[1] + j);
                dungeon.SetTile(gridPoint[0] + i, gridPoint[1] - j);
                dungeon.SetTile(gridPoint[0] - i, gridPoint[1] + j);
                dungeon.SetTile(gridPoint[0] - i, gridPoint[1] - j);
            }
        }

        //Anchor2D anchor = Anchor2D.GridToAnchor(dungeon.grid, gridPoint);
        //dungeon.AddShape(Geometry2D.Shape.ellipse, anchor);
        gameObject.SetActive(false);
    }

}
