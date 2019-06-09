using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{

    // Use this for initialization
    public static int TREE_MAX_HEALTH = 2;
    // number of tree logs after the tree is cut down
    const int NUMBER_OF_TREE_LOG = 3;
    Character m_Character;
    SpriteRenderer spr;

    float m_width;
    public int m_health;
    void Start()
    {
        m_Character = GameObject.FindWithTag("character").GetComponent<Character>();

        Debug.Assert(m_Character != null);
        spr = GetComponent<SpriteRenderer>();
        m_width = Camera.main.WorldToScreenPoint(spr.bounds.max).x - Camera.main.WorldToScreenPoint(spr.bounds.min).x;
        m_health = TREE_MAX_HEALTH;
    }


    // Update is called once per frame
    void Update()
    {


    }
    public void setHealth(int health)
    {
        m_health = health;
    }
	public void AttackTree()
	{
        if (m_Character.m_currentWeapon.m_item != ItemType.AXE) return;

                MusicHandler.PlaySound(SoundType.AXE_SOUND);
                m_health--;
                Debug.Log("tree is attacked");
                if (m_health < 0)
                {
                    gameObject.SetActive(false);
                    Debug.Log("Tree is destroyed");
                    SpriteManage.DESTROY_SPRITE(gameObject);
                    // generate tree log after tree is cut down
                    for (int i = 0; i < NUMBER_OF_TREE_LOG; i++)

                    {

                        GameObject temp = SpriteManage.CREATE_SPRITE(SpriteType.TREE_LOG);
                        Debug.Assert(temp);
                        Vector2 newPos = SpriteManage.randomAroundPoint(transform.position, 16);
                        temp.transform.position = newPos;
                    }
                }



	}
    void OnMouseDown()
    {

    }
}
