using GameEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoalsPlayerPointsModifier : MonoBehaviour
{
    [SerializeField]
    private ScoreCounter m_scoreCounter;

    [SerializeField]
    private int m_pointsForGoal = 100;

    [SerializeField]
    private int m_pointsForMissed = -75;

    private void Awake()
    {
        m_scoreCounter.OnScoreofGreenTeamChangedEvent += OnGreenTeamScoreChanged;
        m_scoreCounter.OnScoreofRedTeamChangedEvent += OnRedTeamScoreChanged;
    }

    private void OnGreenTeamScoreChanged(int greenScore)
    {
        m_scoreCounter.AddPlayerPoints(m_pointsForGoal);
    }

    private void OnRedTeamScoreChanged(int greenScore)
    {
        m_scoreCounter.AddPlayerPoints(m_pointsForMissed);
    }
}
