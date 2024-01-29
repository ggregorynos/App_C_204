using System;
using UnityEngine;

namespace CodeHub.OtherUtilities
{
    public class GameTimer : MonoBehaviour
    {
        private float _secondsFromStart;
        private bool _isPause;

        public float SecondsFromStart => _secondsFromStart;

        public void Initialize()
        {
            _secondsFromStart = 0;
            StartTimer();
        }

        private void Update()
        {
            CoreTimer();
        }

        public string GetStringVisualizeTime(float time)
        {
            return TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
        }

        public void PauseTimer()
        {
            _isPause = true;
        }

        public void StartTimer()
        {
            _isPause = false;
        }

        private void CoreTimer()
        {
            if (_isPause == false)
            {
                _secondsFromStart += Time.deltaTime;
            }
        }
    }
}