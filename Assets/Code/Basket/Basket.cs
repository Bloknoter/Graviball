using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameEngine
{
    public class Basket : MonoBehaviour
    {
        [SerializeField]
        private ScoreCounter scoreCounter;

        public enum TeamEnum
        {
            Red,
            Green
        }

        [SerializeField]
        private TeamEnum AddScoreTo;

        public void Score()
        {
            if(AddScoreTo == TeamEnum.Red)
            {
                scoreCounter.AddScoreToRedTeam();
            }
            else if(AddScoreTo == TeamEnum.Green)
            {
                scoreCounter.AddScoreToGreenTeam();
            }
        }
    }
}
