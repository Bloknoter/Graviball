using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InputEngine.Keyboard
{
    public class KeyListener : ButtonListener
    {
        public KeyListener(string _Name) :base(_Name)
        {

        }

        public KeyListener(string _Name, KeyCode _keyCode) : base(_Name)
        {
            keyCode = _keyCode;
        }

        public KeyCode keyCode = KeyCode.None;

        public override void CheckInput()
        {
            if(Input.GetKeyDown(keyCode))
            {
                InvokeOnButtonDown();
            }
            if (Input.GetKey(keyCode))
            {
                InvokeOnButtonHold();
            }
            if (Input.GetKeyUp(keyCode))
            {
                InvokeOnButtonUp();
            }
        }
    }
}
