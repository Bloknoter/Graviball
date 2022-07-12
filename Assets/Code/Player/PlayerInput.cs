using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using InputEngine.Keyboard;

namespace Player.InputEngine
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        private PlayerController playerController;

        [SerializeField]
        private KeyboardListener keyboardListener;

        void Start()
        {
            keyboardListener.CreateKey("left", KeyCode.A).AddKeyHoldListener(On_left_Key);
            keyboardListener.CreateKey("right", KeyCode.D).AddKeyHoldListener(On_right_Key);
            keyboardListener.CreateKey("jump", KeyCode.W).AddKeyDownListener(On_jump_Key);
            keyboardListener.CreateKey("gravity", KeyCode.E).AddKeyDownListener(On_gravity_Key);
        }

        private void On_left_Key()
        {
            playerController.MoveLeft();
        }

        private void On_right_Key()
        {
            playerController.MoveRight();
        }

        private void On_jump_Key()
        {
            playerController.Jump();
        }

        private void On_gravity_Key()
        {
            playerController.ChangeGravityMode();
        }
    }
}
