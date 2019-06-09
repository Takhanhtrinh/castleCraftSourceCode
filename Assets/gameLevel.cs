using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GameLevel 
{
    public int m_ChanceWood;
    public int m_TotalResouces;
    public int m_NumberOfBoat;
    public int m_NumberOfAttackableBoat;
    public int m_MinEnemy;
    public int m_MaxEnemy;
    public float m_TimeBeweenBoat;
    public int m_CurrentBoat;
    public float m_ReseoucesInterval;
    public int m_NumberOfEater;
    public GameLevel()
    {
        m_ChanceWood = 0;
        m_TotalResouces = 0;
        m_NumberOfBoat =0;
        m_MinEnemy = 0;
        m_MaxEnemy =0;
        m_TimeBeweenBoat = 0;
        m_CurrentBoat = 0;
        m_ReseoucesInterval = 0;
        m_NumberOfAttackableBoat = 0;
        m_NumberOfEater = 0;
    }
    public abstract void CREATE();
    public abstract void updateTimeBetweenBoat();

}