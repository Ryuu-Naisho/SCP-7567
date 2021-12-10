using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
internal enum EnumItemType
{
    ArmoryKey,
    Flamethrower,
    FlamethrowerCanister,
    TranqDart,
    TranquilizerGun,
    Note,
    TapeRecorder
}



public class PickUpUtil : MonoBehaviour
{
    [SerializeField] private EnumItemType enumItemType;
    private bool canPickUp = false;
    private bool collected = false;
    private EventModel eventModel;
    private GUIController _Gui;
    private HintModel hintModel;
    private bool isWeapon = false;
    private ItemStruct itemStruct;
    private string itemType;
    private PickUpModel pickUpModel;
    private PlayerController playerController;
    private string soundEvent;
    private TagModel tags;
    private NameModel names;


         void Awake()
     {
        tags = new TagModel();
        names = new NameModel();
        pickUpModel = new PickUpModel();
        hintModel = new HintModel();
        eventModel = new EventModel();
     }
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.Find(names.Player);
        playerController = playerObject.GetComponent<PlayerController>();
        SetItemType();
        GameObject GUIObject = GameObject.Find(names.GUI);
        _Gui = GUIObject.GetComponent<GUIController>();


        itemStruct = new ItemStruct(itemType, isWeapon);
        itemStruct.GetTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickUp)
        {
            if (Input.GetKeyDown("e"))
            {
                playerController.PickUpItem(itemStruct);
                DoSound();
                _Gui.clearHint();
                if (!isWeapon)
                    Destroy(gameObject);
                collected = true;
                canPickUp = false;
            }
        }   
    }


    private void DoSound()
    {
        if (soundEvent == null)
            return;
        FMODUnity.RuntimeManager.PlayOneShot(soundEvent);
    }


    private void SetItemType()
    {
        switch(this.enumItemType)
        {
            case EnumItemType.ArmoryKey:
                itemType = pickUpModel.ArmoryKey;
                soundEvent = eventModel.Keycard_pickup;
                break;
            case EnumItemType.Flamethrower:
                isWeapon = true;
                itemType = pickUpModel.Flamethrower;
                soundEvent = eventModel.Flamethrower_Pickup;
                break;
            case EnumItemType.FlamethrowerCanister:
                itemType = pickUpModel.FlamethrowerCanister;
                break;
            case EnumItemType.Note:
                itemType = pickUpModel.Note;
                break;
            case EnumItemType.TapeRecorder:
                itemType = pickUpModel.TapeRecorder;
                break;
            case EnumItemType.TranqDart:
                itemType = pickUpModel.TranqDart;
                break;
            case EnumItemType.TranquilizerGun:
                isWeapon = true;
                itemType = pickUpModel.TranquilizerGun;
                soundEvent = eventModel.Tranq_Gun_Pickup;
                break;
                
            }
        }


    private void OnTriggerEnter(Collider other)
    {
        string _tag = other.tag;
        if (_tag == tags.Player)
        {
            if (!collected)
            {
                if (_Gui == null)
                {
                   return;
                }
                try
                {
                _Gui.SetHint(hintModel.PressEToPickUP);
                }
                catch(NullReferenceException e)
                {
                    Debug.Log(e);
                    return;
                }
                canPickUp = true;
                if (isWeapon)
                {
                    if (playerController.HasItem(itemStruct))
                    {
                        canPickUp = false;
                    }
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        string _tag = other.tag;
        if (_tag == tags.Player)
        {
            if (_Gui == null)
            {
               return;
            }
            try
            {
            _Gui.clearHint();
            }
            catch(NullReferenceException e)
            {
                Debug.Log(e);
                return;
            }
            canPickUp = false;
        }
    }
}
