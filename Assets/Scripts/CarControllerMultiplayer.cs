using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(CarController))]
public class CarControllerMultiplayer : MonoBehaviour
{
    [SerializeField] private AudioListener _listener;
    [SerializeField] private GameObject _localCanvas;
    [SerializeField] private GameObject _camera;
    private PhotonView view;
    public CarController carController { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        view = GetComponent<PhotonView>();
        carController = GetComponent<CarController>();
        if (!view.IsMine)
        {
            carController.enabled = false;
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
}

