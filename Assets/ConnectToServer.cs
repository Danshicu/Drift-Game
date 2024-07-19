using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI ConnectText;
    public TMP_InputField UserNameInput;
    
    public void ConnectToMaster()
    {
        if (UserNameInput.text.Length >= 1)
        {
            PhotonNetwork.NickName = UserNameInput.text;
            ConnectText.text = "Finding host...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        SceneManager.LoadScene("Lobby");
    }
    
}
