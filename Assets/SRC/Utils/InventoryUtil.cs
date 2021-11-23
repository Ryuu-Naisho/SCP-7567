using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUtil
{
    private Dictionary<string, int> inventory = new Dictionary < string, int > ();


    public InventoryUtil()
    {
    }


    public void Add(string key)
    {
        int value = 0;
        if (isCollected(key))
        {
            value = inventory[key];
        }
        value ++;


        inventory[key] = value;
    }


    public void RemoveAll(string key)
    {
        inventory.Remove(key);
    }


    public void RemoveOne(string key)
    {
        int value = 0;
        if (isCollected(key))
        {
            value = inventory[key];
            value --;
            inventory[key] = value;
        }

        if (value <= 0)
            RemoveAll(key);
    }


    public bool isCollected(string key)
    {
        bool exists = inventory.ContainsKey(key);
        return exists;
    }
}
