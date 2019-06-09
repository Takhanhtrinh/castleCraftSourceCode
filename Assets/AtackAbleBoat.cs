using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackAbleBoat : AI
{
    const int DEFAULT_HEALTH = 150;
    const float MOVE_SPEED = 2;
    const float DEFAULT_ATTACK_DELAY = 3f;
    const float ANGLE_MOVE = 5 * Mathf.Deg2Rad;
    const float DEFAULT_VISIBILE_TIME = 60.0f;

    int m_NumberOfBullets;
    GameObject m_Castle;
    float m_CurrentAngle = 0;
    float m_FirstAngle = 0;
    float m_VisibleTime;
    public override void CREATE()
    {

        initHealth(DEFAULT_HEALTH);
        m_VisibleTime = DEFAULT_VISIBILE_TIME;
        m_Side = ObjectSide.ENEMY_SIDE;
        m_Castle = GameObject.Find("castle");
        CalculateAngle();
    }
    public override void Attack()
    {

        Vector2 newPos = transform.position;
        Vector2 origin = m_Castle.transform.position;
        float radius = Vector2.Distance(transform.position, origin);
        m_CurrentAngle += ANGLE_MOVE * Time.fixedDeltaTime;
        newPos.x = origin.x + Mathf.Cos(m_CurrentAngle) * radius;
        newPos.y = origin.y + Mathf.Sin(m_CurrentAngle) * radius;
        transform.position = newPos;
        if (m_AttackDelay <= 0)
        {
            m_AttackDelay = DEFAULT_ATTACK_DELAY;
            if (m_EnemyTarget != null)
            {
                var s = SpriteManage.CREATE_SPRITE(SpriteType.SPEAR);
                var spear = s.GetComponent<Spear>();
                float attackAngle = Mathf.Atan2(m_EnemyTarget.transform.position.y - newPos.y, m_EnemyTarget.transform.position.x - newPos.x);
                spear.transform.position = transform.position;
                spear.Init(attackAngle, ObjectSide.ENEMY_SIDE);
				MusicHandler.PlaySound(SoundType.SPEAR_THROW);
            }
        }


    }

    // Use this for initialization
    void Start()
    {
        CREATE();
        Debug.Assert(m_Castle);



    }

    // Update is called once per frame
    void Update()
    {
        if (mainGame.m_IsPause) return;
        
        m_AttackDelay -= Time.fixedDeltaTime;
        if (m_AttackDelay <= 0) m_AttackDelay = 0;
        //checkHealth();
        if (m_VisibleTime <= 0)
        {
           SpriteManage.DESTROY_SPRITE(gameObject);
        }
        m_VisibleTime -= Time.deltaTime;
        findEnemy();
        updateHealthBar();
        checkHealth();

    }
    void CalculateAngle()
    {
        m_CurrentAngle = Mathf.Atan2(m_Castle.transform.position.y - transform.position.y, m_Castle.transform.position.x - transform.position.x);
    }
    void FixedUpdate()
    {
        if (mainGame.m_IsPause) return;
        BoxCollider2D c1 = GetComponent<BoxCollider2D>();
        CircleCollider2D c2 = m_Castle.GetComponent<CircleCollider2D>();
        if (c1.Distance(c2).isOverlapped)
        {
            Attack();
        }
        else
        {
            float angle = Mathf.Atan2(m_Castle.transform.position.y - transform.position.y, m_Castle.transform.position.x - transform.position.x);
            if (m_FirstAngle == 0)
                m_FirstAngle = angle;
            Vector2 newPos = transform.position;
            newPos.x += Mathf.Cos(angle) * MOVE_SPEED * Time.fixedDeltaTime;
            newPos.y += Mathf.Sin(angle) * MOVE_SPEED * Time.fixedDeltaTime;
            transform.position = newPos;
        }

    }
    void findEnemy()
    {
        if (m_EnemyTarget == null)
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);
            Debug.Log("number of colliders: " + colliders.Length);
            float maxDistance = 99999;
            if (colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    Collider2D collider = colliders[i];
                    Debug.Log("attack boat colliders: " + collider.tag);
                    if (collider.tag != "tower" && collider.tag != "character")  continue;
                   float distance = Vector2.Distance(transform.position, collider.transform.position);
                    if (distance < maxDistance)
                    {
                        maxDistance = distance;
                        m_EnemyTarget = collider.gameObject;
                        Debug.Log("found enemy:  " + collider.tag);
                    }
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D c)
    {
		

    }
	void OnTriggerExit2D(Collider2D c)
	{
		if (m_EnemyTarget == c.gameObject) m_EnemyTarget = null;

	}
}
