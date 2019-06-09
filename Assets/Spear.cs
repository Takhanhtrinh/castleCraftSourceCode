using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class Spear: Weapon
{

    // the spear will update m_timeActive after it is created 
    public static float VISIBLE_TIME = 5;
    const int DEFAULT_DAMAGE = 20;
    const float SPEED = 7;
    public float m_TimeActive;
    public float m_AttackAngle;
    ObjectSide m_Side;
    public float m_speed; 
    void Start()
    {
        //transform.localRotation = Quaternion.Euler(0,0,-90);
        m_Damage = DEFAULT_DAMAGE;
        m_speed = SPEED;
    
        
    }
    public void Init(float angle, ObjectSide side)
    {
        Debug.Log("angle: " + angle);
        m_AttackAngle = angle;
        m_TimeActive = VISIBLE_TIME;
        m_Side = side;
        Vector3 rotation = transform.eulerAngles;
        rotation.z = angle * Mathf.Rad2Deg;
        // because the sprite is up
        rotation.z -= 90;
        transform.eulerAngles = rotation;
        m_speed = SPEED;
    }
    void Update()
    {
        if (mainGame.m_IsPause) return;
        m_TimeActive -= Time.deltaTime;
        if (m_TimeActive <= 0)
        {
            SpriteManage.DESTROY_SPRITE(gameObject);
        }

    }
    void FixedUpdate()
    {
        if (mainGame.m_IsPause) return;
        // update new pos
        Vector2 pos = transform.position;
        pos.x += Mathf.Cos(m_AttackAngle) * m_speed * Time.fixedDeltaTime;
        pos.y += Mathf.Sin(m_AttackAngle) * m_speed * Time.fixedDeltaTime;
        transform.position = pos;

    }
    void setTimeActive(float time)
    {
        m_TimeActive = time;
    }
    void setAttackAngle(float angle)
    {
        m_AttackAngle = angle;
    }
    void OnTriggerEnter2D(Collider2D colldier)
    {
        if (colldier.GetType() == typeof(CircleCollider2D)) return;

        GameObject obj = colldier.gameObject;
        AI ai = obj.GetComponent<AI>();
        if (ai)
        {
            if (ai.m_Side == m_Side) return;
            ai.setHealth(ai.getHealth() - m_Damage);
            Debug.Log("update enemy health: " + ai.getHealth());
            SpriteManage.DESTROY_SPRITE(gameObject);

        }
        else 
        {
        }
        
    }
}
