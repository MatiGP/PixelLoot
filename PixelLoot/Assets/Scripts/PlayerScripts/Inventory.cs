﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public InventoryUI ui;

    public GameObject weaponHolder;
    public Item_SO[] items;
    


    private void Awake()
    {
        instance = this;
        ui = GetComponentInChildren<InventoryUI>();
        items = new Item_SO[5];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool addItem(Item_SO item)
    {
        for(int i = 0; i < 5; i++)
        {
            if(items[i] == null)
            {
                items[i] = item;
                ui.UpdateUI();
                return true;

            }
            
        }
        return false;
    }

    public void useItem(int itemIndex)
    {       
        items[itemIndex]?.Use();
        if (items[itemIndex] is Weapon_SO)
        {
            Debug.Log("Equipped a weapon : " + items[itemIndex].itemName);
            //weaponHolder.GetComponent<WeaponDamage>().UpdateWeaponDamage();
        }
        items[itemIndex]= null;
        ui.UpdateUI();
    }
}
