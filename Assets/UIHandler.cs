using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    // Sprites for render
    public Sprite m_treeLog;
    public Sprite m_stone;
    public Sprite m_Spear;
    public Sprite m_Sword;
    public Sprite m_Tower;
    public Sprite m_Axe;

    // Use this for initialization
    public Character m_Character;
    public GameObject m_InventoryPanel;
    public GameObject m_CraftPannel;
    public GameObject m_WeaponsPanel;

    const int NUM_ITEM_LIST = 9;
    const int NUM_WEAPONS = 4;
    const int NUM_CRAFT_ITEM = 3;

    // item button of inventory (3x3 button)
    List<GameObject> m_ItemList;

    // crafting ui button 
    List<GameObject> m_CraftItem;

    bool m_isPlacingTower = false;

    bool m_isInventoryOpen = false;

    // the current gameobject for placing around the map
    GameObject m_currentBuilding;

    // to turn on and turn off the inventory
    public GameObject m_InventoryCanvas;

    // weapons ui
    List<GameObject> m_WeaponsUI;

    public GameObject m_HelperCanvas;
    void Start()
    {
        Debug.Assert(m_InventoryPanel);
        Debug.Assert(m_CraftPannel);
        Debug.Assert(m_InventoryCanvas);
        Debug.Assert(m_WeaponsPanel);
        Debug.Assert(m_Axe);

        m_ItemList = new List<GameObject>();
        m_CraftItem = new List<GameObject>();
        m_WeaponsUI = new List<GameObject>();
        for (int i = 0; i < NUM_ITEM_LIST; i++)
        {
            string name = i.ToString();
            Transform temp = m_InventoryPanel.transform.Find(name);
            Debug.Assert(temp);


            m_ItemList.Add(temp.gameObject);

        }

        // set up ui button for easy to change the item image
        // add first craft ui button
        m_CraftItem.Add(m_CraftPannel.transform.Find("0").gameObject);
        // add second craft ui button
        m_CraftItem.Add(m_CraftPannel.transform.Find("1").gameObject);
        // result craft ui button
        m_CraftItem.Add(m_CraftPannel.transform.Find("result").gameObject);

        for (int i = 0; i < NUM_WEAPONS; i++)
        {
            m_WeaponsUI.Add(m_WeaponsPanel.transform.Find(i.ToString()).gameObject);

        }
    }
    public void setImageCraft(int index, int quantity, Sprite sprite)
    {
        if (index >= 0 && index < 3)
        {
            if (quantity > 0 && sprite != null)
            {
                GameObject obj = m_CraftItem[index];
                GameObject t = m_CraftItem[index].transform.Find("Panel").gameObject;

                t.GetComponent<Image>().sprite = sprite;

                Color color = t.GetComponent<Image>().color;
                color.a = 1;

                t.GetComponent<Image>().color = color;
                //set quantity

                GameObject text = obj.transform.Find("Quantity").gameObject;

                text.GetComponent<Text>().text = quantity.ToString();

            }
            else
            {
                GameObject obj = m_CraftItem[index];
                GameObject t = m_CraftItem[index].transform.Find("Panel").gameObject;

                t.GetComponent<Image>().sprite = null;

                Color color = t.GetComponent<Image>().color;
                color.a = 0;

                t.GetComponent<Image>().color = color;
                //set quantity

                GameObject text = obj.transform.Find("Quantity").gameObject;

                text.GetComponent<Text>().text = "";

            }


        }
        else
        {
            Debug.Assert(false);
        }
    }
    public void setImageWeapons(int index, int quantity, Sprite sprite)
    {
        if (index >= 0 && index < NUM_WEAPONS)
        {
            // set image
            GameObject obj = m_WeaponsUI[index];
            GameObject t = m_WeaponsUI[index].transform.Find("Panel").gameObject;


            // if sprite is not null then set image and quantity text
            if (sprite != null && quantity > 0)
            {
                t.GetComponent<Image>().sprite = sprite;

                Color color = t.GetComponent<Image>().color;
                color.a = 1;

                t.GetComponent<Image>().color = color;
                //set quantity

                GameObject text = obj.transform.Find("Quantity").gameObject;

                text.GetComponent<Text>().text = quantity.ToString();
            }
            // otherwise set image to null and quantity to 0
            else if (quantity <= 0 || sprite == null)
            {
                t.GetComponent<Image>().sprite = null;

                Color color = t.GetComponent<Image>().color;
                color.a = 0;

                t.GetComponent<Image>().color = color;
                //set quantity

                GameObject text = obj.transform.Find("Quantity").gameObject;

                text.GetComponent<Text>().text = "";

            }

        }
        else
        {
            Debug.Assert(false);
        }

    }
    public void setImageInventory(int index, int quantity, Sprite sprite)
    {
        if (index >= 0 && index < NUM_ITEM_LIST)
        {
            // set image
            GameObject obj = m_ItemList[index];
            GameObject t = m_ItemList[index].transform.Find("Panel").gameObject;


            // if sprite is not null then set image and quantity text
            if (sprite != null && quantity > 0)
            {
                t.GetComponent<Image>().sprite = sprite;

                Color color = t.GetComponent<Image>().color;
                color.a = 1;

                t.GetComponent<Image>().color = color;
                //set quantity

                GameObject text = obj.transform.Find("Quantity").gameObject;

                text.GetComponent<Text>().text = quantity.ToString();
            }
            // otherwise set image to null and quantity to 0
            else if (quantity <= 0 || sprite == null)
            {
                t.GetComponent<Image>().sprite = null;

                Color color = t.GetComponent<Image>().color;
                color.a = 0;

                t.GetComponent<Image>().color = color;
                //set quantity

                GameObject text = obj.transform.Find("Quantity").gameObject;

                text.GetComponent<Text>().text = "";

            }

        }
        else
        {
            Debug.Assert(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (m_HelperCanvas.GetComponent<Canvas>().enabled)
            mainGame.m_IsPause = true;
        else if (m_isInventoryOpen)
            mainGame.m_IsPause = true;
        else
            mainGame.m_IsPause = false;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_isInventoryOpen)
            {
                m_isInventoryOpen = false;
                m_Character.m_Craft.returnCraftItemBackToInventory();
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            swapWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            swapWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            swapWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            swapWeapon(3);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            m_isInventoryOpen = !m_isInventoryOpen;
            m_InventoryCanvas.GetComponent<Canvas>().enabled = !m_InventoryCanvas.GetComponent<Canvas>().enabled;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
        }
        else if (Input.GetMouseButtonDown(0))
        {
            placeTower();
            throwSpear();
        }
        // this happend when user tries to place the tower by move it around the map
        moveTowerWithMouse();

        // if the inventory doesn't open then dont need to render the images insde the inventory and craft
        for (int i = 0; i < NUM_WEAPONS; i++)
        {
            InventoryItem item = m_Character.m_Weapons.getItem(i);
            switch (item.m_item)
            {
                case ItemType.NONE:
                    setImageWeapons(i, 0, null);
                    break;
                case ItemType.SPEAR:
                    setImageWeapons(i, item.m_Quantity, m_Spear);
                    break;
                case ItemType.SWORD:
                    setImageWeapons(i, item.m_Quantity, m_Sword);
                    break;
                case ItemType.TOWER:
                    setImageWeapons(i, item.m_Quantity, m_Tower);
                    break;
                case ItemType.AXE:
                    setImageWeapons(i, 1, m_Axe);
                    break;
            }

        }
        if (!m_isInventoryOpen) return;
        for (int i = 0; i < NUM_ITEM_LIST; i++)
        {
            InventoryItem item = m_Character.m_inventory.getItem(i);
            switch (item.m_item)
            {
                case ItemType.NONE:
                    setImageInventory(i, 0, null);
                    break;
                case ItemType.WOOD:
                    setImageInventory(i, item.m_Quantity, m_treeLog);
                    break;
                case ItemType.STONE:
                    setImageInventory(i, item.m_Quantity, m_stone);
                    break;
                default:
                    Debug.Assert(false);
                    break;


            }

        }
        for (int i = 0; i < NUM_CRAFT_ITEM; i++)
        {
            InventoryItem item = m_Character.m_Craft.getItem(i);
            switch (item.m_item)
            {
                case ItemType.WOOD:
                    setImageCraft(i, item.m_Quantity, m_treeLog);
                    break;
                case ItemType.STONE:
                    setImageCraft(i, item.m_Quantity, m_stone);
                    break;
                case ItemType.NONE:
                    setImageCraft(i, 0, null);
                    break;
                case ItemType.SPEAR:
                    setImageCraft(i, item.m_Quantity, m_Spear);
                    break;
                case ItemType.SWORD:
                    setImageCraft(i, item.m_Quantity, m_Sword);
                    break;
                case ItemType.TOWER:
                    setImageCraft(i, item.m_Quantity, m_Tower);
                    break;
            }

        }
    }

    void prepareTowerToPlace()
    {
        // cancel placing tower
        if (m_isPlacingTower)
        {
            // if the placing tower still active, then cancel it
            if (m_currentBuilding != null)
            {
                SpriteManage.DESTROY_SPRITE(m_currentBuilding);
                m_currentBuilding = null;
            }
            m_isPlacingTower = false;
        }
        // start placing tower
        else
        {
            m_isPlacingTower = true;
            m_currentBuilding = SpriteManage.CREATE_SPRITE(SpriteType.TOWER);
        }

    }
    void placeTower()
    {
        if (m_isPlacingTower && m_currentBuilding != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_currentBuilding.transform.position = mousePosition;
            if (SpriteManage.canPlaceItem(Camera.main.WorldToScreenPoint(mousePosition), m_currentBuilding))
            {
                m_currentBuilding.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                // change is collider istrigger to false
                m_currentBuilding.GetComponent<BoxCollider2D>().isTrigger = false;
                m_currentBuilding.GetComponent<CircleCollider2D>().isTrigger = true;
                m_currentBuilding.GetComponent<Tower>().m_IsPlaced = true;
                m_currentBuilding = null;
                m_isPlacingTower = false;
                InventoryItem tower = m_Character.m_Weapons.findItem(ItemType.TOWER);
                tower.m_Quantity--;
                m_Character.m_Weapons.setQuantity(tower);
                if (tower.m_Quantity <= 0)
                    m_Character.changeWeapon(0);
            }
            else
            {
                Debug.Log("cant place");
            }


        }

    }
    void moveTowerWithMouse()
    {
        if (m_isPlacingTower && m_currentBuilding != null)
        {
            m_currentBuilding.GetComponent<BoxCollider2D>().isTrigger = true;
            m_currentBuilding.GetComponent<CircleCollider2D>().isTrigger = true;

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_currentBuilding.transform.position = mousePosition;
            if (SpriteManage.canPlaceItem(Camera.main.WorldToScreenPoint(mousePosition), m_currentBuilding))
            {
                m_currentBuilding.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
                Debug.Log("can place");
            }
            else
            {
                m_currentBuilding.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, .5f);
                Debug.Log("cannt place");
            }


        }

    }
    public void itemCraft(Button b)
    {

        Debug.Log("item craft clicked: " + b.name);

    }
    public void itemClicked(Button b)
    {
        int index = int.Parse(b.name);
        //Debug.Log(m_Character.m_inventory.getItem(index).m_item);
        InventoryItem item = m_Character.m_inventory.getItem(index);
        if (item == null)
            return;
        int status = m_Character.m_Craft.addItemToCraftSpot(item);
        if (status == CraftingEngine.ADD_ITEM_TO_SLOT_DIFFERENT_ITEM || status == CraftingEngine.ADD_ITEM_TO_SLOT_SAME_ITEM)
        {
            //item.m_Quantity--;
            m_Character.m_inventory.setQuantity(item, -1);

        }
    }
    // this function should called after the use pressed escape button to exit the crafting ui
    public void acceptCraft()
    {
        m_Character.m_Craft.acceptCraft();
        m_Character.m_Craft.returnCraftItemBackToInventory();
        m_InventoryCanvas.GetComponent<Canvas>().enabled = !m_InventoryCanvas.GetComponent<Canvas>().enabled;
        m_isInventoryOpen = false;
    }

    public void cancelCraft()
    {
        m_Character.m_Craft.returnCraftItemBackToInventory();
        m_InventoryCanvas.GetComponent<Canvas>().enabled = !m_InventoryCanvas.GetComponent<Canvas>().enabled;
        m_isInventoryOpen = false;
    }

    public void swapWeapon(Button button)
    {
        int index = int.Parse(button.name);
        swapWeapon(index);

    }
    public void swapWeapon(int index)
    {
        InventoryItem item = m_Character.m_Weapons.getItem(index);
        if (item != null)
        {
            if (item.m_item == ItemType.TOWER)
            {
                prepareTowerToPlace();
                Debug.Log("start ");

            }
            else
            {
                m_Character.changeWeapon(index);
            }

        }

    }
    void throwSpear()
    {
        if (m_InventoryCanvas.GetComponent<Canvas>().enabled) return;
        Vector2 characterPos = m_Character.transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - characterPos.y, mousePos.x - characterPos.x);
        m_Character.throwSpear(angle);
    }
    public void ToggleHelperMenu()
    {
        Canvas canvas = m_HelperCanvas.GetComponent<Canvas>();
        canvas.enabled = !canvas.enabled;
    }
}
