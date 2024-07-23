using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PhotonView))]
public class SpawnPlayers : MonoBehaviour//, IPunObservable
{
    private PhotonView _view;
    [SerializeField] private CarControllerMultiplayer[] playerPrefabs;
    [SerializeField] private List<Transform> spawnPositions;
    public List<GameObject> SpawnedPlayers { get; private set; } = new List<GameObject>();

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
    }

    public CarControllerMultiplayer SpawnPlayer(ExitGames.Client.Photon.Hashtable hash, int placeIndex)
    {
        var place = spawnPositions[placeIndex];
        var carIndex = hash["ChosenCar"];
        hash.Remove("ChosenCar");
        var player = PhotonNetwork.Instantiate(playerPrefabs[(int)carIndex].name, place.position, Quaternion.identity);
        var controller = player.GetComponent<CarControllerMultiplayer>();
        controller.Enabled = false;
        var manager = controller.GetComponent<PartsManager>();
        manager.SetPartsFromHashtable(hash);
        return controller;
    }

/*
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            spawnPositions.Clear();
        }
        if (stream.IsWriting)
        {
            stream.SendNext(spawnPositions.Count);
            foreach (var position in spawnPositions)
            {
                stream.SendNext(position);
            }
        }
        else
        {
            int count = (int)stream.ReceiveNext();
            while (spawnPositions.Count < count)
            {
                spawnPositions.Add((Transform)stream.ReceiveNext());
            }
        }
    }*/
}
