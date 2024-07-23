using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(CarController))]
public class CarControllerMultiplayer : MonoBehaviourPunCallbacks
{
    [SerializeField] private AudioListener _listener;
    [SerializeField] private GameObject _localCanvas;
    [SerializeField] private GameObject _camera;
    [SerializeField] private DriftManager _manager;
    [SerializeField] private GameObject _gameEndCanvas;
    [SerializeField] private TextMeshProUGUI _coinsEarnedField;
    [SerializeField] private GameObject _exitGameCanvas;
    public PhotonView View { get; private set; }
    public CarController carController { get; private set; }
    
    void Awake()
    {
        View = GetComponent<PhotonView>();
        carController = GetComponent<CarController>();
        if (!View.IsMine)
        {
            carController.enabled = false;
        }
        
    }

    private void Update()
    {
        if (!View.IsMine) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Enabled = !Enabled;
            _exitGameCanvas.SetActive(!_exitGameCanvas.activeInHierarchy);
        }
    }

    private bool _enabled;
    public bool Enabled
    {
        get => _enabled;
        set
        {
            carController.enabled = value;
            _enabled = value;
        } 
    }

    public bool CameraEnabled
    {
        get => _camera.activeSelf;
        set => _camera.SetActive(value);
    }
    
    public bool LocalCanvasEnabled
    {
        get => _localCanvas.activeSelf;
        set => _localCanvas.SetActive(value);
    }

    public bool AudioListenerEnabled
    {
        get => _listener.enabled;
        set => _listener.enabled = value;
    }

    public void ExitGame()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void ShowEndCanvas()
    {
        _gameEndCanvas.SetActive(true);
        _coinsEarnedField.text = $"You earned {_manager.GetMoneyFromPoints()} coins";
        var currentCoins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", currentCoins+_manager.GetMoneyFromPoints());
    }

    [PunRPC]
    void SetCarPartsRPC(ExitGames.Client.Photon.Hashtable hash)
    {
        GetComponent<PartsManager>().SetPartsFromHashtable(hash);
    }
    
}

