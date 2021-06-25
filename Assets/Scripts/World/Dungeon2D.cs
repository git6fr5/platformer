using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class Dungeon2D : MonoBehaviour
{
    /* --- ENUMS --- */
    public enum Tiles { empty, center, 
                        rightEdge, 
                        ceiling, ceilingRightCorner,
                        leftEdge, centerSpike, ceilingLeftCorner, ceilingSpike,
                        floor, floorRightEdge, platformCenter, platformRightEdge, floorLeftEdge, floorSpike, platformLeftEdge,
                        hangingBlock };

    /* --- COMPONENTS --- */
    [Space(5)][Header("Displays")]
    public Text textBox;
    public Tilemap tilemap;
    // layouts
    [Space(5)][Header("Tiles")]
    public Layout2D[] layouts;
    public float[] layoutDistribution;
    private float distributionSum = 0f;

    /* --- VARIABLES --- */
    // grid dimensions 
    [Space(5)] [Header("Grid Dimensions")]
    [HideInInspector] public int[][] grid;
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
        foreach (Layout2D layout in layouts) {
            layout.InitializeTileArray();
        };
        foreach (float num in layoutDistribution) {
            distributionSum += num;
        }
        InitializeGrid();
        InitializeTileMap();
    }

    // initialize a grid
    void InitializeGrid() {
        grid = new int[sizeVertical][];
        for (int i = 0; i < sizeVertical; i++) {
            grid[i] = new int[sizeHorizontal];
            for (int j = 0; j < grid[i].Length; j++) {
                grid[i][j] = 1;
            }
        }
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
        for (int i = 0; i < sizeVertical; i++) {
            for (int j = 0; j < sizeHorizontal; j++) {
                SetTile(i, j);
            }
        }
    }

    // prints out a grid cell to a tile
    public void SetTile(int i, int j) {
        if (grid[i][j] > Layout2D.layoutSize) { return; }
        Vector3Int tilePosition = GridToTileMap(i, j);
        Layout2D layout = RandomLayout();
        TileBase tileBase = layout.tileArray[grid[i][j]];
        tilemap.SetTile(tilePosition, tileBase);
        if (grid[i][j] == 0) {
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

    bool CellEmpty(int i, int j) {
        if (i < 0 || i > grid.Length - 1 || j < 0 || j > grid[0].Length - 1) { return true; }
        if (grid[i][j] == (int)Tiles.empty) {
            return true;
        }
        return false;
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
                if (subGrid[i][j] != Geometry2D.background) {
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

    public int[] PointToGrid(Vector2 point) {
        int gridY = grid.Length - (int)(point.y + vertOffset);
        int gridX = (int)(point.x + horOffset);
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

    public Layout2D RandomLayout() {
        float prob = Random.Range(0f, distributionSum);
        float distribution = 0f;
        for (int i = 0; i < layoutDistribution.Length; i++)
        {
            distribution += layoutDistribution[i];
            if (prob < distribution)
            {
                return layouts[i];
            }
        }
        return layouts[0];
    }
}
