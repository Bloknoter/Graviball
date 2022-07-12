using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MovingEngine
{
    public class Jumping : MonoBehaviour, ISwitchable
    {
        [SerializeField]
        protected Rigidbody2D myrigidbody;

        [SerializeField]
        private Collider2D BodyCollider;

        [SerializeField]
        private Transform CheckGroundPoint;

        public float JumpForce;

        private bool isEnabled = true;

        public bool IsEnabled 
        { 
            get { return isEnabled; }
            set
            {
                isEnabled = value;
            }
        }

        private float forceMultiplier = 1;

        public float ForceMultiplier
        {
            get { return forceMultiplier; }
            set
            {
                if (value != 0)
                    forceMultiplier = value;
            }
        }

        public bool IsGrounded
        {
            get
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(CheckGroundPoint.position, 0.1f);
                bool wasRigid = false;
                for(int i = 0; i < colliders.Length;i++)
                {
                    if(!colliders[i].isTrigger && colliders[i] != BodyCollider/* && colliders[i].gameObject.tag == "wall"*/)
                    {
                        wasRigid = true;
                        break;
                    }
                }
                return wasRigid;
            }
        }

        public void Jump()
        {
            if (isEnabled)
            {
                if (IsGrounded)
                {
                    myrigidbody.AddForce(Vector2.up * JumpForce * ForceMultiplier, ForceMode2D.Impulse);
                }
            }
        }
    }
}
