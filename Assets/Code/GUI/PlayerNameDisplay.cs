using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace GameGUI
{
    public class PlayerNameDisplay : MonoBehaviour
    {
        [SerializeField]
        private Player.PlayerSessionData m_sessionData;

        [SerializeField]
        private TextMeshProUGUI m_nameText;

        [SerializeField]
        private string m_offlineText;

        [SerializeField]
        private Color m_online;

        [SerializeField]
        private Color m_offline;

        private void Start()
        {
            m_sessionData.OnConnected += OnConnected;
            ShowName(m_sessionData.Connected);
        }

        private void ShowName(bool connected)
        {
            if (!connected)
            {
                m_nameText.color = m_offline;
                m_nameText.text = m_offlineText;
                m_nameText.fontStyle = FontStyles.Italic | FontStyles.Bold;
                return;
            }

            m_nameText.color = m_online;
            m_nameText.text = m_sessionData.Name;
            m_nameText.fontStyle = FontStyles.Bold;
        }

        private void OnConnected()
        {
            m_sessionData.OnConnected -= OnConnected;
            ShowName(m_sessionData.Connected);
        }

        private void OnDisable()
        {
            m_sessionData.OnConnected -= OnConnected;
        }
    }
}
