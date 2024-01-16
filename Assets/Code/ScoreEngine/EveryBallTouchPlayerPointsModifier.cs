using Database.AI;
using GameEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveryBallTouchPlayerPointsModifier : MonoBehaviour
{
    [SerializeField]
    private ScoreCounter m_scoreCounter;

    [SerializeField]
    private int m_pointsForTouch = 2;

    [SerializeField]
    private string m_ballTag;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(m_ballTag))
            m_scoreCounter.AddPlayerPoints(m_pointsForTouch);
    }
}
