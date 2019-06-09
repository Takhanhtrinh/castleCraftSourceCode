using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesEater : AI{
	const int DEFAULT_HEALTH = 50;
	const float MOVE_SPEED = 4;
	const float DEFAULT_ATTACK_DELAY = 1; 
	const int FACING_RIGHT = 0;
	const int FACING_LEFT = 1;
	int m_Facing;

	public override void CREATE()
	{
		m_Facing = FACING_RIGHT;
		m_Side = ObjectSide.ENEMY_SIDE;
		initHealth(DEFAULT_HEALTH);	
	}

	public override void Attack()
	{
		if (m_EnemyTarget != null)
		{
			if (!m_EnemyTarget.active)
			{
				m_EnemyTarget = null;
				return;
			}
			float distance = Vector2.Distance(transform.position, m_EnemyTarget.transform.position);	
			calculateAttackDistance();
			if (distance< m_AttackDistance)
			{
				if (m_AttackDelay <= 0)
				{
					MusicHandler.PlaySound(SoundType.RED_ENEMY_ATTACK);
					m_AttackDelay = DEFAULT_ATTACK_DELAY;
					if (m_EnemyTarget.tag == "tree")
					{
						m_EnemyTarget.GetComponent<Tree>().m_health--;
						if (m_EnemyTarget.GetComponent<Tree>().m_health <= 0)
						{
							SpriteManage.DESTROY_SPRITE(m_EnemyTarget);
							m_EnemyTarget = null;
						}
					}
					else if (m_EnemyTarget.tag == "stone")
					{
						m_EnemyTarget.GetComponent<Rock>().m_health--;
						if (m_EnemyTarget.GetComponent<Rock>().m_health <= 0)
						{
							SpriteManage.DESTROY_SPRITE(m_EnemyTarget);
							m_EnemyTarget = null;
						}
					}
				}
			}
			// moving to the target 
			else 
			{
				float angle = Mathf.Atan2(m_EnemyTarget.transform.position.y - transform.position.y, m_EnemyTarget.transform.position.x - transform.position.x);
				Vector2 pos = transform.position;
				pos.x += Mathf.Cos(angle) * MOVE_SPEED * Time.fixedDeltaTime;
				pos.y += Mathf.Sin(angle) * MOVE_SPEED * Time.fixedDeltaTime;
				transform.position = pos;
			}

		}	

	}
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (mainGame.m_IsPause) return;
		m_AttackDelay -= Time.deltaTime;
		if (m_AttackDelay <= 0) m_AttackDelay = 0;
		updateHealthBar();
		checkHealth();

		
	}
	void FixedUpdate()
	{
		if (mainGame.m_IsPause) return;
		findResources();
		Attack();
		changeFacing();

	}
	void changeFacing()
	{
		if (m_EnemyTarget != null)
		{
			Vector2 enemyPos = m_EnemyTarget.transform.position;
			Vector2 pos = transform.position;

			if (enemyPos.x > pos.x ) m_Facing = FACING_RIGHT;
			else m_Facing = FACING_LEFT;
			
		}
	}
	void findResources()
	{
		if (m_EnemyTarget != null) return;
		Collider2D [] c = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);
		if (c.Length > 0 )
		{
			float minDistance = 999999;
			for (int i = 0; i < c.Length; i++) 
			{
				if (c[i].tag == "tree" || c[i].tag == "stone")
				{
					float distance = Vector2.Distance(transform.position,c[i].transform.position);
					if (distance < minDistance) 
					{
						minDistance = distance;
						m_EnemyTarget = c[i].gameObject;
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
	}
	public void AttackByCharacter()
	{
		Character character = GameObject.Find("character").GetComponent<Character>();
        if (character.m_currentWeapon.m_item != ItemType.SWORD && character.m_currentWeapon.m_item != ItemType.AXE) return;
        {
            MusicHandler.PlaySound(SoundType.AXE_SOUND);
            if (character.m_currentWeapon.m_item == ItemType.SWORD)
                character.setDelayWeapon(Character.SWORD_DELAY);
            else
                character.setDelayWeapon(Character.AXE_DELAY);
            m_Health -= Weapon.getWeaponDamage(character.m_currentWeapon.m_item);
            Debug.Log("enemy is attacked");
        }

	}
}
