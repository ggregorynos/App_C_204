using System;
using UnityEngine;

namespace CodeHub.GameMechanics
{
    public class Timer
    {
        public float Duration { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsPaused { get; set; }
        public Action OnComplete { get; private set; }

        public float ElapsedTime { get; set; }

        public Timer(float duration, Action onComplete)
        {
            Duration = duration;
            OnComplete = onComplete;
        }

        public void StartTimer()
        {
            IsRunning = true;
            IsPaused = false;
        }

        public void StopTimer()
        {
            IsRunning = false;
            IsPaused = false;
            ElapsedTime = 0.0f;
            OnComplete = null;
        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
        }

        public void Update()
        {
            if (!IsRunning || IsPaused)
            {
                return;
            }


            if (ElapsedTime < Duration)
            {
                ElapsedTime += Time.deltaTime;
            }

            else
            {
                IsRunning = false;
                OnComplete?.Invoke();
            }
        }
    }
}