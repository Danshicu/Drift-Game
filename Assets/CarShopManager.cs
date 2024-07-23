using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarShopManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _carCostField;
    [SerializeField] private GameObject[] _cars;
    [SerializeField] private int[] _carCosts;

    private int _shownIndex = 0;
    private int _chosenIndex =0;

    public void SwitchCar(int value)
    {
        _cars[_shownIndex].SetActive(false);
        int index = _shownIndex + value;
        if (index == _cars.Length)
        {
            index = 0;
        }

        if (index == -1)
        {
            index = _cars.Length - 1;
        }
        _cars[index].SetActive(true);
        _shownIndex = index;
        
        _carCostField.text = PlayerPrefs.HasKey(_cars[_shownIndex].name) ? "Select" : _carCosts[_shownIndex].ToString();
        if (PlayerPrefs.GetInt("ChosenCar") == _shownIndex)
        {
            _carCostField.text = "Selected";
        }
    }
    
    
    
    void Start()
    {
        if (PlayerPrefs.HasKey("ChosenCar"))
        {
            int index = PlayerPrefs.GetInt("ChosenCar");
            _chosenIndex = index;
            _shownIndex = index;
            _cars[index].SetActive(true);
            _carCostField.text = "Selected";
        }
        else
        {
            _cars[0].SetActive(true);
            _carCostField.text = "Selected";
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("ChosenCar", _chosenIndex);
        PlayerPrefs.SetString(_cars[_chosenIndex].name, "Open");
    }

    public void ActivateCar()
    {
        if (PlayerPrefs.HasKey(_cars[_shownIndex].name))
        {
            _chosenIndex = _shownIndex;
            _carCostField.text = "Selected";
        }
        var playerCoins = PlayerPrefs.GetInt("Coins");
        if (_carCosts[_shownIndex] <= playerCoins)
        {
            playerCoins -= _carCosts[_shownIndex];
            PlayerPrefs.SetInt("Coins", playerCoins);
            PlayerPrefs.SetString(_cars[_shownIndex].name, "Open");
        }
    }
    
}
