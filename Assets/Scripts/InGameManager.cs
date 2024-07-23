using System.Collections;
using System.Collections.Generic;
using System.Timers;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = System.Collections.Hashtable;

public class InGameManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField] private SpawnPlayers _spawner;
    [SerializeField] private TextMeshProUGUI _textPrefab;
    private List<TextMeshProUGUI> _names = new List<TextMeshProUGUI>();
    [SerializeField] private Transform _namesParent;
    [SerializeField] private GameObject _startGameButton;
    private List<CarControllerMultiplayer> _players = new List<CarControllerMultiplayer>();
    [SerializeField] private GameTimer _timer;

    private CarControllerMultiplayer _myPlayer;
    
    // Start is called before the first frame update 
    void Start()
    {
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                RefreshPlayers();
                ExitGames.Client.Photon.Hashtable hashProperties = new ExitGames.Client.Photon.Hashtable();
                foreach (var pair in player.Value.CustomProperties)
                {
                    hashProperties.Add(pair.Key, pair.Value);
                }
                _myPlayer = _spawner.SpawnPlayer(hashProperties, PhotonNetwork.CurrentRoom.PlayerCount-1);
                _myPlayer.View.RPC("SetCarPartsRPC", RpcTarget.All, hashProperties);
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
        PhotonNetwork.CurrentRoom.IsOpen = false;
        _timer.StartCounting();
        PhotonNetwork.RaiseEvent(100, true, receive, send);

        _timer.OnTimerEnded += EndGame;
        
        _startGameButton.SetActive(false);
   }

    private void EndGame()
    {
        RaiseEventOptions receive = new RaiseEventOptions() { Receivers = ReceiverGroup.All };
        SendOptions send = new SendOptions() { Reliability = true };
        PhotonNetwork.RaiseEvent(199, true, receive, send);
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
        //var i = (int)photonEvent.CustomData;
        switch (photonEvent.Code)
        {
            case 100:
                StartGameHandle();
                break;
            case 199:
                GameEndHandle();
                break;
            
        }
    }

    private void GameEndHandle()
    {
        _myPlayer.Enabled = false;
        _myPlayer.ShowEndCanvas();
    }
    
}
