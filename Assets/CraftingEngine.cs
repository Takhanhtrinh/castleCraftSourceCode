using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingEngine
{

	public static int ADD_ITEM_TO_SLOT_NULL = -1;
	public static int ADD_ITEM_TO_SLOT_NONE_ITEM = 0;
	public static int ADD_ITEM_TO_SLOT_SAME_ITEM = 1;
	public static int ADD_ITEM_TO_SLOT_DIFFERENT_ITEM = 2;
	public static int ADD_ITEM_TO_SLOT_FULL = 3;
	
	public static int NUMBER_STONE_PER_SWORD = 4;
	public static int NUMBER_WOOD_PER_TOWER= 10;
	public static int NUMBER_STONE_PER_TOWER = 10;
	// items craft 
	public List<InventoryItem> m_items;
	public Inventory m_Inventory;
	public WeaponManage m_Weapons;

   public CraftingEngine() 
    {
		m_items = new List<InventoryItem>();
		// 2 spots for crafting 
		for (int i = 0 ; i < 2; i++)
		{
			m_items.Add(new InventoryItem());
		}

    }

    // Update is called once per frame
	public int addItemToCraftSpot(InventoryItem item)
	{
		if (item == null) return ADD_ITEM_TO_SLOT_NULL;
		if (item.m_item == ItemType.NONE) return ADD_ITEM_TO_SLOT_NONE_ITEM;
		// if found a same item, then just add the quantity
		for (int i =0; i < m_items.Count; i++)
		{
			if (m_items[i].m_item == item.m_item)
			{
				m_items[i].m_Quantity += 1;
				Debug.Log("add same item to craft slot");
				return ADD_ITEM_TO_SLOT_SAME_ITEM;
			}
		}
		// if cant find same item, then just add a new item to the slot
		for (int i = 0; i < m_items.Count; i++)
		{
			if (m_items[i].m_item == ItemType.NONE)
			{
				m_items[i].m_item = item.m_item;
				m_items[i].m_Quantity = 1;
				Debug.Log("add different item to craft slot");
				return ADD_ITEM_TO_SLOT_DIFFERENT_ITEM;
			}

		}
				Debug.Log("item craft slot is full");
		// otherwise the slot is full 
		return ADD_ITEM_TO_SLOT_FULL;

	}
	public InventoryItem getItem(int index)
	{
		if (index >= 0 && index < 3)
		{
			if (index  < 2) return m_items[index];
			else 
			{
				return Craft();
			}

		}
		Debug.Assert(false);
		return null;
		
	}
    public InventoryItem Craft()
    {
        if (m_items[0].m_item == ItemType.NONE && m_items[1].m_item == ItemType.NONE) return new InventoryItem();

        else
		{
			List<InventoryItem> tempList = new List<InventoryItem>(m_items);
            tempList.Sort((x, y) => x.m_item.CompareTo(y.m_item));
			// if crafting engine has only one item
			if (tempList[0].m_item == ItemType.NONE)
			{
				// if it is a wood resouce
				if (tempList[1].m_item == ItemType.WOOD)
				{
					InventoryItem temp = new InventoryItem(ItemType.SPEAR);
					temp.m_Quantity = tempList[1].m_Quantity;
					return temp;
				}
				else if (tempList[1].m_item == ItemType.STONE)
				{
					int quantity = tempList[1].m_Quantity;
					int numberOfSword = quantity / NUMBER_STONE_PER_SWORD;
					// if user doesn't have enough stone to craft a sword  then return none item
					if (numberOfSword <= 0)
					{
						return new InventoryItem();
					}
					else 
					{
						numberOfSword = 1;
						if (m_Weapons.findItem(ItemType.SWORD) != null)
						{
							return new InventoryItem();
						}
						else 
							return new InventoryItem(ItemType.SWORD, numberOfSword);
					}

				}

			}
			// if both slots are not empty
			else if (tempList[0].m_item == ItemType.WOOD)
			{
				int quantity1 = tempList[0].m_Quantity;
				if (tempList[1].m_item == ItemType.STONE)
				{
					int quantity2 = tempList[1].m_Quantity;
					// check if we have enough resouce to craft a castle
					if (quantity1 >= NUMBER_WOOD_PER_TOWER && quantity2 >= NUMBER_STONE_PER_TOWER)
					{
						// calcuate number of castle 
						int numberCastle = Math.Min(quantity1 / NUMBER_WOOD_PER_TOWER, quantity2 / NUMBER_STONE_PER_TOWER);
						return new InventoryItem(ItemType.TOWER, numberCastle);
					}
					// if don't have enough resouce then return none item
					else 
					{
						return new InventoryItem();
					}
				}

			}
		}
		// otherwise return NONE
		return new InventoryItem();
    }
	public void reset()
	{
		for (int i = 0; i < 2; i++)
		{
			m_items[i] = new InventoryItem();	
		}
	}
    public void returnCraftItemBackToInventory()
    {
        // there are 2 slots for crafting 
        for (int i = 0; i < 2; i++)
        {
            InventoryItem item = getItem(i);
            m_Inventory.addNewItem(item);

        }
        reset();
    }
	InventoryItem findItem(ItemType type)
	{
		for (int i = 0; i < 2; i++)
		{
			if (m_items[i].m_item == type)
			{
				return m_items[i];
			}
		}
		return null;
	}
	// return index
	int getItemIndex(ItemType type)
	{
		for (int i = 0; i < 2; i++)
		{
			if (m_items[i].m_item == type)
			{
				return i;
			}
		}
		return -1;
	}
	public void acceptCraft()
	{
		InventoryItem craftedItem = Craft();
		if (craftedItem.m_item == ItemType.NONE) return;
		else if (craftedItem.m_item == ItemType.SPEAR)
		{
			InventoryItem woods = findItem(ItemType.WOOD);
			Debug.Assert(woods != null);
			int index = getItemIndex(ItemType.WOOD);
			Debug.Assert(index >= 0);
			m_items[index] = new InventoryItem();
			m_Weapons.addNewWeapon(craftedItem);
		}
		else if (craftedItem.m_item == ItemType.SWORD)
		{
			InventoryItem stones = findItem(ItemType.STONE);
			Debug.Assert(stones != null);
			int index = getItemIndex(ItemType.STONE);
			Debug.Assert(index >= 0);
			m_items[index] = new InventoryItem();
			m_Weapons.addNewWeapon(craftedItem);
		}
		else if (craftedItem.m_item == ItemType.TOWER)
		{
			InventoryItem stones = findItem(ItemType.STONE);
			InventoryItem woods = findItem(ItemType.WOOD);
			int stoneIndex = getItemIndex(ItemType.STONE);
			int woodIndex = getItemIndex(ItemType.WOOD);
			Debug.Assert(stoneIndex >=0 );
			Debug.Assert(woodIndex >= 0);
			// update new resouces to craft slot when there is more than resouces to craft the item
			if (stones.m_Quantity > NUMBER_STONE_PER_TOWER) 
				stones.m_Quantity %= NUMBER_STONE_PER_TOWER;
			else 
				m_items[stoneIndex] = new InventoryItem();

			if (woods.m_Quantity > NUMBER_WOOD_PER_TOWER)
				woods.m_Quantity %= NUMBER_WOOD_PER_TOWER;
			else
				m_items[woodIndex] = new InventoryItem();
			m_Weapons.addNewWeapon(craftedItem);
		}

	}

}
