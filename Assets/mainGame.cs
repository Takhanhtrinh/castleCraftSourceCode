using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainGame : MonoBehaviour
{

    // Use this for initialization

    int m_CurrentWay;

    float m_CurrentGenerateTreeTimeStep = 0;
    LevelManage m_LevelManage;
    float m_BoatGenerate;
    public GameObject m_LeftWall;
    public GameObject m_RightWall;
    public GameObject m_TopWall;
    public GameObject m_BottomWall;
    public Castle m_Castle;
    static public bool m_IsPause;

    public Text m_WaveText;
    GameObject boss;
    public float m_ReoucesEaterGenerateTime;
    const float DEFAULT_RESOURCE_EATER_TIME = 5;
    

    void Start()
    {

        Debug.Assert(m_LeftWall && m_RightWall && m_TopWall && m_BottomWall);

        m_LevelManage = new LevelManage();
        m_BoatGenerate = m_LevelManage.m_Levels[0].m_TimeBeweenBoat;
        m_CurrentWay = 0;
        m_CurrentGenerateTreeTimeStep = 3;
        m_IsPause = true;

        GameObject temp = GameObject.Find("castle");
        m_Castle = temp.GetComponent<Castle>();
        //GameObject boss = SpriteManage.CREATE_SPRITE(SpriteType.FINAL_BOSS);
        m_ReoucesEaterGenerateTime = DEFAULT_RESOURCE_EATER_TIME;

    }

    // Update is called once per frame
    void Update()
    {
        if (boss != null) 
        {
            if (boss.GetComponent<AI>().getHealth() <= 0)
            {
                SceneManager.LoadScene("WinScene");
            }
        }
        m_WaveText.text = "wave " + (m_CurrentWay + 1) + " / " + m_LevelManage.m_Levels.Count;
        if (mainGame.m_IsPause) return;
        if (m_Castle.getHealth() <= 0)
        {
            Debug.Log("gameover");
            SceneManager.LoadScene("gameOver");

        }
        m_BoatGenerate -= Time.deltaTime;
        generateEater();
        Debug.Log("boat generate time: " + m_BoatGenerate);
        if (m_CurrentWay < m_LevelManage.m_Levels.Count - 1)
        {
            if (m_BoatGenerate <= 0)
            {
                generateBoat();
                //generateAttackableBoat();
                // update time 
                if (m_LevelManage.m_Levels[m_CurrentWay].m_NumberOfBoat < 0)
                {
                    m_LevelManage.m_Levels[m_CurrentWay].m_NumberOfBoat = 0;
                }
                m_LevelManage.m_Levels[m_CurrentWay].m_NumberOfBoat--;
            }
            if (m_LevelManage.m_Levels[m_CurrentWay].m_NumberOfBoat <= 0 && SpriteManage.m_RedEnemies.Count >= SpriteManage.DEFAULT_RED_ENEMY_SIZE
                && SpriteManage.m_Boats.Count >= SpriteManage.DEFAULT_BOAT_SIZE)
            {
                m_CurrentWay++;

                if (m_CurrentWay >= m_LevelManage.m_Levels.Count)
                {
                    m_CurrentWay = 0;
                    m_LevelManage = new LevelManage();
                }
                else
                {
                    m_BoatGenerate = m_LevelManage.m_Levels[m_CurrentWay].m_TimeBeweenBoat;
                    //m_LevelManage.m_Levels[m_CurrentWay].updateTimeBetweenBoat();
                }
            }
        }
        else 
        {
            generateBoss();
        }
        m_CurrentGenerateTreeTimeStep -= Time.deltaTime;
        m_ReoucesEaterGenerateTime -= Time.deltaTime;
        generateResouces();
    }
    void generateBoss()
    {
        if (m_CurrentWay == m_LevelManage.m_Levels.Count - 1)
        {
            Debug.Log("generate boss");
            if (boss == null) 
            {
                boss = SpriteManage.CREATE_SPRITE(SpriteType.FINAL_BOSS);
            }
        }
    }
    void generateBoat()
    {
        if (m_LevelManage.m_Levels[m_CurrentWay].m_NumberOfBoat <= 0) return;
        GameObject castle = GameObject.Find("castle");
        GameObject obj = SpriteManage.CREATE_SPRITE(SpriteType.BOAT);
        // random angle 
        float angle = Random.Range(0, Mathf.PI * 2);
        Debug.Log("angle boat: " + angle);
        float distance = 10;
        int wallRan = (int)Random.Range(0, 4);
        Vector3 offset = Random.insideUnitCircle * 30;
        Vector3 newpos = offset + GameObject.Find("castle").transform.position;
        bool generate = false;
        while (!generate)
        {
            generate = true;
            offset = Random.insideUnitCircle * 30;
            newpos = offset + GameObject.Find("castle").transform.position;
            Collider2D[] list = Physics2D.OverlapBoxAll(newpos, obj.GetComponent<SpriteRenderer>().bounds.size, 0);
            for (int i = 0; i < list.Length; i++)
            {
                Collider2D collider = list[i];
                if (collider.tag == "castle") generate = false;
            }

        }
        obj.transform.position = newpos;
        Boat boat = obj.GetComponent<Boat>();
        GameLevel level = m_LevelManage.m_Levels[m_CurrentWay];
        boat.m_NumberEnemies = Random.Range(level.m_MinEnemy, level.m_MaxEnemy + 1);
    }
    void generateEater()
    {
        GameLevel level = m_LevelManage.m_Levels[m_CurrentWay];


        if (m_ReoucesEaterGenerateTime <= 0 )
        {
            if (level.m_NumberOfEater > 0)
            {
                level.m_NumberOfEater--;
                GameObject eater = SpriteManage.CREATE_SPRITE(SpriteType.RESOURCE_EATER);
                Vector2 pos = SpriteManage.RandomPos();
                eater.transform.position = pos;
            }
            m_ReoucesEaterGenerateTime = DEFAULT_RESOURCE_EATER_TIME;
        }
    }
    void generateAttackableBoat()
    {
        if (m_LevelManage.m_Levels[m_CurrentWay].m_NumberOfAttackableBoat <= 0) return;

        m_LevelManage.m_Levels[m_CurrentWay].m_NumberOfAttackableBoat--;

        GameObject castle = GameObject.Find("castle");
        GameObject obj = SpriteManage.CREATE_SPRITE(SpriteType.ATTACKABLE_BOAT);
        // random angle 
        float angle = Random.Range(0, Mathf.PI * 2);
        Debug.Log("angle boat: " + angle);
        float distance = 10;
        int wallRan = (int)Random.Range(0, 4);
        Vector3 offset = Random.insideUnitCircle * 30;
        Vector3 newpos = offset + GameObject.Find("castle").transform.position;
        bool generate = false;
        while (!generate)
        {
            generate = true;
            offset = Random.insideUnitCircle * 30;
            newpos = offset + GameObject.Find("castle").transform.position;
            Collider2D[] list = Physics2D.OverlapBoxAll(newpos, obj.GetComponent<SpriteRenderer>().bounds.size, 0);
            for (int i = 0; i < list.Length; i++)
            {
                Collider2D collider = list[i];
                if (collider.tag == "castle") generate = false;
            }

        }

        obj.transform.position = newpos;
    }
    // generate resources
    void generateResouces()
    {
        if (m_LevelManage.m_Levels[m_CurrentWay].m_TotalResouces < 0) return;
        if (m_CurrentGenerateTreeTimeStep < 0)
        {
            m_CurrentGenerateTreeTimeStep = m_LevelManage.m_Levels[m_CurrentWay].m_ReseoucesInterval;
            Debug.Log("generate resouces");
            m_LevelManage.m_Levels[m_CurrentWay].m_TotalResouces--;
            int chanceWood = m_LevelManage.m_Levels[m_CurrentWay].m_ChanceWood;
            int ran = (int)Random.Range(0, 101);
            GameObject ob = null;

            if (ran <= chanceWood)
            {
                ob = SpriteManage.CREATE_SPRITE(SpriteType.TREE);
            }
            else
            {
                ob = SpriteManage.CREATE_SPRITE(SpriteType.ROCK);
            }

            bool generated = false;
            // number of attem for item generate 
            int attemp = 10;

            while (!generated && attemp > 0)
            {
                //generated = true;
                Vector2 pos = SpriteManage.RandomPos();
                var colliders = Physics2D.OverlapBoxAll(pos, ob.GetComponent<SpriteRenderer>().bounds.size, 0);
                if (colliders.Length == 0)
                {
                    generated = true;
                }
                else
                {
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        Collider2D collider = colliders[i];
                        Debug.Log("collider tag: " + collider.tag);
                        Debug.Log("type: " + collider.GetType());
                        if (collider.GetType() == typeof(BoxCollider2D))
                        {
                            generated = false;

                        }
                        else if (collider.GetType() == typeof(CircleCollider2D))
                        {
                            if (collider.tag == "castle") generated = true;
                        }

                    }

                }
                if (generated)

                {

                    ob.transform.position = pos;
                }
                attemp--;

            }
            Debug.Log("generated: " + generated);
            if (!generated) SpriteManage.DestroyObject(ob);
        }

    }
}
