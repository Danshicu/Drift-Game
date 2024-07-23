using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private int _timeInSeconds;
    public float CurrentTime { get; private set; }
    private bool _isGoing;

    public event Action OnTimerEnded;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartCounting()
    {
        CurrentTime = _timeInSeconds;
        _isGoing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGoing)
        {
            CurrentTime -= Time.deltaTime;
            if (!(CurrentTime <= 0.01f)) return;
            CurrentTime = 0;
            OnTimerEnded?.Invoke();
            _isGoing = false;
        }
    }
}
