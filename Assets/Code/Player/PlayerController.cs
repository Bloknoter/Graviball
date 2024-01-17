using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MovingEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D myrigidbody;

        [SerializeField]
        private Transform mytransform;

        [SerializeField]
        private SpriteRenderer myrenderer; 

        [Header("Move components")]
        [Space(10)]
        [SerializeField]
        private Movement movement;

        [SerializeField]
        private Jumping jumping;

        private bool canJump = true;

        public enum GravityModeEnum
        {
            Down = 1,
            Up = -1
        }

        public GravityModeEnum GravityMode { get; private set; } = GravityModeEnum.Down;

        private void Update()
        {
            myrenderer.flipX = movement.LookingDirection == Movement.LookingDirectionEnum.Right;
        }

        public void MoveLeft()
        {
            movement.MoveLeft();
        }

        public void MoveRight()
        {
            movement.MoveRight();
        }

        public void Jump()
        {
            if (canJump)
            {
                canJump = false;
                jumping.Jump();
                StartCoroutine(JumpCooldown());
            }
        }

        private IEnumerator JumpCooldown()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            canJump = true;
        }

        public void ChangeGravityMode()
        {
            GravityMode = (GravityModeEnum)((int)GravityMode * -1);
            myrigidbody.gravityScale *= -1;
            jumping.ForceMultiplier *= -1;
            mytransform.localScale = new Vector3(mytransform.localScale.x, mytransform.localScale.y * -1, mytransform.localScale.z);
        }

        public void Enable()
        {
            movement.IsEnabled = true;
            jumping.IsEnabled = true;
            myrigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

        public void Disable()
        {
            movement.IsEnabled = false;
            jumping.IsEnabled = false;
            myrigidbody.bodyType = RigidbodyType2D.Static;
            myrigidbody.gravityScale = Mathf.Abs(myrigidbody.gravityScale);
            jumping.ForceMultiplier = 1;
            mytransform.localScale = new Vector3(mytransform.localScale.x, Mathf.Abs(mytransform.localScale.y), mytransform.localScale.z);
        }
    }
}
