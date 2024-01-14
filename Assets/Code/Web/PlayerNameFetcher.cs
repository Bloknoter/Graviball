using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WebCommunication
{
    public class PlayerNameFetcher : MonoBehaviour
    {
        public const string PLAYERNAME_KEY = "playername";

        [SerializeField]
        private GameSocket m_socket;

        [SerializeField]
        private PlayerSessionData m_sessionData;

        private void Awake()
        {
            m_socket.OnTextWithKeyReceived.AddListener(OnTextWithKeyReceived);
        }

        private void OnTextWithKeyReceived(TextWithKey data)
        {
            if (data.key != PLAYERNAME_KEY)
                return;

            m_sessionData.Name = data.text;
            m_sessionData.SetAsConnected();
        }
    }
}
