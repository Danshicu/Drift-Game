using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GarageHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject[] _cars;
    void Start()
    {
        SpawnCar();
        SetMoneyText();
    }

    private void SetMoneyText()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            _coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("Coins", 0);
            _coinsText.text = "0";
        } 
    }

    private void SpawnCar()
    {
        if (PlayerPrefs.HasKey("ChosenCar"))
        {
            int index = PlayerPrefs.GetInt("ChosenCar");
            GameObject.Instantiate(_cars[index], _spawnPoint);
        }
        else
        {
            GameObject.Instantiate(_cars[0], _spawnPoint);
        }
    }
}
