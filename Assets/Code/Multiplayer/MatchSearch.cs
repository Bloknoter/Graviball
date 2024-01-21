using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Multiplayer
{
    public class MatchSearch : MonoBehaviour
    {
        private bool m_isSearching;

        public bool IsSearching => m_isSearching;

        public void StartSearch()
        {
            m_isSearching = true;
        }

        public void StopSearch()
        {
            m_isSearching = false;
        }
    }
}
