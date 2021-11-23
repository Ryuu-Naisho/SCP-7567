using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InventoryUtil))]
public class PlayerController : MonoBehaviour
{
    private GUIController _Gui;
    private InventoryUtil inventory;
    private NameModel names;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new InventoryUtil();
        names = new NameModel();
        GameObject GUIObject = GameObject.Find(names.GUI);
        _Gui = GUIObject.GetComponent<GUIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            _Gui.clearDebug();
        }
    }


    public void PickUpItem(string item)
    {
        inventory.Add(item);
        string msg = item + " has been collected.";
        _Gui.writeDebug(msg);
    }


    private void DisposeItem(string item)
    {
        inventory.RemoveOne(item);
    }
}
