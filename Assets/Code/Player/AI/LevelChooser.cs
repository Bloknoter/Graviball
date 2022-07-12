using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Database.Score;

namespace MainMenu.Options
{
    public class LevelChooser : MonoBehaviour
    {
        [SerializeField]
        private Database.AI.BotLevelsData levelsData;

        [SerializeField]
        private ScoreData scoreData;

        [SerializeField]
        private Image[] LevelButtonsBGs;

        [SerializeField]
        private Color OpenedColorBG;

        [SerializeField]
        private Color ClosedColorBG;

        void Start()
        {
            for (int i = 0; i < LevelButtonsBGs.Length; i++)
            {
                if (i == 0)
                    LevelButtonsBGs[i].color = OpenedColorBG;
                else
                {
                    LevelData previoslevelData = scoreData.GetLevelData(i - 1);
                    if (previoslevelData.isPlayed && previoslevelData.GreenTeamScore > previoslevelData.RedTeamScore)
                    {
                        LevelButtonsBGs[i].color = OpenedColorBG;
                    }
                    else
                    {
                        LevelButtonsBGs[i].color = ClosedColorBG;
                    }
                }
            }
        }

        void Update()
        {

        }

        public void SelectLevelDifficulty(int level)
        {
            if (level == 0 || scoreData.GetLevelData(level - 1).isPlayed &&
                scoreData.GetLevelData(level - 1).GreenTeamScore > scoreData.GetLevelData(level - 1).RedTeamScore)
            {
                levelsData.ChoosedLevel = level;
                SceneManager.LoadScene("Game");
            }
        }
    }
}
