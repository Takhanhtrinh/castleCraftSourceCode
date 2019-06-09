
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2: GameLevel
{
    const float DEAFULT_TIME_BETWEEN_BOAT = 30;
    const float DEFAULT_RESOUCES_INTERVAL = 4f;
    public Level2()
    {
        CREATE();
    }
    public override void CREATE()
    {
        m_TotalResouces = 10;
        m_ChanceWood = 60;
        m_NumberOfBoat = 1;
        m_MinEnemy = 2;
        m_MaxEnemy = 5;
        m_TimeBeweenBoat = DEAFULT_TIME_BETWEEN_BOAT;
        m_ReseoucesInterval = DEFAULT_RESOUCES_INTERVAL;
    }
    public override void updateTimeBetweenBoat()
    {
        m_CurrentBoat++;
        m_TimeBeweenBoat = DEAFULT_TIME_BETWEEN_BOAT;
    }

}