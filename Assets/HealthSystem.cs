using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem: MonoBehaviour
{
    public int m_MaxHealth;
    public int m_CurrentHealth;
    public GameObject m_HealthSprite;
    public void Init(int maxHealth)
    {
        m_MaxHealth = maxHealth;
        m_CurrentHealth = maxHealth;
        Debug.Assert(m_HealthSprite);
    }
    void setHealth(int health)
    {
        m_CurrentHealth = health;
    }
    void Start()
    {

    }
    void Update()
    {
        m_HealthSprite.transform.localScale = new Vector3((float)m_CurrentHealth / (float)m_MaxHealth, 1, 1);
    }




}