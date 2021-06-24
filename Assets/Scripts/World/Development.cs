using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Development : MonoBehaviour
{
    /* --- COMPONENTS --- */
    public Status2D playerState;
    public FollowContainer cameraContainer;
    public List<Character2D> characters;

    /* --- VARIABLES --- */
    public int characterIndex;

    /* --- METHODS --- */
    void Start() {
        SetPlayer(characters[characterIndex]);
    }

    void Update() { 
        if (Input.GetKeyDown("p")) {
            characters[characterIndex].gameObject.SetActive(false);
            NextPlayer();
        }
    }

    /* --- METHODS --- */
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
