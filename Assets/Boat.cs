
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat: AI 
{
    const int DEFAULT_HEALTH = 150;
    const float MOVE_SPEED = 2;
    const float RESPAWN_TIME = 2;
    public int m_NumberEnemies;
    GameObject m_Castle;
    bool m_Arrived;
    float m_TimeRespawnEnemy;
    GameObject m_Wall;
    public override void CREATE() 
    {

        m_Health = DEFAULT_HEALTH;
        initHealth(DEFAULT_HEALTH);
        m_Arrived = false;
        m_Castle = GameObject.Find("castle");
        m_TimeRespawnEnemy = 0;
        m_Wall = null;
        Debug.Assert(m_Castle);

    }
    public override void Attack()
    {

    }
    void Start()
    {
    }
    void Update()
    {
        if (mainGame.m_IsPause) return;
        updateHealthBar();

    }
    void FixedUpdate()
    {
        if (mainGame.m_IsPause) return;

            if(!m_Arrived)
            {
                float angle = Mathf.Atan2(m_Castle.transform.position.y - transform.position.y, m_Castle.transform.position.x - transform.position.x);
                Vector2 pos = transform.position;
                pos.x += Mathf.Cos(angle) * MOVE_SPEED * Time.fixedDeltaTime;
                pos.y += Mathf.Sin(angle) * MOVE_SPEED * Time.fixedDeltaTime;
                transform.position = pos;
            }
            else 
            {
                m_TimeRespawnEnemy -= Time.fixedDeltaTime;
                if (m_TimeRespawnEnemy <= 0)
                {
                    if (m_NumberEnemies > 0)
                    {
                        m_TimeRespawnEnemy = RESPAWN_TIME;
                        m_NumberEnemies--;
                        Vector2 castlePos = m_Castle.transform.position;
                        Vector2 currentPos = transform.position;
                        float angle = Mathf.Atan2(castlePos.y - currentPos.y, castlePos.x - currentPos.x);
                        float offset = m_Wall.GetComponent<BoxCollider2D>().size.x;
                        currentPos.x += Mathf.Cos(angle) + offset;
                        currentPos.x += Mathf.Sin(angle) + offset;
                        GameObject enemy = SpriteManage.CREATE_SPRITE(SpriteType.RED_ENEMY);
                        enemy.transform.position = currentPos;
                    }
                    // delete the boat
                    else 
                    {
                        SpriteManage.DESTROY_SPRITE(gameObject);
                    }

                }

            }
            checkHealth();
    }
    
    void OnTriggerEnter2D(Collider2D c)
    {
        string leftWall = "leftWall";
        string rightWall = "rightWall";
        string topWall = "topWall";
        string bottomWall = "bottomWall";
        string tag = c.tag;
        if (tag == leftWall || tag == rightWall || tag == topWall || tag == bottomWall)
        {
            m_Arrived = true;
            m_Wall = c.gameObject;
        }

    }

}