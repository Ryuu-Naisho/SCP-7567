using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct ItemStruct
{
    public ItemStruct(string name, bool isWeapon=false, bool isSCP=false)
    {
        Name = name;
        IsWeapon = isWeapon;
        IsSCP = isSCP;
        GetTransform = null;
    }

    public string Name { get; }
    public bool IsSCP {get; set;}
    public bool IsWeapon { get; set; }
    public Transform GetTransform {get; set;}
    public override string ToString() => Name;
}