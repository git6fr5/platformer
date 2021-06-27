using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class Map2D : MonoBehaviour
{

    /* --- COMPONENTS --- */
    [Space(5)][Header("Tilemap")]
    public Tilemap tilemap;
    // layouts
    [Space(5)][Header("Tiles")]
    public Layout2D[] tileLayouts;
    public float[] tileLayoutDistribution;
    private float distributionSum = 0f;

    /* --- VARIABLES --- */
    [Space(5)] [Header("Auto Generate")]
    public bool generate = true;
    // arena dimensions 
    [Space(5)] [Header("Arena Dimensions")]
    [HideInInspector] public int[][] grid;
    [Range(16, 128)] public int sizeVertical = 64;
    [Range(16, 128)] public int sizeHorizontal = 64;
    // brush size
    [Space(5)][Header("Brush Size")]
    [Range(0.05f, 0.8f)] public float sizeMin = 0.2f;
    [Range(0.05f, 0.8f)] public float sizeMax = 0.2f;
    // offset
    int horOffset = 0;
    int vertOffset = 0;

    /* --- UNITY --- */
    void Awake() {
        Initialize();
        if (generate) { Generate(); }
    }

    /* --- VIRTUAL --- */
    public virtual void Generate(){
        // generate
    }

    public virtual void OrganizeLayouts() { 
        // organize layouts
    }

    /* --- INITIALIZERS --- */
    public void Initialize() {
        foreach (float num in tileLayoutDistribution) {
            distributionSum += num;
        }
        OrganizeLayouts();
        SetGrid();
        SetMap();
    }


    // initialize a grid
    void SetGrid() {
        grid = new int[sizeVertical][];
        for (int i = 0; i < sizeVertical; i++) {
            grid[i] = new int[sizeHorizontal];
        }
    }

    // initialize a tilemap
    void SetMap() {
        horOffset = (int)sizeHorizontal / 2;
        vertOffset = (int)sizeVertical / 2;
        PrintMap();
    }

    // prints the grid to a tilemap
    public void PrintMap() {
        for (int i = 0; i < sizeVertical; i++) {
            for (int j = 0; j < sizeHorizontal; j++) {
                PrintTile(i, j);
            }
        }
    }

    // prints out a grid cell to a tile
    public void PrintTile(int i, int j) {
        // get the tile position from the grid coordinates
        Vector3Int tilePosition = GridToTileMap(i, j);
        // pick a tile from a random layout style
        Layout2D tileLayout = RandomLayout();
        if (grid[i][j] >= tileLayout.tiles.Length) { return; }
        TileBase tileBase = RandomLayout().tiles[grid[i][j]];
        // set the tile 
        tilemap.SetTile(tilePosition, tileBase);
    }

    /* --- CONVERSION --- */
    // mouse click to grid coordinate
    public int[] ClickToGrid() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return PointToGrid(mousePos);
    }

    // a given point to grid coordinates 
    public int[] PointToGrid(Vector2 point) {
        int gridY = grid.Length - (int)(point.y + vertOffset);
        int gridX = (int)(point.x + horOffset);
        return new int[] { gridY, gridX };
    }

    // checks if a coordinate is in the grid
    public bool PointInGrid(int[] point) {
        return (point[1] < sizeHorizontal && point[1] >= 0 && point[0] < sizeVertical && point[0] >= 0);
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
    public Anchor2D RandomCardinalAnchor() {
        return Anchor2D.anchors[Random.Range(0, Anchor2D.anchors.Length)];
    }

    // generates a random anchor
    public Anchor2D RandomAnchor() {
        return new Anchor2D(Random.Range(0.05f, 0.95f), Random.Range(0.05f, 0.95f));
    }

    public Layout2D RandomLayout() {
        float prob = Random.Range(0f, distributionSum);
        float distribution = 0f;
        for (int i = 0; i < tileLayoutDistribution.Length; i++)
        {
            distribution += tileLayoutDistribution[i];
            if (prob < distribution)
            {
                return tileLayouts[i];
            }
        }
        return tileLayouts[0];
    }
}
