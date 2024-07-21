using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField] private SpawnPlayers _spawner;
    [SerializeField] private TextMeshProUGUI _textPrefab;
    private List<TextMeshProUGUI> _names = new List<TextMeshProUGUI>();
    [SerializeField] private Transform _namesParent;
    [SerializeField] private GameObject _startGameButton;
    private List<CarControllerMultiplayer> _players = new List<CarControllerMultiplayer>();

    private CarControllerMultiplayer _myPlayer;
    
    // Start is called before the first frame update 
    void Start()
    {
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                RefreshPlayers();
                _myPlayer = _spawner.SpawnPlayer();
                _players.Add(_myPlayer);
                _myPlayer.CameraEnabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        RaiseEventOptions receive = new RaiseEventOptions() { Receivers = ReceiverGroup.All };
        SendOptions send = new SendOptions() { Reliability = true };
        PhotonNetwork.RaiseEvent(100, true, receive, send);
        
        _startGameButton.SetActive(false);
   }

    public void StartGameHandle()
    {
        _myPlayer.Enabled = true;
        _startGameButton.transform.parent.gameObject.SetActive(false);
        _myPlayer.LocalCanvasEnabled = true;
        _myPlayer.AudioListenerEnabled = true;
    }
    
    

    public void LeaveGame()
    {
        PhotonNetwork.LeaveRoom();
    }
    
    public override void OnLeftRoom()
    {
        PhotonNetwork.Destroy(_myPlayer.gameObject);
        SceneManager.LoadScene("Lobby");
    }

    private void RefreshPlayers()
    {
        foreach (var name in _names)
        {
            Destroy(name);
        }
        _names.Clear();
        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            var newName = Instantiate(_textPrefab, _namesParent);
            newName.text = player.Value.NickName;
            _names.Add(newName);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RefreshPlayers();
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            _startGameButton.SetActive(true);
        }
        else
        {
            _startGameButton.SetActive(false);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RefreshPlayers();
        foreach (var player in _spawner.SpawnedPlayers)
        {
            //if(otherPlayer.)
        }
    }


    public void OnEvent(EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case 100:
                StartGameHandle();
                break;
        }
    }
}
