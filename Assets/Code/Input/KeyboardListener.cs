using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InputEngine.Keyboard
{
    public class KeyboardListener : ButtonDeviceListener
    {
        public KeyListener CreateKey(string Name)
        {
            KeyListener keyListener = new KeyListener(Name);
            AddButtonListener(keyListener);
            return keyListener;
        }

        public KeyListener CreateKey(string Name, KeyCode keyCode)
        {
            KeyListener keyListener = new KeyListener(Name, keyCode);
            AddButtonListener(keyListener);
            return keyListener;
        }
    }
}
