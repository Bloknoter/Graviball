using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GameEngine;

namespace GUI.GameEngine
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

        void Start()
        {
            scoreCounter.OnScoreofRedTeamChangedEvent += OnRedTeamScoreChanged;
            scoreCounter.OnScoreofGreenTeamChangedEvent += OnGreenTeamScoreChanged;

            RedTeamScoreT.text = scoreCounter.RedTeamScore.ToString();
            GreenTeamScoreT.text = scoreCounter.GreenTeamScore.ToString();
        }

        void Update()
        {

        }

        private void OnRedTeamScoreChanged(int newscore)
        {
            RedTeamScoreT.text = newscore.ToString();
        }

        private void OnGreenTeamScoreChanged(int newscore)
        {
            GreenTeamScoreT.text = newscore.ToString();
        }
    }
}
