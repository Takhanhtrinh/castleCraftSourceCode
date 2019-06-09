using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level5: GameLevel 
{
    const float DEAFULT_TIME_BETWEEN_BOAT = 90;
    const float DEFAULT_RESOUCES_INTERVAL = 4f;
    public Level5()
    {
        CREATE();
    }
    public override void CREATE()
    {
        m_TotalResouces = 20;
        m_ChanceWood = 30;
        m_NumberOfBoat = 0;
      //  m_MinEnemy = 4;
      //  m_MaxEnemy = 5;
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