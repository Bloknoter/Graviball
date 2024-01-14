using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameEngine
{
    public class GameEnd : MonoBehaviour
    {
        [SerializeField]
        private Timer timer;

        [SerializeField]
        private Ball ball;

        [SerializeField]
        private AudioSource gameoverSoundSource;

        [SerializeField]
        private MenuEngine.MenuController menuController;

        [SerializeField]
        private Player.PlayerController[] allPlayers;

        private void Start()
        {
            timer.OnTimeRunOut += OnTimeRunOut;
        }

        private void OnTimeRunOut()
        {
            ball.Deactivate();
            for (int i = 0; i < allPlayers.Length; i++)
            {
                allPlayers[i].Disable();
            }
            gameoverSoundSource.Play();
            menuController.SetPage("GameOver");
        }
    }
}
