using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
   
    public TMP_InputField nameInput;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(nameInput.text, new RoomOptions(){MaxPlayers = 8, BroadcastPropsChangeToAll = true});
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(nameInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameSceneMultiplayer");
    }

    public void GoBack()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("Garage");
        }
    }
    
}
