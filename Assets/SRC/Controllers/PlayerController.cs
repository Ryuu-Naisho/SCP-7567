using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InventoryUtil))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private Camera overheadCamera;
    private GUIController _Gui;
    private InventoryUtil inventory;
    private NameModel names;
    private EventModel eventModel;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<InventoryUtil>();
        names = new NameModel();
        eventModel = new EventModel();
        GameObject GUIObject = GameObject.Find(names.GUI);
        _Gui = GUIObject.GetComponent<GUIController>();
        ShowFirstPersonView();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            _Gui.clearDebug();
        }
        else if (Input.GetKeyDown("p"))
        {
            inventory.Print();
        }
        else if (Input.GetKeyDown("z"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(eventModel.ConcreteStep);
        }
    }


    public ItemStruct GetFromInventory(string _item)
    {
        ItemStruct item = inventory.GetByValue(_item);
        return item;
    }


    public bool HasItem(ItemStruct item)
    {
        return inventory.isCollected(item);
    }


    public void PickUpItem(ItemStruct item)
    {
        inventory.Add(item);
        string msg = item.ToString() + " has been collected.";
        _Gui.writeDebug(msg);
    }


    private void DisposeItem(ItemStruct item)
    {
        inventory.RemoveOne(item);
    }


    private void ShowOverheadView() {
        firstPersonCamera.enabled = false;
        overheadCamera.enabled = true;
    }


    private void ShowFirstPersonView() {
        firstPersonCamera.enabled = true;
        overheadCamera.enabled = false;
    }
}
