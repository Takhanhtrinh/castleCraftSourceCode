using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory
{
    const int MAX_ITEM = 9;
    List<InventoryItem> m_items;
    public Inventory()
    {
        m_items = new List<InventoryItem>();
        for (int i = 0; i < MAX_ITEM; i++)
        {
            m_items.Add(new InventoryItem());
        }
        if(Debug.isDebugBuild)
        {
                    //m_items[0] = new InventoryItem(ItemType.WOOD, 20);
                    //m_items[1] = new InventoryItem(ItemType.STONE, 20);

        }

    }
    public void setQuantity(InventoryItem item, int quantity)
    {
        for (int i = 0; i < m_items.Count; i++)
        {
            if (m_items[i].m_item == item.m_item)
            {
                if (m_items[i].m_Quantity + quantity <= 0)
                {
                    Debug.Log("update item current quantity: " + m_items[i].m_Quantity + " update quantity: " + quantity);
                    m_items[i] = new InventoryItem();
                }
                else
                {
                        m_items[i].m_Quantity += quantity;
                }
                return;
            }

        }
        Debug.Log("not found item\n");
    }
    public InventoryItem getItem(int index)
    {
        if (index >= 0 && index < 9)
        {
            return m_items[index];
        }
        else
            return null;
    }
    public void addNewItem(InventoryItem item)
    {
        // find if the item is exist in the list, if found 1 then increase the quantity
        for (int i = 0; i < m_items.Count; i++)
        {
            if (m_items[i].m_item == item.m_item)
            {
                m_items[i].m_Quantity += item.m_Quantity;
                Debug.Log("set item quantity");
                return;
            }

        }
        // otherwise add to the inventory
        for (int i = 0; i < m_items.Count; i++)
        {
            if (m_items[i].m_item == ItemType.NONE)
            {
                m_items[i] = item;
                Debug.Log("put new item");
                return;
            }
        }

    }
    public void addNewItem(ItemType item)
    {
        // find if the item is exist in the list, if found 1 then increase the quantity
        for (int i = 0; i < m_items.Count; i++)
        {
            if (m_items[i].m_item == item)
            {
                m_items[i].m_Quantity++;
                return;
            }

        }
        // otherwise add to the inventory
        for (int i = 0; i < m_items.Count; i++)
        {
            if (m_items[i].m_item == ItemType.NONE)
            {
                m_items[i] = new InventoryItem(item);
                return;
            }
        }

    }

}