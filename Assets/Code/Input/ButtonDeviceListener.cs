using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InputEngine
{
    public abstract class ButtonDeviceListener : MonoBehaviour
    {
        private List<ButtonListener> buttonListeners = new List<ButtonListener>();

        void Start()
        {

        }

        protected void Update()
        {
            for (int i = 0; i < buttonListeners.Count; i++)
            {
                buttonListeners[i].CheckInput();
            }
        }

        public ButtonListener FindButtonListener(string Name)
        {
            for (int i = 0; i < buttonListeners.Count; i++)
            {
                if (buttonListeners[i].Name == Name)
                {
                    return buttonListeners[i];
                }
            }
            throw new System.Exception($"You are trying to find key named '{Name}', but it doesn't exist");
        }

        protected void AddButtonListener(ButtonListener buttonListener)
        {
            for(int i = 0; i < buttonListeners.Count;i++)
            {
                if(buttonListeners[i].Name == buttonListener.Name)
                {
                    return;
                }
            }
            buttonListeners.Add(buttonListener);
        }
    }
}
