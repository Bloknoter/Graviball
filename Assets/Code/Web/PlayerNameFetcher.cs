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
            GetPlayerName();
        }

        private void GetPlayerName()
        {
            m_sessionData.Name = WebBridge.GetValue(PLAYERNAME_KEY);
            m_sessionData.SetAsConnected();
            WebBridge.ShowMessageBox("If you see this, that means plugin is working!");
        }
    }
}
