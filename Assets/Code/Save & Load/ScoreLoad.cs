using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Database;
using Database.Score;


namespace SaveLoad
{
    public class ScoreLoad : MonoBehaviour
    {
        [SerializeField]
        private SaveFileInfo fileInfo;

        [SerializeField]
        private ScoreData scoreData;

        private void OnEnable()
        {
            if (File.Exists(fileInfo.FilePath))
            {
                FileStream stream = new FileStream(fileInfo.FilePath, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                LevelData[] data = (LevelData[])formatter.Deserialize(stream);
                stream.Close();
                for(int i = 0; i < data.Length;i++)
                {
                    scoreData.GetLevelData(i).RedTeamScore = data[i].RedTeamScore;
                    scoreData.GetLevelData(i).GreenTeamScore = data[i].GreenTeamScore;
                    scoreData.GetLevelData(i).isPlayed = data[i].isPlayed;
                }
            }
        }
    }
}
