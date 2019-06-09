
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1: GameLevel
{
    const float NEXT_BOAT_GENERATION = 60.0f;
    const float DEFAULT_RESOUCE_INTERVAL = 5.0f;
    
    public Level1()
    {
        CREATE();
    }
    public override void CREATE()
    {
        m_TotalResouces = 10;
        m_ChanceWood = 50;
        m_NumberOfBoat = 1;
        m_MinEnemy = 2;
        m_MaxEnemy = 3;
        m_TimeBeweenBoat = NEXT_BOAT_GENERATION;
        m_ReseoucesInterval = DEFAULT_RESOUCE_INTERVAL;
        m_NumberOfAttackableBoat = 0;
        m_NumberOfEater = 0;
    }
    public override void updateTimeBetweenBoat()
    {
        m_CurrentBoat++;
        m_TimeBeweenBoat += NEXT_BOAT_GENERATION;

    }

}