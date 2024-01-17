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

        private KeyListener m_leftKey;
        private KeyListener m_rightKey;
        private KeyListener m_jumpKey;
        private KeyListener m_gravityKey;

        private void Start()
        {
            m_leftKey = keyboardListener.CreateKey("left", KeyCode.A);
            m_leftKey.AddKeyHoldListener(On_left_Key);

            m_rightKey = keyboardListener.CreateKey("right", KeyCode.D);
            m_rightKey.AddKeyHoldListener(On_right_Key);

            m_jumpKey = keyboardListener.CreateKey("jump", KeyCode.W);
            m_jumpKey.AddKeyDownListener(On_jump_Key);

            m_gravityKey = keyboardListener.CreateKey("gravity", KeyCode.E);
            m_gravityKey.AddKeyDownListener(On_gravity_Key);
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

        private void OnDisable()
        {
            m_leftKey.RemoveKeyHoldListener(On_left_Key);

            m_rightKey.RemoveKeyHoldListener(On_right_Key);

            m_jumpKey.RemoveKeyDownListener(On_jump_Key);

            m_gravityKey.RemoveKeyDownListener(On_gravity_Key);
        }
    }
}
