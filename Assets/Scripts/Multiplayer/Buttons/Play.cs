using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Play : Button2D
{
    public override void Press() {
        string nickName = menu.SetPlayerName();
        string roomID = menu.SetRoomID();
        menu.JoinRoom(roomID);       
    }
}
