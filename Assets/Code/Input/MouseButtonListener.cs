using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InputEngine.Mouse
{
    public class MouseButtonListener : ButtonListener
    {
        public MouseButtonListener(string _Name) : base(_Name)
        {

        }

        public MouseButtonListener(string _Name, int _code) : base(_Name)
        {
            Code = _code;
        }

        private int code;

        public int Code
        {
            get { return code; }
            set
            {
                if(value == 0 || value == 1 || value == 2)
                {
                    code = value;
                }
            }
        }

        public override void CheckInput()
        {
            if (Input.GetMouseButtonDown(code))
            {
                InvokeOnButtonDown();
            }
            if (Input.GetMouseButton(code))
            {
                InvokeOnButtonHold();
            }
            if (Input.GetMouseButtonUp(code))
            {
                InvokeOnButtonUp();
            }
        }
    }
}
