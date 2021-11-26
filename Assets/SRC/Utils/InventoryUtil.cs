using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(WeaponsManagerController))]
public class InventoryUtil:  MonoBehaviour
{
    private Dictionary<ItemStruct, string> inventory = new Dictionary < ItemStruct, string > ();
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
        inventory[item] = item.Name;
        if (item.IsWeapon)
        {
            AddToWeaponManager(item);
        }
    }


    private void AddToWeaponManager(ItemStruct item)
    {
        weaponsManagerController.AddWeapon(item);
    }


    public ItemStruct GetByValue(string item)
    {
        var _item = inventory.First(pair => pair.Value == item);
        return _item.Key;
    }


    public int GetCountByValue(string value)
    {
        int count = 0;
        foreach (KeyValuePair<ItemStruct, string> _item in inventory)
        {
            if (_item.Key.Name == value)
            {
                count ++;
            }
        }

        return count;
    }


    public bool isCollected(ItemStruct item)
    {
        bool exists = false;
        foreach (KeyValuePair<ItemStruct, string> _item in inventory)
        {
            if (_item.Key.Name == item.Name)
            {
                exists = true;
                break;
            }
        }
        return exists;
    }



    public void RemoveAll(ItemStruct item)
    {
        foreach (KeyValuePair<ItemStruct, string> _item in inventory)
        {
            if (_item.Key.Name == item.Name)
                inventory.Remove(_item.Key);
        }
    }


    public void RemoveOne(ItemStruct item)
    {
        ItemStruct _item = GetByValue(item.Name);
        inventory.Remove(_item);
    }


    public void RemoveByCount(ItemStruct item, int maxCount)
    {
        int count = 0;

        for (int i = 0; i < maxCount; i++)
        {
            RemoveOne(item);
        }
    }



    public void Print()
    {
        foreach(KeyValuePair<ItemStruct, string> item in inventory)
        {
            Debug.Log(item.Value);
        }
    }
}
