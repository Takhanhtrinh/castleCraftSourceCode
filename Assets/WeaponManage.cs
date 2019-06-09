using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManage {
	// max weapon slot
	const int MAX_WEAPON = 4;
	List<InventoryItem> m_weapons;

	// Use this for initialization
	public WeaponManage() {
		m_weapons = new List<InventoryItem>();
		for (int i = 0; i < MAX_WEAPON; i++)
		{
			InventoryItem item = new InventoryItem();
			m_weapons.Add(item);
		}
		
	}
	
	public void addNewWeapon(ItemType weapon)
	{
		// if found weapon add 1 for quantity
		for (int i = 0; i < m_weapons.Count; i++)
		{
			if (m_weapons[i].m_item == weapon)
			{
				m_weapons[i].m_Quantity++;
				return;
			}
		}
		for (int i = 0 ; i < m_weapons.Count; i++)
		{
			if (m_weapons[i].m_item == ItemType.NONE)
			{
				m_weapons[i] = new InventoryItem(weapon, 1);
				return;
			}
		}
	}
	public void addNewWeapon(InventoryItem item)
	{
		// if found weapon add 1 for quantity
		for (int i = 0; i < m_weapons.Count; i++)
		{
			if (m_weapons[i].m_item == item.m_item)
			{
				m_weapons[i].m_Quantity += item.m_Quantity;
				return;
			}
		}

		for (int i = 0 ; i < m_weapons.Count; i++)
		{
			if (m_weapons[i].m_item == ItemType.NONE)
			{
				m_weapons[i] = item;
				return;
			}
		}

	}
	public void setQuantity(InventoryItem item)
	{
		for (int i = 0; i < MAX_WEAPON; i++)
		{
			if (m_weapons[i].m_item == item.m_item)
			{
				if(item.m_Quantity > 0)
					m_weapons[i] = item;
				else 
					m_weapons[i] = new InventoryItem();
				return;
			}
		}
	}
	public InventoryItem findItem(ItemType item)
	{
		for (int i = 0; i < m_weapons.Count; i++)
		{
			if (m_weapons[i].m_item == item)
			{
				return m_weapons[i];

			}
		}
		return null;

	}
	public InventoryItem getItem(int index)
	{
		Debug.Assert(index >= 0 && index < MAX_WEAPON);
		return m_weapons[index];
	}
	public int getWeaponSize()
	{
		return m_weapons.Count;
	}
	
}
