using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private CarControllerMultiplayer playerPrefab;
    [SerializeField] private List<Transform> spawnPositions;
    public List<GameObject> SpawnedPlayers { get; private set; } = new List<GameObject>();


    public CarControllerMultiplayer SpawnPlayer()
    {
        Debug.Log("Spawned");
        var place = spawnPositions[Random.Range(0, spawnPositions.Count)];
        spawnPositions.Remove(place);
        var player = PhotonNetwork.Instantiate(playerPrefab.name, place.position, Quaternion.identity);
        var controller = player.GetComponent<CarControllerMultiplayer>();
        controller.Enabled = false;
        return controller;
    }
}
