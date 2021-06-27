using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Development : MonoBehaviour
{
    /* --- ENUMS --- */
    public enum Physics { Earth, Mars }

    /* --- COMPONENTS --- */
    public Status2D playerState;
    public Follow cameraFollow;
    //public List<Character2D> characters;
    public Arena dungeon;
    public Background background;

    /* --- VARIABLES --- */
    public int characterIndex;
    public bool buildMode = true;
    public bool cleanGrid = true;
    public Physics physics = Physics.Earth;

    /* --- METHODS --- */
    void Start() {
        //SetPlayer();
        dungeon.Initialize();
        background.Initialize();
        if (buildMode) { BuildMode(); }
        else {
            background.RandomizeGrid();
            for (int i = 0; i < dungeon.density; i++) {
                dungeon.AddShape(Geometry2D.Shape.ellipse, dungeon.RandomAnchor());
            }
            if (cleanGrid) { dungeon.CleanGrid(); }
            dungeon.PrintMap();
        }
    }

    void Update() { 
        if (Input.GetKeyDown("p")) {
            //characters[characterIndex].gameObject.SetActive(false);
            //NextPlayer();
        }
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
            Time.timeScale = 0f;
            cameraFollow.enabled = false;
            Camera.main.orthographicSize = 25f;
            Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);
        }
        else {
            Time.timeScale = 1f;
            cameraFollow.enabled = true;
            //SetPlayer(characters[characterIndex]);
            Camera.main.orthographicSize = 7f;
        }
    }

    void BuildModeCommands()
    {
        // reset
        if (Input.GetKeyDown("0")) {
            dungeon.Initialize();
        }
        // hide tilemap
        if (Input.GetKeyDown("h")) {
            dungeon.tilemap.gameObject.SetActive(!dungeon.tilemap.gameObject.activeSelf);
        }
        // add a shape
        if (Input.GetMouseButton(0)) {
            if (Input.GetKey("1")) {
                dungeon.AddShape(Geometry2D.Shape.ellipse, dungeon.ClickToAnchor());
            }
            if (Input.GetKey("2")) {
                dungeon.AddShape(Geometry2D.Shape.triangle, dungeon.ClickToAnchor());
            }
            else {
                int[] point = dungeon.ClickToGrid();
                dungeon.AddPoint(point[0], point[1]);
            }
            if (cleanGrid) { dungeon.CleanGrid(); }
            dungeon.PrintMap();
        }
        if (Input.GetMouseButtonDown(1)) {
            if (Input.GetKey("1")) {
                for (int i = 0; i < dungeon.density; i++) {
                    dungeon.AddShape(Geometry2D.Shape.ellipse, dungeon.RandomAnchor());
                }
            }
            if (Input.GetKey("2")) {
                for (int i = 0; i < dungeon.density; i++) {
                    dungeon.AddShape(Geometry2D.Shape.triangle, dungeon.RandomAnchor());
                }
            }
            if (cleanGrid) { dungeon.CleanGrid(); }
            dungeon.PrintMap();
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            //background.RandomizeGrid();
            //background.SetTilemap();
        }
    }

    void PhysicsSettings() {
        if (physics == Physics.Earth) {
            playerState.defaultGravity = 10f;
            playerState.jumpGravity = 3f;
        }
        else if (physics == Physics.Mars) {
            playerState.defaultGravity = 5f;
            playerState.jumpGravity = 2f;
        }
    }

    void NextPlayer() {
        /*characterIndex = (characterIndex + 1) % characters.Count;
        SetPlayer(characters[characterIndex]);*/
        
    }

    void SetPlayer() {
        /*character.gameObject.SetActive(true);
        cameraContainer.follow = character.hitbox;
        playerState.character = character;*/
    }
}
