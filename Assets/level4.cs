
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level4: GameLevel
{
    const float DEAFULT_TIME_BETWEEN_BOAT = 40;
    const float DEFAULT_RESOUCES_INTERVAL = 4f;
    public Level4()
    {
        CREATE();
    }
    public override void CREATE()
    {
        m_TotalResouces = 15;
        m_ChanceWood = 40;
        m_NumberOfBoat = 1;
        m_MinEnemy = 5;
        m_MaxEnemy = 7;
        m_TimeBeweenBoat = DEAFULT_TIME_BETWEEN_BOAT;
        m_ReseoucesInterval = DEFAULT_RESOUCES_INTERVAL;
        m_NumberOfEater = 6;
        m_NumberOfAttackableBoat = 1;
        
    }
    public override void updateTimeBetweenBoat()
    {
        m_CurrentBoat++;
        m_TimeBeweenBoat = DEAFULT_TIME_BETWEEN_BOAT;
    }

}