using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

using Database.Score;
using SaveLoad;

namespace MainMenu.Options
{
    public class LevelChooser : MonoBehaviour
    {
        [SerializeField]
        private Database.AI.BotLevelsData levelsData;

        public void SelectLevelDifficulty(int level)
        {
            levelsData.ChoosedLevel = level;
            SceneManager.LoadScene("Game");
        }
    }
}
