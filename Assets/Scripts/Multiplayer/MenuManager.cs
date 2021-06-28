using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class MenuManager : MonoBehaviourPunCallbacks
{
    /* --- DEBUG --- */
    public bool doDebug = true;
    string DebugTag = "[Menu]: ";

    /* --- COMPONENTS --- */
    // inputs
    [Space(5)][Header("Input Fields")]
    public InputField playerName;
    // buttons
    [Space(5)] [Header("Buttons")]
    public Play playButton;
    
    /* --- VARIABLES --- */
    // rooms
    [Space(5)] [Header("Rooms")]
    public int rooms = 0;
    public int players = 0;
    List<RoomInfo> roomInfoList = new List<RoomInfo>();
    public List<string> roomIdList = new List<string>();

    /* --- Unity Methods --- */
    void Awake() {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    /* --- PHOTON --- */
    
    // connection
    public override void OnConnectedToMaster() {
        print(DebugTag + "A player has connected to master");
        // Join the lobby
        PhotonNetwork.JoinLobby();
        // Allow interaction with the menu
    }

    // disconnection
    public override void OnDisconnected(DisconnectCause cause) {
        Debug.Log("Disconnected because " + cause.ToString());
    }

    // lobby
    public override void OnJoinedLobby() {
        Debug.Log("A player has joined the lobby");
        playButton.interactable = true;
    }

    // room
    public override void OnJoinedRoom() {
        Debug.Log(DebugTag + PhotonNetwork.LocalPlayer.NickName + " has joined the room " + PhotonNetwork.CurrentRoom.Name);
        if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.LoadLevel(1);
            Debug.Log("Beginning game");
        }
    }

    // room List
    public override void OnRoomListUpdate(List<RoomInfo> _roomInfoList) {
        Debug.Log(DebugTag + "Updating Room List");
        List<RoomInfo> roomInfoList = _roomInfoList;
        roomIdList = new List<string>();
        foreach (RoomInfo roomInfo in roomInfoList) {
            roomIdList.Add(roomInfo.Name);
        }
        Debug.Log(DebugTag + "Found " + roomInfoList.Count + " rooms");
    }

    /* --- METHODS --- */

    // sets the players name
    public string SetPlayerName() {
        if (playerName.text == "") { playerName.text = players.ToString(); }
        PhotonNetwork.LocalPlayer.NickName = playerName.text;
        players++;
        return playerName.text; 
    }

    // sets the room id for the player
    public string SetRoomID() { 
        foreach (RoomInfo roomInfo in roomInfoList) {
            print(roomInfo.Name);
        }
        if (roomInfoList.Count > 0) {
            return roomInfoList[0].Name;
        }
        return CreateRoom();
    }

    // creates a room in case one couldn't be found
    public string CreateRoom() {
        string roomID = rooms.ToString();
        PhotonNetwork.CreateRoom(roomID);
        rooms += 1;
        return roomID;
    }

    // joins a room
    public void JoinRoom(string roomID) {
        PhotonNetwork.JoinRoom(roomID);
    }

}
