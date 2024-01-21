using Multiplayer;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace GameGUI
{
    public class MatchmakingDisplay : MonoBehaviour
    {
        [SerializeField]
        private MatchSearch m_matchSearch;

        [SerializeField]
        private Button m_findMatchButton;

        [SerializeField]
        private TMP_InputField m_playerNameInputField;

        [SerializeField]
        private GameObject m_loading;

        [SerializeField]
        private CanvasGroup m_notFound;

        [SerializeField]
        private CanvasGroup m_found;

        [SerializeField]
        private TextMeshProUGUI m_opponentName;

        private string m_playerName;

        private void OnEnable()
        {
            m_findMatchButton.gameObject.SetActive(true);
            m_loading.SetActive(false);
            m_playerName = m_playerNameInputField.text;
            m_playerNameInputField.interactable = true;

            UpdateFindMatchState();
        }

        public void OnNameChanged(string name)
        {
            m_playerName = name;
            UpdateFindMatchState();
        }

        private void UpdateFindMatchState()
        {
            if(string.IsNullOrEmpty(m_playerName))
            {
                m_findMatchButton.interactable = false;
            }
            else
            {
                m_findMatchButton.interactable = true;
            }
        }

        public void OnMatchmakingClick()
        {
            m_findMatchButton.gameObject.SetActive(false);
            m_playerNameInputField.interactable = false;
            m_loading.SetActive(true);

            m_matchSearch.StartSearch();
        }


        private void OnDisable()
        {
            m_playerNameInputField.onValueChanged.RemoveListener(OnNameChanged);
            m_matchSearch.StopSearch();
        }
    }
}
