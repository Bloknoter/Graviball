using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameEngine;
using Database.Score;

namespace GameGUI.GameEngine
{
    public class FinalResultsDisplay : MonoBehaviour
    {
        [SerializeField]
        private SaveLoad.ScoreSave scoreSave;

        [SerializeField]
        private GameObject HighScore;

        void Start()
        {
            HighScore.SetActive(false);
            scoreSave.OnNewHighScore += OnNewHighScore;
        }

        void Update()
        {

        }

        private void OnNewHighScore()
        {
            HighScore.SetActive(true);
        }
    }
}
