using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameEngine
{
    public class Timer : MonoBehaviour
    {
        public delegate void OnTimeRunOutDelegate();

        public event OnTimeRunOutDelegate OnTimeRunOut;

        [SerializeField]
        private Text TimerT;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        [Min(0)]
        private float StartTime;

        [SerializeField]
        private Color TimeFinishedColor;

        private float timeleft;

        public float TimeLeft
        {
            get
            {
                return timeleft;
            }
        }

        private bool isStopped;

        void Start()
        {
            timeleft = StartTime;
            TimerT.text = GetStringTime(timeleft);
        }

        private bool wasStarted;

        void Update()
        {
            if (!wasStarted)
            {
                wasStarted = true;
                StartTimer();
            }
        }

        public void StartTimer()
        {
            StopAllCoroutines();
            StartCoroutine(TimerCount());
        }

        public void Stop()
        {
            isStopped = true;
        }

        public void Continue()
        {
            isStopped = false;
        }

        private IEnumerator TimerCount()
        {
            int iteration = (int)StartTime;
            for (int i = 0; i < iteration; i++)
            {
                if (!isStopped)
                {
                    timeleft = iteration - i;
                    TimerT.text = GetStringTime(timeleft); 
                }
                else
                {
                    i--;
                }
                yield return new WaitForSeconds(1f * Kickoff.TIME_SCALE);
            }
            OnTimeRunOut?.Invoke();
            timeleft = 0;
            TimerT.text = "0:00";
            TimerT.color = TimeFinishedColor;
            animator.SetInteger("state", 1);
        }

        private string GetStringTime(float time)
        {
            string text = "";
            int minutes = (int)(time / 60f);
            text += $"{minutes}:";

            int seconds = ((int)time) - (minutes * 60);
            if (seconds >= 10)
                text += seconds.ToString();
            else
                text += $"0{seconds}";

            return text;
        }
    }
}
