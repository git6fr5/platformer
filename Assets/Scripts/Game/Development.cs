using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Development : MonoBehaviour
{
    /* --- ENUMS --- */
    public enum Physics { Earth, Mars }

    /* --- COMPONENTS --- */
    public Status2D[] players;
    public Follow cameraFollow;
    public Arena arena;
    public Background background;

    /* --- VARIABLES --- */
    public bool buildMode = true;
    public Physics physics = Physics.Earth;

    /* --- METHODS --- */
    void Start() {
        if (buildMode) { BuildMode(); }
    }

    void Update() { 
        if (Input.GetKeyDown("b")) {
            buildMode = !buildMode;
            BuildMode();
        }
        if (buildMode) {
            BuildModeCommands();
        }
        PhysicsSettings();
    }

    /* --- METHODS --- */
    void BuildMode() {
        if (buildMode) {
            //Time.timeScale = 0f;
            cameraFollow.enabled = false;
            Camera.main.orthographicSize = 25f;
            Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);
        }
        else {
            Time.timeScale = 1f;
            cameraFollow.enabled = true;
            Camera.main.orthographicSize = 7f;
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
