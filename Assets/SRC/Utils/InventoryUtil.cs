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
        var _item = inventory.First(pair => pair.Value == item.Name);
        inventory.Remove(_item.Key);
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


    public void Print()
    {
        foreach(KeyValuePair<ItemStruct, string> item in inventory)
        {
            Debug.Log(item.Value);
        }
    }
}
