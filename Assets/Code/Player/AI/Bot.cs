using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Database.AI;

namespace GameEngine.AI
{
    public class Bot : MonoBehaviour
    {
        [SerializeField]
        private Player.PlayerController playerController;

        [SerializeField]
        private Transform mytransform;

        [SerializeField]
        private MovingEngine.Movement movement;

        [SerializeField]
        private MovingEngine.Jumping jumping;

        [SerializeField]
        private Transform Ball;

        [SerializeField]
        private BotLevelsData levelsData;

        [SerializeField]
        [Min(0)]
        private float JumpTriggerDistance;

        private enum AttackDirectionEnum
        {
            Right,
            Left
        }

        [SerializeField]
        private AttackDirectionEnum AttackDirection = AttackDirectionEnum.Right;

        private enum StateEnum
        {
            Attack,
            GoingToWaitingDefense,
            Defense,
            WaitingDefense,
            ChangingGravity,
            GoingOutOfBasket
        }

        private StateEnum State = StateEnum.Attack;

        void Start()
        {
            if (levelsData != null)
            {
                movement.MovementSpeed = levelsData.ChoosedLevelData.Speed;
                jumping.JumpForce = levelsData.ChoosedLevelData.JumpForce;
            }
        }

        void Update()
        {
            float distance = Vector2.Distance(mytransform.position, Ball.position);
            float Xdelta = mytransform.position.x - Ball.position.x;
            float Ydelta = mytransform.position.y - Ball.position.y;
            if (AttackDirection == AttackDirectionEnum.Right)
            {
                if (State == StateEnum.Attack)
                {
                    if (Xdelta < 0)
                    {
                        playerController.MoveRight();
                    }
                    else
                    {
                        State = StateEnum.Defense;
                    }
                    if (distance < JumpTriggerDistance && Ydelta < -1f)
                    {
                        playerController.Jump();
                    }
                }
                if (State == StateEnum.Defense)
                {
                    if (Ball.position.x < mytransform.position.x + 1)
                    {
                        playerController.MoveLeft();
                    }
                    else
                    {
                        State = StateEnum.Attack;
                    }
                }

                if (State == StateEnum.GoingToWaitingDefense)
                {
                    if (Mathf.Abs(Xdelta) < 3f)
                    {
                        playerController.MoveLeft();
                    }
                    else
                    {
                        playerController.MoveRight();
                        State = StateEnum.WaitingDefense;
                    }
                }
            }
            else if(AttackDirection == AttackDirectionEnum.Left)
            {
                if (State == StateEnum.Attack)
                {
                    if (Xdelta > 0)
                    {
                        playerController.MoveLeft();
                    }
                    else
                    {
                        State = StateEnum.Defense;
                    }
                    if (distance < JumpTriggerDistance && Ydelta < -1f)
                    {
                        playerController.Jump();
                    }
                }
                if (State == StateEnum.Defense)
                {
                    if (Xdelta < 1)
                    {
                        playerController.MoveRight();
                    }
                    else
                    {
                        State = StateEnum.Attack;
                    }
                }

                if (State == StateEnum.GoingToWaitingDefense)
                {
                    if (Mathf.Abs(Xdelta) < 3f)
                    {
                        playerController.MoveRight();
                    }
                    else
                    {
                        playerController.MoveLeft();
                        State = StateEnum.WaitingDefense;
                    }
                }
            }

            if (State != StateEnum.ChangingGravity && State != StateEnum.GoingOutOfBasket)
            {
                if (Mathf.Abs(Xdelta) < 1f && Mathf.Abs(Ydelta) > 3.5f)
                {
                    playerController.ChangeGravityMode();
                    State = StateEnum.ChangingGravity;
                }
            }

            if (State == StateEnum.ChangingGravity)
            {
                if (Ball.position.y + 2 < mytransform.position.y)
                {
                    playerController.ChangeGravityMode();
                    State = StateEnum.Defense;
                }
                if (jumping.IsGrounded)
                {
                    playerController.ChangeGravityMode();
                    State = StateEnum.Defense;
                }
            }

            if (State == StateEnum.WaitingDefense)
            {
                if (Mathf.Abs(Xdelta) < 2.5f)
                {
                    State = StateEnum.Defense;
                }
            }

            if(movement.IsEnabled == false)
            {
                State = StateEnum.Attack;
            }

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.GetComponent<Ball>() != null)
            {
                if(collision.gameObject.transform.position.y - mytransform.position.y > 0.4f)
                {
                    playerController.Jump();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.GetComponent<NoChanceToScoreZone>() != null)
            {
                State = StateEnum.GoingToWaitingDefense;
                return;
            }
            BasketZone basketZone = collision.gameObject.GetComponent<BasketZone>();
            if (basketZone != null)
            {
                State = StateEnum.GoingOutOfBasket;
                StartCoroutine(GoingOutOfBasket(basketZone.BasketSide));
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<NoChanceToScoreZone>() != null)
            {
                State = StateEnum.Defense;
            }
        }

        private IEnumerator GoingOutOfBasket(BasketZone.SideEnum sideEnum)
        {
            playerController.ChangeGravityMode();
            yield return new WaitForSecondsRealtime(0.5f);
            for(int i = 0; i < 25;i++)
            {
                if (sideEnum == BasketZone.SideEnum.Left)
                    playerController.MoveRight();
                else if (sideEnum == BasketZone.SideEnum.Right)
                    playerController.MoveLeft();
                yield return new WaitForFixedUpdate();
            }
            State = StateEnum.Defense;
        }

    }
}
