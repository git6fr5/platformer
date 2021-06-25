﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Development : MonoBehaviour
{
    /* --- COMPONENTS --- */
    public Status2D playerState;
    public FollowContainer cameraContainer;
    public List<Character2D> characters;
    public Dungeon2D dungeon;

    /* --- VARIABLES --- */
    public int characterIndex;
    public bool buildMode = true;

    /* --- METHODS --- */
    void Start() {
        SetPlayer(characters[characterIndex]);
        BuildMode();
    }

    void Update() { 
        if (Input.GetKeyDown("p")) {
            characters[characterIndex].gameObject.SetActive(false);
            NextPlayer();
        }
        if (Input.GetKeyDown("b")) {
            buildMode = !buildMode;
            BuildMode();
        }
        if (buildMode) {
            BuildModeCommands();
        }
    }

    /* --- METHODS --- */
    void BuildMode() {
        if (buildMode) {
            Time.timeScale = 0f;
            cameraContainer.enabled = false;
            Camera.main.orthographicSize = 50f;
            Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);
        }
        else {
            Time.timeScale = 1f;
            cameraContainer.enabled = true;
            SetPlayer(characters[characterIndex]);
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
                dungeon.AddPoint();
            }
            //dungeon.PrintGrid();
            dungeon.CleanGrid();
            dungeon.SetTilemap();
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
            //dungeon.PrintGrid();
            dungeon.CleanGrid();
            dungeon.SetTilemap();
        }
    }

    void NextPlayer() {
        characterIndex = (characterIndex + 1) % characters.Count;
        SetPlayer(characters[characterIndex]);
        
    }

    void SetPlayer(Character2D character) {
        character.gameObject.SetActive(true);
        cameraContainer.follow = character.hitbox;
        playerState.character = character;
    }
}