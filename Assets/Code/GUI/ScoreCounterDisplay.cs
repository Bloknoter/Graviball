using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using GameEngine;

namespace GameGUI.GameEngine
{
    public class ScoreCounterDisplay : MonoBehaviour
    {
        [SerializeField]
        private ScoreCounter scoreCounter;

        [Space(10)]
        [Header("GUI Elements")]
        [SerializeField]
        private Text RedTeamScoreT;

        [SerializeField]
        private Text GreenTeamScoreT;

        [SerializeField]
        private TextMeshProUGUI PlayerScoreT;

        private void Start()
        {
            scoreCounter.OnScoreofRedTeamChangedEvent += OnRedTeamScoreChanged;
            scoreCounter.OnScoreofGreenTeamChangedEvent += OnGreenTeamScoreChanged;
            scoreCounter.OnPlayerPointsChanged += OnPlayerScoreChanged;

            RedTeamScoreT.text = scoreCounter.RedTeamScore.ToString();
            GreenTeamScoreT.text = scoreCounter.GreenTeamScore.ToString();
            PlayerScoreT.text = scoreCounter.CalculatePlayerScore().ToString();
        }

        private void OnRedTeamScoreChanged(int newscore)
        {
            RedTeamScoreT.text = newscore.ToString();
        }

        private void OnGreenTeamScoreChanged(int newscore)
        {
            GreenTeamScoreT.text = newscore.ToString();
        }

        private void OnPlayerScoreChanged(int newscore)
        {
            PlayerScoreT.text = newscore.ToString();
        }

        private void OnDisable()
        {
            scoreCounter.OnScoreofRedTeamChangedEvent -= OnRedTeamScoreChanged;
            scoreCounter.OnScoreofGreenTeamChangedEvent -= OnGreenTeamScoreChanged;
            scoreCounter.OnPlayerPointsChanged -= OnPlayerScoreChanged;
        }
    }
}
