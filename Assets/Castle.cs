
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle: AI
{
    const int DEFAULT_HEALTH = 500;
    public float m_circleRadius;

    public override void CREATE()
    {

    }
    public override void Attack()
    {


    }
    void Start()
    {
        
        initHealth(DEFAULT_HEALTH);
        m_Side = ObjectSide.OUR_SIDE;
        m_circleRadius = GetComponent<CircleCollider2D>().radius;
    }

    void Update()
    {
        if (mainGame.m_IsPause) return;
        updateHealthBar();
        
        
    }
    void FixedUpdate()
    {
        if (mainGame.m_IsPause) return;
        

    }
}