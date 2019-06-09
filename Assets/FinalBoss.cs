using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : AI
{
    const float ATTACK_RANGE = 4.5f;
    const int DEFAULT_HEALTH = 700;
    // the delay when the boss has 50% - 100% health
    const float ATTACK_DELAY_PERIOD_1 = 1.0f;

    // the delay when the boss has less than 50% health
    const float ATTACK_DELAY_PERIOD_2 = .5f;
    const int DEFAULT_DAMAGE = 20;
    const float DEFAULT_MOVE_SPEED = 2.0f;
	const float DEFAULT_ULTIMATE = 15.0f;

    // max spear when the boss health is less than 50%
    const int MAX_SPEAR = 6;
    const int FACING_LEFT = 1;
    const int FACING_RIGHT = 2;
    // Use this for initialization
    Character m_Character;
    Castle m_Castle;

    int m_Facing;
	float m_UltimateAttack;
    public override void CREATE()
    {
		m_UltimateAttack = DEFAULT_ULTIMATE;
        m_Character = GameObject.Find("character").GetComponent<Character>();
        Debug.Assert(m_Character);

        m_Castle = GameObject.Find("castle").GetComponent<Castle>();
        Debug.Assert(m_Castle);

        m_AttackDelay = 0;
        m_Damage = DEFAULT_DAMAGE;
        m_Facing = FACING_LEFT;
        m_Side = ObjectSide.ENEMY_SIDE;

        initHealth(DEFAULT_HEALTH);
        GetComponent<BoxCollider2D>().isTrigger = true;

    }
    public override void Attack()
    {
        if (m_EnemyTarget == null)
        {
            if (m_Character.m_Health > 0)
                m_EnemyTarget = m_Character.gameObject;
            else
                m_EnemyTarget = m_Castle.gameObject;
        }

        if (m_EnemyTarget != null)
        {
            if (!m_EnemyTarget.active )
            {
                m_EnemyTarget = null;
                return;
            }
            float distance = Vector2.Distance(m_EnemyTarget.transform.position, transform.position);
            // attack
            if (distance <= ATTACK_RANGE)
            {
				if (m_UltimateAttack <= 0 )
				{
						for (int i = 0; i < MAX_SPEAR; i++)
						{
							GameObject spear = SpriteManage.CREATE_SPRITE(SpriteType.SPEAR);

     		           		float angle = i * 45 * Mathf.Deg2Rad ;
							Spear s = spear.GetComponent<Spear>();
							s.Init(angle,ObjectSide.ENEMY_SIDE);
							s.m_speed = 9;
					
							spear.transform.position = transform.position;	

						}
						m_UltimateAttack = DEFAULT_ULTIMATE;
						return;

				}
				if (m_AttackDelay <= 0)
				{
					MusicHandler.PlaySound(SoundType.SPEAR_THROW);
					if (m_Health < DEFAULT_HEALTH / 2)
					{
						m_AttackDelay = ATTACK_DELAY_PERIOD_2;
					}
					else 
					{
						m_AttackDelay = ATTACK_DELAY_PERIOD_1;
					}
						GameObject spear = SpriteManage.CREATE_SPRITE(SpriteType.SPEAR);

     	           		float angle = Mathf.Atan2(m_EnemyTarget.transform.position.y - transform.position.y, m_EnemyTarget.transform.position.x - transform.position.x);
						Spear s = spear.GetComponent<Spear>();
						s.Init(angle,ObjectSide.ENEMY_SIDE);
						s.m_speed = 9;
					
						spear.transform.position = transform.position;	

					
				}
            }
            else
            {
                // update position
                float angle = Mathf.Atan2(m_EnemyTarget.transform.position.y - transform.position.y, m_EnemyTarget.transform.position.x - transform.position.x);
                Vector2 newPos = transform.position;
                newPos.x += Mathf.Cos(angle) * DEFAULT_MOVE_SPEED * Time.fixedDeltaTime;
                newPos.y += Mathf.Sin(angle) * DEFAULT_MOVE_SPEED * Time.fixedDeltaTime;
                transform.position = newPos;
            }
        }
    }
    void WalkToCastle ()
    {

        if (!GetComponent<BoxCollider2D>().isTrigger) return;
        if (SpriteManage.canPlaceItem(Camera.main.WorldToScreenPoint(transform.position), gameObject))
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
        m_EnemyTarget = m_Castle.gameObject;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if (mainGame.m_IsPause) return;
        // get all the colliders around boss
        if (m_EnemyTarget == null)
        {
            Collider2D[] colliders = getColliders();
            Collider2D collider = findShortestEnemy(colliders);
            if (collider  != null)
            {
                m_EnemyTarget = collider.gameObject;
            }
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
			m_EnemyTarget = m_Character.gameObject;
            m_Health -= Weapon.getWeaponDamage(m_Character.m_currentWeapon.m_item);
            Debug.Log("enemy is attacked");
        }

	}
    void FixedUpdate()
    {
		if (mainGame.m_IsPause) return;
        WalkToCastle();

		m_AttackDelay -= Time.fixedDeltaTime;
		m_UltimateAttack -= Time.fixedDeltaTime;
        updateHealthBar();
        ChangeFacingDirection();
        Attack();
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
    void OnTriggerExit2D(Collider2D collider)
    {
        if (m_EnemyTarget == collider.gameObject)
        {
            m_EnemyTarget = null;
        }
    }
    Collider2D[] getColliders()
    {
        return Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);
    }
    Collider2D findShortestEnemy(Collider2D[] colliders)
    {
        Debug.Assert(colliders.Length > 0);
        float minDistance = 99999999;
        Collider2D enemy = null;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "character" || colliders[i].tag == "castle" || colliders[i].tag == "tower")
            {
                if (colliders[i].tag == "tower") 
                {
                    Debug.Assert(false);
                    return colliders[i];
                }

                Vector2 enemyPos = colliders[i].gameObject.transform.position;
                float distance = Vector2.Distance(enemyPos, transform.position);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    enemy = colliders[i];
                }
            }
            else if (colliders[i].tag == "tree" || colliders[i].tag == "stone")
            {
                SpriteManage.DESTROY_SPRITE(colliders[i].gameObject);
            }
        }

        return enemy;

    }
}
