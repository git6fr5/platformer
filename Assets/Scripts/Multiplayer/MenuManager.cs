using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class MenuManager : MonoBehaviourPunCallbacks
{
    /* --- Debugging ---*/
    //private bool isDebug = true;
    private string DebugTag = "[CardGame MenuManager]: ";

    /* --- Components --- */

    // Inputs
    public InputField playerName;
    public InputField roomName;

    // Buttons
    public Button createRoomButton;
    public Button joinRoomButton;
    public Button playButton;

    // Rooms
    public RoomListing roomListing;
    public Transform roomList;

    /* --- Unity Methods --- */
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        createRoomButton.interactable = false;
        joinRoomButton.interactable = false;

        PhotonNetwork.ConnectUsingSettings();
    }

    /* --- Photon Methods --- */
    
    // Connection
    public override void OnConnectedToMaster()
    {
        Debug.Log(DebugTag + "A player has connected to master");

        // Join the lobby
        PhotonNetwork.JoinLobby();

        // Allow interaction with the menu
        createRoomButton.interactable = true;
        joinRoomButton.interactable = true;
    }

    // Disconnection
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected because " + cause.ToString());
    }

    // Lobby
    public override void OnJoinedLobby()
    {
        Debug.Log("A player has joined the lobby");
    }

    // Room
    public override void OnJoinedRoom()
    {
        Debug.Log(DebugTag + PhotonNetwork.LocalPlayer.NickName + " has joined the room " + PhotonNetwork.CurrentRoom.Name);
    }

    // Room List
    public override void OnRoomListUpdate(List<RoomInfo> roomInfoList)
    {
        Debug.Log(DebugTag + "Updating Room List");
        foreach (RoomInfo roomInfo in roomInfoList)
        {
            RoomListing _roomListing = Instantiate(roomListing, roomList);
            _roomListing.gameObject.SetActive(true);
            _roomListing.SetInfo(roomInfo);
        }
        Debug.Log(DebugTag + "Found " + roomInfoList.Count + " rooms");
    }


    /* --- Button Methods --- */

    // Create Room
    public void OnClick_CreateRoom()
    {
        if (roomName.text != "" && playerName.text != "")
        {
            PhotonNetwork.LocalPlayer.NickName = playerName.text;
            PhotonNetwork.CreateRoom(roomName.text);
            Debug.Log(DebugTag + playerName.text + "has created the room: " + roomName.text);
        }
        else { Debug.Log(DebugTag + "Room does not have a name"); }
    }

    // Join Room
    public void OnClick_JoinRoom()
    {
        if (roomName.text != "" && playerName.text != "")
        {
            PhotonNetwork.LocalPlayer.NickName = playerName.text;
            PhotonNetwork.JoinRoom(roomName.text);
            Debug.Log(DebugTag + "Room or player does not have a name");
        }
    }

    // Play Game
    public void OnClick_Play()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(1);
            Debug.Log(DebugTag + "Beginning game");
        }
    }

}
