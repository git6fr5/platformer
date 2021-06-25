using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class Dungeon2D : MonoBehaviour
{
    /* --- ENUMS --- */
    public enum Tiles { center, empty, leftWall, rightWall, floor, ceiling,
                        floorRight, floorLeft, floorSpike,
                        ceilingLeft, ceilingRight, ceilingSpike,
                        leftWallSpike, rightWallSpike };

    /* --- COMPONENTS --- */
    [Space(5)][Header("Displays")]
    public Text textBox;
    public Tilemap tilemap;
    // tiles
    [Space(5)][Header("Tiles")]
    public TileBase[] tileArray;
    public TileBase center; // 0
    public TileBase empty; // 1
    public TileBase leftWall; // 2
    public TileBase rightWall; // 3
    public TileBase floor; // 4
    public TileBase ceiling; // 5
    public TileBase floorRight; // 6
    public TileBase floorLeft; // 7
    public TileBase floorSpike; // 8
    public TileBase ceilingLeft; // 9
    public TileBase ceilingRight; // 10
    public TileBase ceilingSpike; // 11
    public TileBase leftWallSpike; // 12
    public TileBase rightWallSpike; // 13

    /* --- VARIABLES --- */
    // grid dimensions 
    [Space(5)] [Header("Grid Dimensions")]
    [HideInInspector] public int[][] grid;
    public bool printGrid = false;
    [Range(16, 64)] public int sizeVertical = 25;
    [Range(16, 64)] public int sizeHorizontal = 25;
    // brush size
    [Space(5)][Header("Brush Size")]
    [Range(0.05f, 0.8f)] public float sizeMin = 0.2f;
    [Range(0.05f, 0.8f)] public float sizeMax = 0.2f;
    [Range(50, 500)] public int density = 250;
    // offset
    int horOffset = 0;
    int vertOffset = 0;

    /* --- UNITY --- */
    void Start() {
        //Initialize();
    }

    /* --- INITIALIZERS --- */
    public void Initialize() {
        InitializeTileArray();
        InitializeGrid();
        InitializeTileMap();
    }

    // initialize the tile array
    void InitializeTileArray() {
        tileArray = new TileBase[16];
        tileArray[0] = center; tileArray[1] = empty; // center, empty
        tileArray[2] = leftWall; tileArray[3] = rightWall; // left wall, right wall
        tileArray[4] = floor; tileArray[5] = ceiling; // floor , ceiling
        tileArray[6] = floorRight; tileArray[7] = floorLeft; tileArray[8] = floorSpike; // floor corners
        tileArray[9] = ceilingLeft; tileArray[10] = ceilingRight; tileArray[11] = ceilingSpike; // ceiling Corners
        tileArray[12] = leftWallSpike; tileArray[13] = rightWallSpike; // spikes
    }

    // initialize a grid
    void InitializeGrid() {
        grid = new int[sizeVertical][];
        for (int i = 0; i < sizeVertical; i++) {
            grid[i] = new int[sizeHorizontal];
        }
        if (printGrid) { PrintGrid(); }
    }

    // initialize a tilemap
    void InitializeTileMap() {
        horOffset = (int)sizeHorizontal / 2;
        vertOffset = (int)sizeVertical / 2;
        SetTilemap();
    }

    // prints the dungeon grid to a textbox
    public void PrintGrid() { 
        string text = "";
        for (int i = 0; i < sizeVertical; i++) {
            for (int j = 0; j < sizeHorizontal; j++) {
                text = text + grid[i][j].ToString() + "  ";
            }
            text = text + "\n";
        }
        textBox.text = text;
    }

    // prints the dungeon grid to a tilemap
    public void SetTilemap() {
        if (printGrid) { PrintGrid(); }
        for (int i = 0; i < sizeVertical; i++) {
            for (int j = 0; j < sizeHorizontal; j++) {
                SetTile(i, j);
            }
        }
    }

    // prints out a grid cell to a tile
    void SetTile(int i, int j) {
        if (grid[i][j] > tileArray.Length) { return; }
        Vector3Int tilePosition = GridToTileMap(i, j);
        TileBase tileBase = tileArray[grid[i][j]];
        tilemap.SetTile(tilePosition, tileBase);
        if (grid[i][j] == 1) {
            tilemap.SetTile(tilePosition, null);
        }
    }

    public void CleanGrid() {
        for (int i = 0; i < sizeVertical; i++) {
            for (int j = 0; j < sizeHorizontal; j++) {
                CleanCell(i, j);
            }
        }
    }

    void CleanCell(int i, int j) {
        // check only the empty tiles
        if (grid[i][j] == 1) {
            // above is not empty, so make it a celing
            if (i > 0 && grid[i-1][j] != 1) {
                grid[i - 1][j] = (int)Tiles.ceiling;
            }
            // below is not empty, so make it a floor
            if (i < grid.Length - 1 && grid[i + 1][j] != 1) {
                grid[i + 1][j] = (int)Tiles.floor;
            }
            // right is not empty so make it a left wall
            if (j > 0 && grid[i][j - 1] != 1) {
                grid[i][j - 1] = (int)Tiles.leftWall;
            }
            // left is not empty so make it a right wall
            if (j < grid.Length && grid[i][j + 1] != 1) {
                grid[i][j + 1] = (int)Tiles.rightWall;
            }
        } 
    }

    /* --- BRUSHES --- */
    // adds a point
    public void AddPoint() {
        int[] point = ClickToGrid();
        if (PointInGrid(point)) {
            grid[point[0]][point[1]] = 1;
        }
    }

    // add a shape subgrid
    public void AddShape(Geometry2D.Shape shape, Anchor2D anchor) {
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
                if (subGrid[i][j] != 0) {
                    grid[i + anchor[0]][j + anchor[1]] = subGrid[i][j];
                }
            }
        }
    }

    /* --- CONVERSION --- */
    // mouse click to grid coordinate
    public int[] ClickToGrid() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int gridY = -(int)(mousePos.y + vertOffset);
        int gridX = (int)(mousePos.x + horOffset);
        return new int[] { gridY, gridX };
    }

    // checks if a coordinate is in the grid
    public bool PointInGrid(int[] point) {
        return (point[1] < sizeHorizontal && point[1] > 0 && point[0] < sizeVertical && point[0] > 0);
    }

    // mouse click to anchor point
    public Anchor2D ClickToAnchor() {
        Anchor2D anchor = Anchor2D.GridToAnchor(grid, ClickToGrid());
        return anchor;
    }

    // grid coordinate to tilemap position
    Vector3Int GridToTileMap(int i, int j) { 
        return new Vector3Int(j - horOffset, -(i - vertOffset), 0);
    }

    // random anchor snapped to cardinal direction
    public Anchor2D RandomCardinalAnchor()
    {
        return Anchor2D.anchors[Random.Range(0, Anchor2D.anchors.Length)];
    }

    // generates a random anchor
    public Anchor2D RandomAnchor()
    {
        return new Anchor2D(Random.Range(0.05f, 0.95f), Random.Range(0.05f, 0.95f));
    }
}
