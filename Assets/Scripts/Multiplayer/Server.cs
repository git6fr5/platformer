using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Server : MonoBehaviour
{

    public Text count;
    public Text playerNames;
    private float serverTickInterval = 0.5f;
    private float serverDeltaTime = 0f;

    //private List<PlayerAction> playerActions;

    /* --- Unity Methods --- */
    void Start()
    {
        Debug.Log("Started");
    }

    void Update()
    {
        serverDeltaTime = serverDeltaTime + Time.deltaTime;
        
        if (serverDeltaTime > serverTickInterval)
        {
            UpdateServer();
            serverDeltaTime = 0f;
        }

    }

    /* --- Methods --- */
    void UpdateServer()
    {
        count.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();

        string playerNameText = "";
        for (int i = 1; i < PhotonNetwork.CurrentRoom.PlayerCount + 1; i++)
        {
            playerNameText = playerNameText + PhotonNetwork.CurrentRoom.Players[i].NickName + ", " + i.ToString() + "\n";
        }

        playerNames.text = playerNameText;

    }

}
