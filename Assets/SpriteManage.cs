using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SpriteType
{
    TREE,
    TREE_LOG,
    ROCK,
    STONE,
    TOWER,
    SPEAR,
    RED_ENEMY, // red enemy
    BOAT,
    ATTACKABLE_BOAT,
    HEART,
    RESOURCE_EATER,
    FINAL_BOSS

};


public class SpriteManage : MonoBehaviour
{
    public static int DEFAULT_RED_ENEMY_SIZE = 50;
    public static int DEFAULT_BOAT_SIZE  = 50;
    public GameObject m_TreeLog;
    public GameObject m_Tree;
    public GameObject m_Rock;
    public GameObject m_Stone;
    public GameObject m_Tower;
    public GameObject m_Spear;
    public GameObject m_Health;
    public GameObject m_RedEnemy;
    public GameObject m_Boat;
    public GameObject m_Heart;
    public GameObject m_AttackableBoat;
    public GameObject m_ResourceEater;
    public GameObject m_Boss;

    // static tree 
    static GameObject s_Tree;
    // static treeLog
    static GameObject s_TreeLog;
    // static rock 
    static GameObject s_Rock;
    static GameObject s_Stone;
    static GameObject s_Boss;

    static GameObject s_Tower;
    static GameObject s_Spear;
    static GameObject s_Health;
    static GameObject s_RedEnemy;
    static GameObject s_Boat;
    static GameObject s_Heart;
    static GameObject s_AttackableBoat;
    static GameObject s_ResourceEater;




    public static Stack<GameObject> m_Trees;
    public static Stack<GameObject> m_TreeLogs;

    public static Stack<GameObject> m_Rocks;
    public static Stack<GameObject> m_Stones;
    public static Stack<GameObject> m_Towers;
    public static Stack<GameObject> m_Spears;
    public static Stack<GameObject> m_RedEnemies;

    public static Stack<GameObject> m_Boats;
    public static Stack<GameObject> m_Hearts;
    public static Stack<GameObject> m_AttackableBoats;

    public static Stack<GameObject> m_ResourceEaters;
    public static Stack<GameObject> m_Bosses;


    // Use this for initialization
    void Start()
    {

        Debug.Assert(m_Tree);
        Debug.Assert(m_TreeLog);
        Debug.Assert(m_Rock);
        Debug.Assert(m_Stone);
        Debug.Assert(m_Tower);
        Debug.Assert(m_Spear);
        Debug.Assert(m_Health);
        Debug.Assert(m_RedEnemy);
        Debug.Assert(m_Boat);
        Debug.Assert(m_Heart);
        Debug.Assert(m_AttackableBoat);
        Debug.Assert(m_ResourceEater);
        Debug.Assert(m_Boss);

		// because we cant have static public in inspector, so use set static variable to static functions below
        s_Tree = m_Tree;
        s_TreeLog = m_TreeLog;
        s_Rock = m_Rock;
        s_Stone = m_Stone;
        s_Tower = m_Tower;
        s_Spear = m_Spear;
        s_Health = m_Health;
        s_RedEnemy = m_RedEnemy;
        s_Boat = m_Boat;
        s_Heart = m_Heart;
        s_AttackableBoat = m_AttackableBoat;
        s_ResourceEater = m_ResourceEater;
        s_Boss = m_Boss;

        m_Tree.SetActive(false);
        m_TreeLog.SetActive(false);
        m_Stone.SetActive(false);
        m_Rock.SetActive(false);
        m_Tower.SetActive(false);
		m_Spear.SetActive(false);
        m_RedEnemy.SetActive(false);
        m_Health.SetActive(false);
        s_Boat.SetActive(false);
        s_Heart.SetActive(false);
        s_AttackableBoat.SetActive(false);
        s_ResourceEater.SetActive(false);
        s_Boss.SetActive(false);

        m_Trees = new Stack<GameObject>();
        m_TreeLogs = new Stack<GameObject>();
        m_Rocks = new Stack<GameObject>();
        m_Stones = new Stack<GameObject>();
        m_Towers = new Stack<GameObject>();
        m_Spears = new Stack<GameObject>();
        m_RedEnemies = new Stack<GameObject>();
        m_Boats = new Stack<GameObject>();
        m_Hearts = new Stack<GameObject>();
        m_AttackableBoats = new Stack<GameObject>();
        m_ResourceEaters = new Stack<GameObject>();
        m_Bosses = new Stack<GameObject>();

        // create object pooling
        for (int i = 0; i < 50; i++)
        {
            GameObject treeTemp = Instantiate(m_Tree);
            GameObject treeLogTemp = Instantiate(m_TreeLog);
            GameObject rock = Instantiate(m_Rock);
            GameObject stone = Instantiate(m_Stone);
            GameObject tower = Instantiate(m_Tower);
            GameObject spear = Instantiate(m_Spear);
            GameObject redEnemy = Instantiate(m_RedEnemy);
            GameObject boat = Instantiate(m_Boat);
            m_Trees.Push(treeTemp);
            m_TreeLogs.Push(treeLogTemp);
            m_Rocks.Push(rock);
            m_Stones.Push(stone);
            m_Towers.Push(tower);
            m_Spears.Push(spear);
            m_RedEnemies.Push(redEnemy);
            m_Boats.Push(boat);
        }
        for (int i = 0; i < 20; i++)
        {
            GameObject heart = Instantiate(m_Heart);
            m_Hearts.Push(heart); 
        }
        for (int i = 0; i < 10; i++)
        {
            GameObject boat = Instantiate(m_AttackableBoat);
            GameObject eater = Instantiate(m_ResourceEater);
            GameObject boss = Instantiate(m_Boss);

            m_AttackableBoats.Push(boat);
            m_ResourceEaters.Push(eater);
            m_Bosses.Push(boss);
            
        }
        

    }

    // Update is called once per frame
    void Update()
    {

    }
    static GameObject getNewBoss()
    {
        if (m_Bosses.Count > 0)
        {
            return m_Bosses.Pop();
        }
        else 
            return Instantiate(SpriteManage.s_Boss);

    }
    static GameObject getNewEater()
    {
        if (m_ResourceEaters.Count > 0)
        {
            return m_ResourceEaters.Pop();
        }
        else 
            return Instantiate(SpriteManage.s_ResourceEater);
    }
    static GameObject getNewAttackableBoat()
    {
        if (m_AttackableBoats.Count > 0)
            return m_AttackableBoats.Pop();
        else 
            return Instantiate(SpriteManage.s_AttackableBoat);
    }
    static GameObject getNewHeart()
    {
        if (m_Hearts.Count > 0)
            return m_Hearts.Pop();
        else 
            return Instantiate(SpriteManage.s_Heart);
    }
    static GameObject getNewBoat()
    {
        if (m_Boats.Count > 0)
            return m_Boats.Pop();
        else 
            return Instantiate(SpriteManage.s_Boat);

    }
    static GameObject getNewRedEnemy()
    {
        if (m_RedEnemies.Count > 0)
            return m_RedEnemies.Pop();
        else 
            return Instantiate(SpriteManage.s_RedEnemy);

    }
    static GameObject getNewTower()
    {
        if (m_Towers.Count > 0)
            return m_Towers.Pop();
        else
            return Instantiate(SpriteManage.s_Tower);
    }
    static GameObject getNewRock()
    {
        if (m_Rocks.Count > 0)
            return m_Rocks.Pop();
        else
            return Instantiate(SpriteManage.s_Rock);
    }
    static GameObject getNewStone()
    {
        if (m_Stones.Count > 0)
            return m_Stones.Pop();
        else
            return Instantiate(SpriteManage.s_Stone);
    }
    static GameObject getNewTree()
    {
        if (m_Trees.Count > 0)
            return m_Trees.Pop();
        else
            return Instantiate(SpriteManage.s_Tree);
    }
    static GameObject getNewTreeLog()
    {
        if (m_TreeLogs.Count > 0)
            return m_TreeLogs.Pop();
        else
            return Instantiate(SpriteManage.s_TreeLog);
    }
    static GameObject getNewSpear()
    {
        if (m_Spears.Count > 0)
            return m_Spears.Pop();
        else
            return Instantiate(SpriteManage.s_Spear);
    }
    public static GameObject getNewHealth()
    {
        return Instantiate(SpriteManage.s_Health);
    }
    public static GameObject CREATE_SPRITE(SpriteType type)
    {
        if (type == SpriteType.TREE)
        {
            
            var temp = getNewTree();
            temp.SetActive(true);
            temp.GetComponent<Tree>().setHealth(Tree.TREE_MAX_HEALTH);
            Debug.Log("generate tree");
            return temp;
        }
        else if (type == SpriteType.TREE_LOG)
        {
            var temp = getNewTreeLog();
            temp.SetActive(true);
            return temp;
        }
        else if (type == SpriteType.ROCK)
        {
            var temp = getNewRock();
            temp.SetActive(true);
            temp.GetComponent<Rock>().setHealth(Rock.ROCK_MAX_HEALTH);
            return temp;
        }
        else if (type == SpriteType.STONE)
        {
            var temp = getNewStone();
            temp.SetActive(true);
            return temp;
        }
        else if (type == SpriteType.TOWER)
        {
            var temp = getNewTower();
            var healthSystem = getNewHealth();

            temp.SetActive(true);
            healthSystem.SetActive(true);
            AI ai = temp.GetComponent<AI>();

            ai.m_HealthSystemOBJ = healthSystem;
            ai.CREATE();
            return temp;
        }
        else if (type == SpriteType.RED_ENEMY)
        {
            var temp = getNewRedEnemy();
            var healthSystem = getNewHealth();

            temp.SetActive(true);
            healthSystem.SetActive(true);

            AI ai = temp.GetComponent<AI>();
            ai.m_HealthSystemOBJ = healthSystem;
            ai.CREATE();
            return temp;

        }
        else if (type == SpriteType.SPEAR)
        {
            var temp = getNewSpear();
            Spear spear = temp.GetComponent<Spear>();
            spear.m_TimeActive = Spear.VISIBLE_TIME;
            temp.transform.localRotation = Quaternion.Euler(0,0,0);
            temp.SetActive(true);
            return temp;
        }
        else if (type == SpriteType.BOAT)
        {
            var temp = getNewBoat();
            var healthSystem = getNewHealth();

            temp.SetActive(true);
            healthSystem.SetActive(true);

            AI ai = temp.GetComponent<AI>();
            ai.m_HealthSystemOBJ = healthSystem;
            ai.CREATE();
            return temp;

        }
        else if (type == SpriteType.HEART)
        {
            var temp = getNewHeart();
            temp.SetActive(true);
            return temp;
        }
        else if (type == SpriteType.ATTACKABLE_BOAT)
        {
            var temp = getNewAttackableBoat(); 
            var healthSystem = getNewHealth();

            temp.SetActive(true);
            healthSystem.SetActive(true);

            AI ai = temp.GetComponent<AI>();
            ai.m_HealthSystemOBJ = healthSystem;
            ai.CREATE();
            return temp;

        }
        else if (type == SpriteType.RESOURCE_EATER)
        {
            var temp = getNewEater();
            var healthSystem = getNewHealth();

            temp.SetActive(true);
            healthSystem.SetActive(true);

            AI ai = temp.GetComponent<AI>();
            ai.m_HealthSystemOBJ = healthSystem;
            ai.CREATE();
            return temp;
        }
        else if (type == SpriteType.FINAL_BOSS)
        {
            var temp = getNewBoss();
            var healthSystem = getNewHealth();

            temp.SetActive(true);
            healthSystem.SetActive(true);
            Debug.Assert(healthSystem != null);

            AI ai = temp.GetComponent<AI>();
            ai.m_HealthSystemOBJ = healthSystem;
            ai.CREATE();
            return temp;
        }
        Debug.Assert(false);
        return null;
    }
    public static void DESTROY_SPRITE(GameObject obj)
    {
        if (obj == null) return;
        else
            obj.SetActive(false);
        {
            if (obj.tag == "tree")
            {
                m_Trees.Push(obj);
            }
            else if (obj.tag == "treeLog")
            {
                m_TreeLogs.Push(obj);
            }
            else if (obj.tag == "stoneItem")
            {
                m_Stones.Push(obj);
            }
            else if (obj.tag == "stone")
            {
                m_Rocks.Push(obj);
            }
            else if (obj.tag == "tower")
            {
                AI ai = obj.GetComponent<AI>();
                Destroy(ai.m_HealthSystemOBJ);
                m_Towers.Push(obj);
        }
            else if (obj.tag == "spear")
            {
				m_Spears.Push(obj);
            }
            else if (obj.tag == "redEnemy")
            {
                AI ai = obj.GetComponent<AI>();
                Destroy(ai.m_HealthSystemOBJ);
                m_RedEnemies.Push(obj);
            }
            else if (obj.tag == "boat")
            {
                AI ai = obj.GetComponent<AI>();
                Destroy(ai.m_HealthSystemOBJ);
                m_Boats.Push(obj);
            }
            
            else if (obj.tag == "heart")
            {
                m_Hearts.Push(obj);
            }
            else if (obj.tag == "attackableBoat")
            {
                AI ai = obj.GetComponent<AI>();
                Destroy(ai.m_HealthSystemOBJ);
                m_AttackableBoats.Push(obj);
            }
            else if (obj.tag == "resourceEater")
            {
                AI ai = obj.GetComponent<AI>();
                Destroy(ai.m_HealthSystemOBJ);
                m_ResourceEaters.Push(obj);
            }
            else if (obj.tag == "finalBoss")
            {
                AI ai = obj.GetComponent<AI>();
                Destroy(ai.m_HealthSystemOBJ);
                m_Bosses.Push(obj);
            }
            else
            {
                Debug.Log("not found obj: " + obj.tag);
                Debug.Assert(false);
            }
        }

    }

    public static Vector2 randomAroundPoint(Vector2 pos, float radius)
    {
        Vector2 temp = Camera.main.WorldToScreenPoint(pos);
        float angle = Random.Range(0, 180);
        float newRadius = Random.Range(0, radius);

        Vector2 result = new Vector2(temp.x + Mathf.Cos(angle) * radius, temp.y + Mathf.Sin(angle) * newRadius);
        result = Camera.main.ScreenToWorldPoint(result);
        return result;

    }
    // use this function to generate random sprite around the map
    public static Vector2 RandomPos()
    {
        GameObject topWall = GameObject.FindGameObjectWithTag("topWall");
        GameObject bottomWall = GameObject.FindGameObjectWithTag("bottomWall");
        GameObject leftWall = GameObject.FindGameObjectWithTag("leftWall");
        GameObject rightWall = GameObject.FindGameObjectWithTag("rightWall");
        // Debug.Log("leftWall: " + leftWall.transform.position + " rightWall: " + rightWall.transform.position);

        // Debug.Assert(topWall && bottomWall && leftWall && rightWall);
        const int GRID_SIZE = 16;
        float minX = Camera.main.WorldToScreenPoint(leftWall.transform.position).x;
        float maxX = Camera.main.WorldToScreenPoint(rightWall.transform.position).x;
        int tempMinX = (int)Mathf.Floor(minX) / GRID_SIZE;
        int tempMaxX = (int)Mathf.Floor(maxX) / GRID_SIZE;

        minX = tempMinX * GRID_SIZE;
        maxX = tempMaxX * GRID_SIZE;

        float minY = Camera.main.WorldToScreenPoint(bottomWall.transform.position).y;
        float maxY = Camera.main.WorldToScreenPoint(topWall.transform.position).y;

        int tempMinY = (int)Mathf.Floor(minY) / GRID_SIZE;
        int tempMaxY = (int)Mathf.Floor(maxY) / GRID_SIZE;

        minY = tempMinY * GRID_SIZE;
        maxY = tempMaxY * GRID_SIZE;

        Vector3 pos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Camera.main.farClipPlane);

        return Camera.main.ScreenToWorldPoint(pos);
    }
    public static bool canPlaceItem(Vector2 mousePos, GameObject obj)
    {
        if (obj == null)
        {
            Debug.Assert(false);
            return false;
        }

        GameObject topWall = GameObject.FindGameObjectWithTag("topWall");
        GameObject bottomWall = GameObject.FindGameObjectWithTag("bottomWall");
        GameObject leftWall = GameObject.FindGameObjectWithTag("leftWall");
        GameObject rightWall = GameObject.FindGameObjectWithTag("rightWall");

        float minX = Camera.main.WorldToScreenPoint(leftWall.transform.position).x;
        float maxX = Camera.main.WorldToScreenPoint(rightWall.transform.position).x;

        float minY = Camera.main.WorldToScreenPoint(bottomWall.transform.position).y;
        float maxY = Camera.main.WorldToScreenPoint(topWall.transform.position).y;
        Vector3 size = obj.GetComponent<BoxCollider2D>().bounds.size;

        size.x = Mathf.Abs(size.x);
        size.y = Mathf.Abs(size.y);
        size.z = Mathf.Abs(size.z);

        if (mousePos.x > minX && mousePos.x < maxX && mousePos.y > minY && mousePos.y < maxY)
        {
            var collider = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(mousePos), size, 0);
            
            if (collider != null)
            {
                if (collider.GetType() == typeof(CircleCollider2D)) return true;
                GameObject temp = collider.gameObject;
                if (temp == obj) return true;
                else
                    return false;

            }
            else

            {
                return true;
            }

    }
        else
        {
            return false;

        }

    }

}
