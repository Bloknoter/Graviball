using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InputEngine
{
    public abstract class ButtonListener
    {
        public ButtonListener(string _Name)
        {
            name = _Name;
        }

        private string name;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public delegate void OnButtonCallback();

        private event OnButtonCallback OnButtonDown;
        protected void InvokeOnButtonDown()
        {
            OnButtonDown?.Invoke();
        }
        public void AddKeyDownListener(OnButtonCallback callback)
        {
            OnButtonDown += callback;

        }
        public void RemoveKeyDownListener(OnButtonCallback callback)
        {
            OnButtonDown -= callback;

        }


        private event OnButtonCallback OnButtonHold;
        protected void InvokeOnButtonHold()
        {
            OnButtonHold?.Invoke();
        }
        public void AddKeyHoldListener(OnButtonCallback callback)
        {
            OnButtonHold += callback;
        }
        public void RemoveKeyHoldListener(OnButtonCallback callback)
        {
            OnButtonHold -= callback;
        }


        private event OnButtonCallback OnButtonUp;
        protected void InvokeOnButtonUp()
        {
            OnButtonUp?.Invoke();
        }
        public void AddKeyUpListener(OnButtonCallback callback)
        {
            OnButtonUp += callback;
        }
        public void RemoveKeyUpListener(OnButtonCallback callback)
        {
            OnButtonUp -= callback;
        }

        public abstract void CheckInput();
    }
}
