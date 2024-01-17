using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameEngine;

namespace MainMenu
{
    public class MenuKickoff : MonoBehaviour
    {
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


        private void Start()
        {
            scoreCounter.OnScoreofRedTeamChangedEvent += OnScoreChanged;
            scoreCounter.OnScoreofGreenTeamChangedEvent += OnScoreChanged;
            StartKickoff();
        }

        private void StartKickoff()
        {
            ball.Deactivate();
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

            ball.transform.position = KickoffBallPoint.position;
            ball.Appear();

            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown()
        {
            yield return new WaitForSecondsRealtime(1f);

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
            StartCoroutine(AfterGoalWaiting());
        }

        private IEnumerator AfterGoalWaiting()
        {
            yield return new WaitForSecondsRealtime(1f);
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
