using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitHandler : MonoBehaviour
{
    [SerializeField] private GameObject _exitCanvas;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _exitCanvas.SetActive(!_exitCanvas.activeInHierarchy);
        }
    }
}
