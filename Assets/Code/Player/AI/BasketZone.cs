using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameEngine.AI
{
    public class BasketZone : MonoBehaviour
    {
        public enum SideEnum
        {
            Left,
            Right
        }

        [SerializeField]
        private SideEnum basketSide;

        public SideEnum BasketSide
        {
            get
            {
                return basketSide;
            }
        }
    }
}
