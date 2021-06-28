using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Development : MonoBehaviour
{
    /* --- ENUMS --- */
    public enum Physics { Earth, Mars }
    public enum Screen { Follow, Zoomed }

    /* --- COMPONENTS --- */
    public Status2D[] players;
    public Follow cameraFollow;
    public Arena arena;
    public Background background;

    /* --- VARIABLES --- */
    public bool developing = true;
    public bool buildMode = true;
    public Physics physics = Physics.Earth;
    public Screen screenMode = Screen.Zoomed;

    /* --- METHODS --- */
    void Start() {
    }

    void Update() {
        if (!developing) { return; }
        DeveloperCommands();
        if (buildMode) { BuildModeCommands(); }
        PhysicsSettings();
    }

    /* --- METHODS --- */
    void DeveloperCommands() { 
        if (Input.GetKeyDown("b")) {
            buildMode = !buildMode;
        }
    }

    void BuildModeCommands()
    {
        // reset all
        if (Input.GetKeyDown("0")) {
            arena.generate = false;
            arena.Initialize();
            background.Initialize();
        }
        // hide tilemap
        if (Input.GetKeyDown("h")) {
            arena.tilemap.gameObject.SetActive(!arena.tilemap.gameObject.activeSelf);
        }
        // brushes a shape
        if (Input.GetMouseButton(0)) {
            if (Input.GetKey("1")) {
                arena.AddShape(Geometry2D.Shape.ellipse, arena.ClickToAnchor());
            }
            if (Input.GetKey("2")) {
                arena.AddShape(Geometry2D.Shape.triangle, arena.ClickToAnchor());
            }
            else {
                int[] point = arena.ClickToGrid();
                arena.AddPoint(point[0], point[1]);
            }
            arena.CleanGrid();
            arena.PrintMap();
        }
        if (Input.GetMouseButtonDown(1)) {
            arena.Generate();
        }
    }

    void PhysicsSettings() {
        for (int i = 0; i < players.Length; i++) {
            if (physics == Physics.Earth) {
                players[i].defaultGravity = 10f;
                players[i].jumpGravity = 3f;
            }
            else if (physics == Physics.Mars) {
                players[i].defaultGravity = 5f;
                players[i].jumpGravity = 2f;
            }
        }
        
    }
}
