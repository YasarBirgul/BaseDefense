using System;
using Signals;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    { 
        private void Awake()
        {
            Application.targetFrameRate = 60;
            GameOpen();
        } 
        private void Start()
        {
            ReadyToPlay();
        }
        private void OnApplicationPause(bool IsPaused)
        {
            if (IsPaused) CoreGameSignals.Instance.onApplicationPause?.Invoke();
        }
        private void GameOpen()
        {
            CoreGameSignals.Instance.onGameOpen?.Invoke();
        }
        private void ReadyToPlay()
        {
            CoreGameSignals.Instance.onReadyToPlay?.Invoke();
        }
        private void OnApplicationQuit()
        {
            CoreGameSignals.Instance.onApplicationQuit?.Invoke();
        }
    }
}