using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InputEngine.Mouse
{
    public class MouseListener : ButtonDeviceListener
    {
        public MouseButtonListener CreateKey(string Name)
        {
            MouseButtonListener mouseButtonListener = new MouseButtonListener(Name);
            AddButtonListener(mouseButtonListener);
            return mouseButtonListener;
        }

        public MouseButtonListener CreateKey(string Name, int code)
        {
            MouseButtonListener mouseButtonListener = new MouseButtonListener(Name, code);
            AddButtonListener(mouseButtonListener);
            return mouseButtonListener;
        }
    }
}
