using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(WeaponsManagerController))]
public class InventoryUtil:  MonoBehaviour
{
    private Dictionary<ItemStruct, int> inventory = new Dictionary < ItemStruct, int > ();
    private PickUpModel pickUpModel;
    private WeaponsManagerController weaponsManagerController;



    void Start()
    {
        weaponsManagerController = GetComponent<WeaponsManagerController>();
        pickUpModel = new PickUpModel();
    }

    void Update()
    {

    }


    public void Add(ItemStruct item)
    {
        int value = 0;
        if (isCollected(item))
        {
            value = inventory[item];
        }
        value ++;
        inventory[item] = value;


        if (item.IsWeapon)
        {
            AddToWeaponManager(item);
        }
    }


    private void AddToWeaponManager(ItemStruct item)
    {
        weaponsManagerController.AddWeapon(item);
    }



    public void RemoveAll(ItemStruct item)
    {
        inventory.Remove(item);
    }


    public void RemoveOne(ItemStruct item)
    {
        int value = 0;
        if (isCollected(item))
        {
            value = inventory[item];
            value --;
            inventory[item] = value;
        }

        if (value <= 0)
            RemoveAll(item);
    }


    public bool isCollected(ItemStruct item)
    {
        bool exists = inventory.ContainsKey(item);
        return exists;
    }


    public void Print()
    {
        foreach(KeyValuePair<ItemStruct, int> item in inventory)
        {
            Debug.Log(item.Key.Name + " Count: " + item.Value);
        }
    }
}
