using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InventoryUtil))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private Camera overheadCamera;
    [SerializeField] private int MaxFlamethrowerCanisterCapacity;
    [SerializeField] private Transform Shoulder;
    private GUIController _Gui;
    private InventoryUtil inventory;
    private NameModel names;
    private PickUpModel pickUpModel;
    private EventModel eventModel;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<InventoryUtil>();
        names = new NameModel();
        pickUpModel = new PickUpModel();
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
        if (item.IsSCP)
        {
            GameObject scp = item.GetTransform.gameObject;
            scp.transform.parent = Shoulder;
            scp.transform.position = Shoulder.position;
            scp.transform.rotation = Shoulder.rotation;
            //scp.transform.RotateAround (scp.transform.position, transform.up, 180f);
            Rigidbody rigidbody = scp.GetComponent<Rigidbody>();
            Destroy (rigidbody);
            Collider cldr = scp.GetComponent<Collider>();
            cldr.enabled = false;
        }
    }


    public bool ReachedCanisterCap()
    {
        if (inventory.GetCountByValue(pickUpModel.FlamethrowerCanister) >= MaxFlamethrowerCanisterCapacity)
        {
            return true;
        }
        else
        {
            return false;
        }
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
