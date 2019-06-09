using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : AI{
	// enemy target
	const float DEFAULT_ATTACK_DELAY = 3;
	const int DEAFULT_HEALTH = 200;
	
	public bool m_IsPlaced;

	public override void CREATE()
	{
		initHealth(DEAFULT_HEALTH);
		m_EnemyTarget = null;
		m_AttackDelay = 0;
		m_IsPlaced = false;

	}
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
        if (mainGame.m_IsPause) return;
		m_AttackDelay -= Time.deltaTime;
		updateHealthBar();

		
	}
	
	void FixedUpdate()
	{
        if (mainGame.m_IsPause) return;
		findEnemy();
		Attack();
		checkHealth();

	}
	public override void Attack()
	{
		if (!m_IsPlaced) return;
		if (m_AttackDelay <= 0)
		{
			if (m_EnemyTarget != null)
			{
				if (!m_EnemyTarget.active || m_EnemyTarget.GetComponent<AI>().getHealth() <= 0)
				{
					m_EnemyTarget = null;
					return;
				}
				MusicHandler.PlaySound(SoundType.SPEAR_THROW);
				m_AttackDelay = DEFAULT_ATTACK_DELAY;
				float angle = Mathf.Atan2(m_EnemyTarget.transform.position.y - transform.position.y,  m_EnemyTarget.transform.position.x - transform.position.x );
				GameObject obj = SpriteManage.CREATE_SPRITE(SpriteType.SPEAR);
				Debug.Assert(obj);
				obj.transform.position = transform.position;
				Spear spear = obj.GetComponent<Spear>();
				spear.Init(angle, ObjectSide.OUR_SIDE);
				
			}
			else 
			{
				m_AttackDelay = 0;
			}
			
		}

	}
	void findEnemy()
	{
		var colliders = getColliders();
		// find enemy if it is attacked by an enemy
		if (m_EnemyTarget == null)
		{
        	Debug.Assert(colliders.Length > 0);
        	for (int i = 0; i < colliders.Length; i++)
        	{
				Collider2D collider = colliders[i];
				AI ai = collider.GetComponent<AI>();
				if (!ai) continue;
				if (ai.m_Side == ObjectSide.ENEMY_SIDE)
				{
					m_EnemyTarget = ai.gameObject;
					return;
				}
        	}

		} 

	}

    Collider2D[] getColliders()
    {
        return Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);
    }
	void OnTriggerEnter2D(Collider2D collider)
	{

		if (m_EnemyTarget != null) return;
		string enemy1Name = "redEnemy";
		string enemy2Name = "resourceEater";
		string enemy3Name = "boat";
		GameObject temp = collider.gameObject;
		Debug.Log(temp.tag);
		// if the current target is current atack target then do nothing
		if (m_EnemyTarget == temp) return;
		// if the collider is enemy and current attack target is null then set new attack target
		if (m_EnemyTarget == null)
		{
			if (temp.tag == enemy1Name ) 

			{
					m_EnemyTarget = temp;
					//Debug.Log("new enemy");
			}
			else if (temp.tag == enemy2Name)
			{
				m_EnemyTarget = temp;
			}
			else if (temp.tag == enemy3Name) 
			{
				m_EnemyTarget = temp;
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
}
