using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    public static int ROCK_MAX_HEALTH = 3;
    const int NUMBER_OF_STONE = 2;
    // Use this for initialization
    public int m_health;
    float m_width;
    Character m_Character;
    SpriteRenderer spr;
void Start()
    {
        m_Character = GameObject.FindWithTag("character").GetComponent<Character>();

        Debug.Assert(m_Character != null);
        spr = GetComponent<SpriteRenderer>();
        m_width = Camera.main.WorldToScreenPoint(spr.bounds.max).x - Camera.main.WorldToScreenPoint(spr.bounds.min).x;
        m_health = ROCK_MAX_HEALTH;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setHealth(int health)
    {
        m_health = health;
    }
    public void AttackRock()
    {

        // set the selection square is show
        // if characters holding axe and clicked on  the tree in range, then tree health is -1
        if (m_Character.m_currentWeapon.m_item != ItemType.AXE) return;
        const float OFFSET = 16;
        float distance = Vector2.Distance(Camera.main.WorldToScreenPoint(m_Character.transform.position), Camera.main.WorldToScreenPoint(transform.position));

            MusicHandler.PlaySound(SoundType.AXE_SOUND);
            m_health--;
            Debug.Log("rock is attacked");
            if (m_health < 0)
            {
                gameObject.SetActive(false);
                Debug.Log("rock is destroyed");
                SpriteManage.DESTROY_SPRITE(gameObject);
                // generate tree log after tree is cut down
                for (int i = 0; i < NUMBER_OF_STONE; i++)

                {

                    GameObject temp = SpriteManage.CREATE_SPRITE(SpriteType.STONE);
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
