using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class RedEnemy : AI
{
    const int FACING_RIGHT = 1;
    const int FACING_LEFT = 2;
    const int DEFAULT_HEALTH = 100;
    const float DEFAULT_ATTACK_SPEED = 1.0f;
    // each attack is 5 damage
    const int DEFAULT_DAMAGE = 5;
    const float MOVING_SPEED = 2;
    // for facing direction
    int m_Facing;
    float m_width;

    Character m_Character;



    void Start()
    {
        m_Character = GameObject.Find("character").GetComponent<Character>();
        Debug.Assert(m_Character);
        GetComponent<BoxCollider2D>().isTrigger = true;
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        m_width = Camera.main.WorldToScreenPoint(spr.bounds.max).x - Camera.main.WorldToScreenPoint(spr.bounds.min).x;

    }
    void Update()
    {
        if (mainGame.m_IsPause) return;
        updateHealthBar();


        // get castle 

    }
    void FixedUpdate()
    {
        if (mainGame.m_IsPause) return;
        m_AttackDelay -= Time.fixedDeltaTime;
        if (m_EnemyTarget == null)
        {
            m_EnemyTarget = GameObject.Find("castle");
            Debug.Assert(m_EnemyTarget);
        }
        if (SpriteManage.canPlaceItem(Camera.main.WorldToScreenPoint(transform.position), gameObject))
        //if (m_timeToTurnOnBoundingBox <= 0)
        {

            GetComponent<BoxCollider2D>().isTrigger = false;

        }

        Attack();

        ChangeFacingDirection();



    }
    public override void CREATE()
    {
        m_Health = DEFAULT_HEALTH;
        m_AttackDelay = 0;
        m_Damage = DEFAULT_DAMAGE;
        m_AttackDistance = 100.0f;
        m_Facing = FACING_RIGHT;
        initHealth(DEFAULT_HEALTH);
        GetComponent<BoxCollider2D>().isTrigger = true;
        m_Side = ObjectSide.ENEMY_SIDE;

    }
    public override void Attack()
    {

        float distance = Vector2.Distance(transform.position, m_EnemyTarget.transform.position);
        calculateAttackDistance();
        if (transform.position.x > m_EnemyTarget.transform.position.x)
        {
            m_Facing = FACING_LEFT;
        }
        else
        {
            m_Facing = FACING_RIGHT;

        }
        // if in attack range then attack
        if (distance < m_AttackDistance)
        {
            if (m_AttackDelay <= 0)
            {
                MusicHandler.PlaySound(SoundType.RED_ENEMY_ATTACK);
                m_AttackDelay = DEFAULT_ATTACK_SPEED;
                AI ai = m_EnemyTarget.GetComponent<AI>();
                ai.setHealth(ai.getHealth() - m_Damage);
                if (ai.getHealth() <= 0) m_EnemyTarget = null;

            }

        }
        else
        {
            float angle = Mathf.Atan2(m_EnemyTarget.transform.position.y - transform.position.y, m_EnemyTarget.transform.position.x - transform.position.x);
            Vector2 newPos = transform.position;
            newPos.x += Mathf.Cos(angle) * MOVING_SPEED * Time.fixedDeltaTime;
            newPos.y += Mathf.Sin(angle) * MOVING_SPEED * Time.fixedDeltaTime;
            transform.position = newPos;
        }
        if (m_Health <= 0)
        {
            // generate heart for player to gain health
            float ran = Random.Range(0, 101);
            if (Debug.isDebugBuild)
            {
                ran = 80;
            }
            if (ran >= 70)
            {
                var heart = SpriteManage.CREATE_SPRITE(SpriteType.HEART);
                heart.transform.position = transform.position;
            }
        }
        checkHealth();
    }


    void ChangeFacingDirection()
    {
        if (m_Facing == FACING_LEFT)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {

        // first priority is the tower 
        // second priority is the player
        GameObject obj = collider.gameObject;
        Debug.Log("obj: " + obj.tag);
        if (obj.tag == "tower")
        {
            if (!obj.GetComponent<Tower>().m_IsPlaced) return;
            // set atack target as tower
            if (m_EnemyTarget == null)
            {
                m_EnemyTarget = obj;
            }
            // if current attack target is tower then do nothing
            else
            {
                if (m_EnemyTarget.tag == "tower")
                {
                    return;
                }
                else if (m_EnemyTarget.tag == "character")
                {
                    m_EnemyTarget = obj;

                }
                else if (m_EnemyTarget.tag == "castle")
                {
                    m_EnemyTarget = obj;
                }

            }

        }
        else if (obj.tag == "character")
        {
            if (m_EnemyTarget == null || m_EnemyTarget.tag == "castle")
            {
                m_EnemyTarget = obj;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        // if current attack target is null then do nothing
        if (m_EnemyTarget == null) return;
        // otherwise check if the current attack target is same with current enemy, and then set attack target to null
        GameObject temp = collider.gameObject;
        if (temp == m_EnemyTarget)
        {
            m_EnemyTarget = null;
        }
    }
    public void AttackByCharacter()
    {
        if (m_Character.m_currentWeapon.m_item != ItemType.SWORD && m_Character.m_currentWeapon.m_item != ItemType.AXE) return;
        {
            MusicHandler.PlaySound(SoundType.AXE_SOUND);
            if (m_Character.m_currentWeapon.m_item == ItemType.SWORD)
                m_Character.setDelayWeapon(Character.SWORD_DELAY);
            else
                m_Character.setDelayWeapon(Character.AXE_DELAY);
            m_Health -= Weapon.getWeaponDamage(m_Character.m_currentWeapon.m_item);
            Debug.Log("enemy is attacked");
        }

    }
    void OnMouseDown()
    {



    }

}