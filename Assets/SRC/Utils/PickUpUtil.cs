using System.Collections;
using System.Collections.Generic;
using UnityEngine;
internal enum EnumItemType
{
    ArmoryKey,
    Flamethrower,
    TranqDart,
    TranquilizerGun,
    Note,
    TapeRecorder
}

public class PickUpUtil : MonoBehaviour
{
    [SerializeField] private EnumItemType enumItemType;
    private bool canPickUp = false;
    private GUIController _Gui;
    private HintModel hintModel;
    private string itemType;
    private PickUpModel pickUpModel;
    private PlayerController playerController;
    private TagModel tags;
    private NameModel names;
    // Start is called before the first frame update
    void Start()
    {
        tags = new TagModel();
        names = new NameModel();
        pickUpModel = new PickUpModel();
        hintModel = new HintModel();
        GameObject playerObject = GameObject.Find(names.Player);
        playerController = playerObject.GetComponent<PlayerController>();
        SetItemType();
        GameObject GUIObject = GameObject.Find(names.GUI);
        _Gui = GUIObject.GetComponent<GUIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickUp)
        {
            if (Input.GetKeyDown("e"))
            {
                playerController.PickUpItem(itemType);
                Destroy(gameObject);
            }
        }   
    }


    private void SetItemType()
    {
        switch(this.enumItemType)
        {
            case EnumItemType.ArmoryKey:
                itemType = pickUpModel.ArmoryKey;
                break;
            case EnumItemType.Flamethrower:
                itemType = pickUpModel.Flamethrower;
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
                itemType = pickUpModel.TranquilizerGun;
                break;
                
            }
        }


    private void OnTriggerEnter(Collider other)
    {
        string _tag = other.tag;
        if (_tag == tags.Player)
        {
            _Gui.SetHint(hintModel.PressEToPickUP);
            canPickUp = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        string _tag = other.tag;
        if (_tag == tags.Player)
        {
            _Gui.clearHint();
            canPickUp = false;
        }
    }
}
