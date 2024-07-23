using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI ConnectText;
    [SerializeField] private TMP_InputField UserNameInput;
    [SerializeField] private GameObject inputNamePanel;

    private void Awake()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
    }

    public void SetNameAndConnect()
    {
        if (UserNameInput.text.Length >= 1)
        {
            PhotonNetwork.NickName = UserNameInput.text;
            PlayerPrefs.SetString("Nickname", UserNameInput.text);
            ConnectText.text = "Finding host...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    
    public void ConnectToMaster()
    {
        if (PlayerPrefs.HasKey("Nickname"))
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("Nickname");
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            inputNamePanel.SetActive(true);  
        }
    }

    public override void OnConnectedToMaster()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
            SceneManager.LoadScene("Lobby");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
