using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(CarController))]
public class CarControllerMultiplayer : MonoBehaviour
{
    private PhotonView view;
    private CarController carController;
    
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
}

