using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjectSide
{
    OUR_SIDE,
    ENEMY_SIDE
}
public abstract class AI: MonoBehaviour {

    public int m_Health; 
    protected float m_AttackDelay;
    protected int m_Damage;
	protected float m_AttackDistance;
	protected GameObject m_EnemyTarget = null;
	
	public HealthSystem m_HealthSystem;
	public GameObject m_HealthSystemOBJ;
    public ObjectSide m_Side;


	public abstract void CREATE();
	protected void initHealth(int maxHealth)
	{
		m_Health = maxHealth;
        GameObject bar = m_HealthSystemOBJ.transform.Find("Bar").gameObject;
        m_HealthSystem = bar.GetComponent<HealthSystem>();
        m_HealthSystem.Init(maxHealth);
        Debug.Assert(bar);

	}
	public abstract void Attack();

	// Use this for initialization
	void Start () {
		
	}
    protected void setHealthBarPosition()
    {
        Debug.Assert(m_HealthSystemOBJ);
        Vector2 pos = transform.position;
		pos.y += GetComponent<SpriteRenderer>().bounds.size.y / 2 + .5f;
        m_HealthSystemOBJ.transform.position = pos;

    }
	protected void updateHealthBar()
	{
		m_HealthSystem.m_CurrentHealth = m_Health;
		setHealthBarPosition();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void setHealth(int health)
	{
		m_Health = health;
	}
	public int getHealth()
	{
		return m_Health;
	}
	protected void checkHealth()
	{
		if (m_Health <= 0)
		{
			SpriteManage.DESTROY_SPRITE(gameObject);
		} 
	}
	protected void calculateAttackDistance()
	{
		Debug.Assert(m_EnemyTarget);
		if (m_EnemyTarget)
		{
			Vector2 size = m_EnemyTarget.GetComponent<SpriteRenderer>().bounds.size;
			m_AttackDistance = Mathf.Max(size.x, size.y);
		}
	}
}
