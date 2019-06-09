
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3: GameLevel
{
    const float DEAFULT_TIME_BETWEEN_BOAT = 40;
    const float DEFAULT_RESOUCES_INTERVAL = 4f;
    public Level3()
    {
        CREATE();
    }
    public override void CREATE()
    {
        m_TotalResouces = 14;
        m_ChanceWood = 30;
        m_NumberOfBoat = 1;
        m_MinEnemy = 5;
        m_MaxEnemy = 7;
        m_TimeBeweenBoat = DEAFULT_TIME_BETWEEN_BOAT;
        m_ReseoucesInterval = DEFAULT_RESOUCES_INTERVAL;
        m_NumberOfEater = 0;
        
    }
    public override void updateTimeBetweenBoat()
    {
        m_CurrentBoat++;
        m_TimeBeweenBoat = DEAFULT_TIME_BETWEEN_BOAT;
    }

}