using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Database.AI
{
    [CreateAssetMenu(fileName = "BotLevelsData", menuName = "Bot levels data", order = 0)]
    public class BotLevelsData : ScriptableObject
    {
        [SerializeField]
        private LevelData[] levelDatas;

        private int choosedLevel = 0;

        public int ChoosedLevel
        {
            get { return choosedLevel; }
            set
            {
                if(value >= 0 && value < levelDatas.Length)
                {
                    choosedLevel = value;
                }
            }
        }

        public LevelData ChoosedLevelData
        {
            get
            {
                return levelDatas[choosedLevel];
            }
        }
    }

    [System.Serializable]
    public class LevelData
    {
        [SerializeField]
        private int speed;

        public int Speed
        {
            get { return speed; }
        }

        [SerializeField]
        private int jumpForce;

        public int JumpForce
        {
            get { return jumpForce; }
        }
    }
}
