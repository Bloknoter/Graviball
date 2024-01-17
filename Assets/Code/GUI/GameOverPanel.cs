using SaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameGUI
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField]
        private ScoreSave m_scoreSave;

        [SerializeField]
        private GameObject m_savingInformationPanel;

        [SerializeField]
        private GameObject m_controlsPanel;   

        private void Awake()
        {
            m_scoreSave.OnScoreSaved += OnScoreSaved;
            m_savingInformationPanel.SetActive(true);
            m_controlsPanel.SetActive(false);
        }

        private void OnScoreSaved(bool succeed)
        {
            m_savingInformationPanel.SetActive(false);
            m_controlsPanel.SetActive(true);
            m_scoreSave.OnScoreSaved -= OnScoreSaved;
        }

        private void OnDisable()
        {
            m_scoreSave.OnScoreSaved -= OnScoreSaved;
        }
    }
}
