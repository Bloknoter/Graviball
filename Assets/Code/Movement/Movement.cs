using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MovingEngine
{
    public class Movement : MonoBehaviour, ISwitchable
    {
        [SerializeField]
        protected Rigidbody2D myrigidbody;

        public float MovementSpeed;

        private float Xmovement;

        public enum LookingDirectionEnum
        {
            Left = -1, Right = 1
        }

        private LookingDirectionEnum lookingDirection;

        private bool isEnabled = true;

        [HideInInspector]
        public float SpeedMultiplier = 1;

        public enum AccelerationModeEnum
        {
            None,
            Soft
        }

        [SerializeField]
        private AccelerationModeEnum AccelerationMode;

        private float Xdelta;

        private void Start()
        {
            Xdelta = MovementSpeed / 2 * SpeedMultiplier;
        }

        private void FixedUpdate()
        {
            myrigidbody.velocity = new Vector2(Xmovement, myrigidbody.velocity.y);
            if (AccelerationMode == AccelerationModeEnum.None)
                Xmovement = 0;
            if (AccelerationMode == AccelerationModeEnum.Soft)
                Xmovement *= 0.99f;
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
            }
        }

        public LookingDirectionEnum LookingDirection
        {
            get { return lookingDirection; }
        }

        public bool IsMoving
        {
            get { return Xmovement != 0; }
        }

        public void StopMoving()
        {
            Xmovement = 0;
        }

        public void MoveLeft()
        {
            if (isEnabled)
            {
                lookingDirection = LookingDirectionEnum.Left;
                if (AccelerationMode == AccelerationModeEnum.None)
                    Xmovement = -Xdelta;
                if (AccelerationMode == AccelerationModeEnum.Soft)
                    Xmovement = Mathf.Clamp(Xmovement - 0.01f, -Xdelta, 10000);
            }
        }

        public void MoveRight()
        {
            if (isEnabled)
            {
                lookingDirection = LookingDirectionEnum.Right;
                if (AccelerationMode == AccelerationModeEnum.None)
                    Xmovement = Xdelta;
                if (AccelerationMode == AccelerationModeEnum.Soft)
                    Xmovement = Mathf.Clamp(Xmovement + 0.01f, 10000, Xdelta);
            }
        }
    }
}
