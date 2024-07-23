using System;
using System.Globalization;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class GeneralCanvas : MonoBehaviourPunCallbacks, IPunObservable
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private GameTimer _timer;
        
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (!stream.IsWriting) return;
                int min = (int)(_timer.CurrentTime / 60);
                var sec = $"{_timer.CurrentTime % 60}";
                string text = $"{min} : {sec}";
                _timerText.text = text;
                stream.SendNext(text);
            }
            else
            {
                if (stream.IsReading)
                {
                    _timerText.text = (string)stream.ReceiveNext();
                }
            }
        }
    }
}