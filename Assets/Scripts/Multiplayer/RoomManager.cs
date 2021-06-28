using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoomManager : MonoBehaviour
{
    public Text roomIDText;
    public GameObject playerPrefab;
    public Arena arena;

    void Start() {
        print("Player has joined");
        print(PhotonNetwork.LocalPlayer.NickName);
        arena.GetGridFromHost();
        PhotonNetwork.Instantiate(playerPrefab.name, 1.5f*Random.insideUnitCircle, Quaternion.identity);
    }
}
