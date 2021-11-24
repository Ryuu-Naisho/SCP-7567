using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct ItemStruct
{
    public ItemStruct(string name, bool isWeapon)
    {
        Name = name;
        IsWeapon = isWeapon;
        GetTransform = null;
    }

    public string Name { get; }
    public bool IsWeapon { get; set; }
    public Transform GetTransform {get; set;}
    public override string ToString() => Name;
}