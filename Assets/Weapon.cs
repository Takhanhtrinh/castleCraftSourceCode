using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class Weapon: MonoBehaviour
{
    protected int m_Damage;
    void Start()
    {
        

    }
    public static int getWeaponDamage(ItemType type)
    {
        if (type == ItemType.AXE) 
        {
            return 10;
        }
        else if (type == ItemType.SWORD)
        {
            return 20;
        }
        else 
        return 0;
    }
    void Update()
    {
    }
}
