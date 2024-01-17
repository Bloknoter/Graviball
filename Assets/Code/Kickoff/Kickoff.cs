using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameEngine
{
    public class Kickoff : MonoBehaviour
    {
        public const float TIME_SCALE = 0.75f;

        [Header("Players' info")]
        [SerializeField]
        private PlayerInfo[] RedTeamPlayers;

        [SerializeField]
        private PlayerInfo[] GreenTeamPlayers;

        [Space(10)]
        [Header("Kickoff starts")]
        [SerializeField]
        private Transform[] RedTeamKickoffStarts;

        [SerializeField]
        private Transform[] GreenTeamKickoffStarts;

        [SerializeField]
        private Transform KickoffBallPoint;

        [Space(10)]
        [Header("Other scripts")]
        [SerializeField]
        private Ball ball;

        [SerializeField]
        private ScoreCounter scoreCounter;

        [SerializeField]
        private Timer timer;

        [Space(10)]
        [Header("Audio")]
        [SerializeField]
        private AudioSource goalSoundSource;

        [SerializeField]
        private AudioSource fansSoundSource;

        [SerializeField]
        private AudioSource countdownSoundSource;

        [Space(10)]
        [Header("GUI")]
        [SerializeField]
        private Text CountdownT;

        [Space(10)]
        [Header("Animators")]
        [SerializeField]
        private Animator countdownAnimator;

        private bool wasStart;

        private void Start()
        {
            Time.timeScale = 1;
            CountdownT.gameObject.SetActive(false);
            scoreCounter.OnScoreofRedTeamChangedEvent += OnScoreChanged;
            scoreCounter.OnScoreofGreenTeamChangedEvent += OnScoreChanged;
            timer.Stop();
            ball.Deactivate();
        }

        private void Update()
        {
            if(!wasStart)
            {
                wasStart = true;
                StartCoroutine(GameStarting());
            }
        }

        private IEnumerator GameStarting()
        {
            ReturnPlayersToKickoff();
            yield return new WaitForSecondsRealtime(2f);
            StartKickoff();
        }

        private void ReturnPlayersToKickoff()
        {
            for (int i = 0; i < RedTeamPlayers.Length; i++)
            {
                RedTeamPlayers[i].Controller.Disable();
                RedTeamPlayers[i].PlayerTransform.position = RedTeamKickoffStarts[i].position;
            }

            for (int i = 0; i < GreenTeamPlayers.Length; i++)
            {
                GreenTeamPlayers[i].Controller.Disable();
                GreenTeamPlayers[i].PlayerTransform.position = GreenTeamKickoffStarts[i].position;
            }
        }

        private void StartKickoff()
        {
            Time.timeScale = 1f;
            timer.Stop();
            ball.Deactivate();
            ReturnPlayersToKickoff();

            ball.transform.position = KickoffBallPoint.position;
            ball.Appear();

            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown()
        {
            CountdownT.gameObject.SetActive(true);
            countdownAnimator.SetTrigger("count");
            countdownSoundSource.Play();
            for(int i = 0; i < 3;i++)
            {
                CountdownT.text = $"{3 - i}";
                yield return new WaitForSecondsRealtime(1f);
            }
            CountdownT.text = "PLAY!";
            timer.Continue();
            yield return new WaitForSecondsRealtime(0.5f);
            CountdownT.gameObject.SetActive(false);
            Time.timeScale = TIME_SCALE;

            ball.Activate();

            for (int i = 0; i < RedTeamPlayers.Length; i++)
            {
                RedTeamPlayers[i].Controller.Enable();
            }

            for (int i = 0; i < GreenTeamPlayers.Length; i++)
            {
                GreenTeamPlayers[i].Controller.Enable();
            }
        }

        private void OnScoreChanged(int newscore)
        {
            Time.timeScale = 1;
            timer.Stop();
            fansSoundSource.Play();
            goalSoundSource.Play();
            StartCoroutine(AfterGoalWaiting());
        }

        private IEnumerator AfterGoalWaiting()
        {
            yield return new WaitForSecondsRealtime(3f);
            StartKickoff();
        }

        private void OnDisable()
        {
            scoreCounter.OnScoreofRedTeamChangedEvent -= OnScoreChanged;
            scoreCounter.OnScoreofGreenTeamChangedEvent -= OnScoreChanged;
        }
    }

    [System.Serializable]
    public class PlayerInfo
    {
        public Transform PlayerTransform;
        public Player.PlayerController Controller;
    }
}
