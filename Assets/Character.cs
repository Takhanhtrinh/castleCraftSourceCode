using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : AI
{


    const int DEFAULT_HEART_HEALTH = 10;
    public static float AXE_DELAY = 1f;
    public static float SWORD_DELAY = .5f;
    const int FACING_RIGHT = 1;

    const int FACING_LEFT = 0;
    const int MOVING_RIGHT = 1;
    const int MOVING_LEFT = 2;
    const int MOVING_UP = 3;
    const int MOVING_DOWN = 4;
    const int IDLE = 0;
    const int DEFAULT_HEALTH = 100;

    const int WEAPON_AXE = 0x00;

    const int numAnimations = 4;
    const int numFramePerAnimation = 4;
    public int m_state;


    // different weapons sprite
    public GameObject m_Axe;
    public GameObject m_Sword;
    public GameObject m_Spear;
    // for animation
    public Sprite[] m_sprites;
    // for set sprite animation and other stuff
    public SpriteRenderer spr;
    // the current index of m_sprites
    public int m_currentAnimation;
    // the current frame of animation
    public int m_currentFrame;
    // facing direction
    public int m_facing;
    // moving speed
    const float SPEED = 5;
    Camera m_cam;

    public InventoryItem m_currentWeapon;

    // prevent user clicked too fast for chopping tree or attacking 
    float m_weaponDelay;

    public Inventory m_inventory;
    public CraftingEngine m_Craft;
    public WeaponManage m_Weapons;

    GameObject m_currentWeaponSprite;

    UIHandler m_UIhandler;


    float m_DefaultWeaponAngle;
    bool m_AnimationDown = true;
    // Use this for initialization

    public override void CREATE()
    {

    }
    public override void Attack()
    {
            if (m_weaponDelay <= 0)
            {
                setDelayWeapon(Character.AXE_DELAY);

                Vector2 pos = transform.position;
                if (m_facing == FACING_LEFT)
                {
                    pos.x -= 2;
                }
                else
                    pos.x += 2;
                var colliders = Physics2D.OverlapCircleAll(pos, 1);
                var collider = ShortestDistance(colliders);
                if (collider != null)
                {
                    if (collider.tag == "tree")
                        collider.GetComponent<Tree>().AttackTree();
                    else if (collider.tag == "stone")
                        collider.GetComponent<Rock>().AttackRock();
                    else if (collider.tag == "redEnemy")
                        collider.GetComponent<RedEnemy>().AttackByCharacter();
                    else if (collider.tag == "resourceEater") 
                    {
                        ResourcesEater t = collider.GetComponent<ResourcesEater>();
                        t.AttackByCharacter();
                    }
                    else if (collider.tag == "finalBoss")
                    {
                        FinalBoss t = collider.GetComponent<FinalBoss>();
                        t.AttackByCharacter();

                    }
                }
            }

    }
    void Start()
    {
        Debug.Assert(m_Axe);
        Debug.Assert(m_Spear);
        Debug.Assert(m_Sword);

        m_inventory = new Inventory();
        m_Craft = new CraftingEngine();
        m_Weapons = new WeaponManage();

        m_Weapons.addNewWeapon(new InventoryItem(ItemType.AXE));

        m_Craft.m_Inventory = m_inventory;
        m_Craft.m_Weapons = m_Weapons;

        m_facing = FACING_RIGHT;
        changeWeapon(0);


        // initialize 
        m_HealthSystemOBJ = SpriteManage.getNewHealth();
        m_HealthSystemOBJ.SetActive(true);
        Debug.Assert(m_HealthSystemOBJ);
        initHealth(DEFAULT_HEALTH);

        spr = GetComponent<SpriteRenderer>();
        m_currentAnimation = 0;
        m_currentFrame = 0;
        m_state = IDLE;
        m_cam = Camera.main;
        m_weaponDelay = 0;
        m_Side = ObjectSide.OUR_SIDE;



        // get uihandler
        GameObject i = GameObject.Find("Inventory");
        Debug.Assert(i);
        m_UIhandler = i.GetComponent<UIHandler>();
        Debug.Assert(m_UIhandler);

        //m_DefaultWeaponAngle = m_currentWeaponSprite.transform.eulerAngles.z;



    }
    // angle in radian
    public void throwSpear(float angle)
    {
        if (m_Health <= 0 ) return;
        if (m_currentWeapon != null)
        {
            if (m_weaponDelay > 0) return;
            if (m_currentWeapon.m_Quantity <= 0) return;
            if (m_currentWeapon.m_item == ItemType.SPEAR)
            {
                m_currentWeapon.m_Quantity--;
                m_Weapons.setQuantity(m_currentWeapon);

                GameObject temp = SpriteManage.CREATE_SPRITE(SpriteType.SPEAR);
                Spear spear = temp.GetComponent<Spear>();
                spear.transform.position = transform.position;
                spear.Init(angle, ObjectSide.OUR_SIDE);
                MusicHandler.PlaySound(SoundType.SPEAR_THROW);
                m_weaponDelay = AXE_DELAY;
                if (m_currentWeapon.m_Quantity <= 0)
                {
                    changeWeapon(0);
                }
            }
        }
    }
    public void changeWeapon(int index)
    {
        Debug.Assert(index >= 0 && index < m_Weapons.getWeaponSize());
        InventoryItem temp = m_Weapons.getItem(index);
        if (temp.m_item == ItemType.NONE)
        {
            temp = m_Weapons.getItem(0);
        }
        ChangeWeaponSprite(temp.m_item);
        m_currentWeapon = temp;
        m_weaponDelay = 0;
        Vector3 rotation = m_currentWeaponSprite.transform.eulerAngles;

        //m_DefaultWeaponAngle = m_currentWeaponSprite.transform.eulerAngles.z;
        rotation.z = -15;
        m_currentWeaponSprite.transform.eulerAngles = rotation;
        m_DefaultWeaponAngle = -15;



    }
    void ChangeWeaponSprite(ItemType weapon)
    {
        if (m_currentWeaponSprite)
        {
            Destroy(m_currentWeaponSprite);
            m_currentWeaponSprite = null;
        }
        if (weapon == ItemType.AXE)
        {
            m_currentWeaponSprite = Instantiate(m_Axe);

        }
        else if (weapon == ItemType.SPEAR)
        {
            m_currentWeaponSprite = Instantiate(m_Spear);
        }
        else if (weapon == ItemType.SWORD)
        {
            m_currentWeaponSprite = Instantiate(m_Sword);
        }

        if (m_currentWeaponSprite)
        {
            if (m_facing == FACING_LEFT)
            {
                Vector3 scale = m_currentWeaponSprite.transform.localScale;
                scale.x *= -1;
                m_currentWeaponSprite.transform.localScale = scale;
                m_currentWeaponSprite.transform.localRotation = Quaternion.Euler(0, 0, 51);
            }
            m_currentWeaponSprite.transform.parent = transform;
            m_currentWeaponSprite.AddComponent<calculateZOrder>();
            Vector2 newPos = transform.position;
            if (m_facing == FACING_RIGHT)
                newPos.x += .5f;
            else
                newPos.x -= .5f;
            m_currentWeaponSprite.transform.position = newPos;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Health > DEFAULT_HEALTH) m_Health = DEFAULT_HEALTH;
        else if (m_Health <= 0) m_Health = 0;
        if (mainGame.m_IsPause) return;
        updateHealthBar();
        if (Input.GetKey(KeyCode.D))
        {
            m_state = MOVING_RIGHT;
            m_facing = FACING_RIGHT;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            m_state = MOVING_LEFT;
            m_facing = FACING_LEFT;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            m_state = MOVING_UP;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            m_state = MOVING_DOWN;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            m_currentFrame = 0;
            m_state = IDLE;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            m_currentFrame = 0;
            m_state = IDLE;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            m_currentFrame = 0;
            m_state = IDLE;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            m_currentFrame = 0;
            m_state = IDLE;
        }

        // mouse click 
        if (Input.GetMouseButtonDown(0))
        {
            Attack();

        }

        if (m_state != IDLE)
            spriteAnimation();
        else
            spr.sprite = m_sprites[0];

        if (m_state == MOVING_RIGHT)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (m_state == MOVING_LEFT)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (m_state == MOVING_UP)
        {

        }
        else if (m_state == MOVING_DOWN)
        {
        }
        m_weaponDelay -= Time.deltaTime;
        if (m_weaponDelay < 0) m_weaponDelay = 0;

        checkHealth();

    }
    void weaponAnimation()
    {
        if (m_currentWeapon.m_item == ItemType.AXE || m_currentWeapon.m_item == ItemType.SWORD)
        {


            Vector3 angle = m_currentWeaponSprite.transform.localRotation.eulerAngles;
            float animationSpeed = 50 * Time.fixedDeltaTime;
            //Debug.Log("angle: " + angle.z);
            if (m_weaponDelay > 0)
            {
                if (m_AnimationDown)
                {
                    angle.z -= 30;

                }
                else
                {
                    //angle.z += animationSpeed;
                    angle.z += 30;

                }
                if (angle.z <= 220)
                {
                    m_AnimationDown = false;
                }
                else if (angle.z >= 300)
                {
                    m_AnimationDown = true;

                }

            }
            else
            {
                angle.z = -15;
            }
            m_currentWeaponSprite.transform.localEulerAngles = angle;
        }

    }
    void FixedUpdate()
    {

        if (mainGame.m_IsPause) return;
        Vector3 currentPos = transform.position;
        if (m_state == MOVING_DOWN)
        {
            transform.position = new Vector3(currentPos.x, currentPos.y - SPEED * Time.fixedDeltaTime);
        }
        else if (m_state == MOVING_UP)
        {
            transform.position = new Vector3(currentPos.x, currentPos.y + SPEED * Time.fixedDeltaTime);
        }
        else if (m_state == MOVING_LEFT)
        {
            transform.position = new Vector3(currentPos.x - SPEED * Time.fixedDeltaTime, currentPos.y);
        }
        else if (m_state == MOVING_RIGHT)
        {
            transform.position = new Vector3(currentPos.x + SPEED * Time.fixedDeltaTime, currentPos.y);

        }

        Vector3 newCamPos = m_cam.transform.position;
        newCamPos.x = Mathf.Lerp(newCamPos.x, transform.position.x, Time.fixedDeltaTime * 4);
        newCamPos.y = Mathf.Lerp(newCamPos.y, transform.position.y, Time.fixedDeltaTime * 4);
        m_cam.transform.position = newCamPos;
        if (m_weaponDelay > 0)
            weaponAnimation();
    }
    public float getDelayWeapon() { return m_weaponDelay; }
    public void setDelayWeapon(float val) { m_weaponDelay = val; }
    private void spriteAnimation()
    {

        m_currentFrame++;
        m_currentFrame %= numFramePerAnimation;
        if (m_currentFrame == 0)
        {
            m_currentAnimation++;
            m_currentAnimation %= numAnimations;
            spr.sprite = m_sprites[m_currentAnimation];

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject temp = col.gameObject;
        if (temp.tag == "treeLog")
        {
            SpriteManage.DESTROY_SPRITE(temp);
            m_inventory.addNewItem(ItemType.WOOD);
        }
        else if (temp.tag == "stoneItem")
        {
            SpriteManage.DESTROY_SPRITE(temp);
            m_inventory.addNewItem(ItemType.STONE);

        }
        else if (temp.tag == "heart")
        {
            SpriteManage.DESTROY_SPRITE(temp);
            m_Health += DEFAULT_HEART_HEALTH;


        }
    }
    Collider2D ShortestDistance(Collider2D[] colliders)
    {
        Collider2D temp = null;
        float distance = 9999999;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "character") continue;
            float currentDistance = Vector2.Distance(colliders[i].gameObject.transform.position, transform.position);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                temp = colliders[i];
            }
        }
        return temp;
    }

}
