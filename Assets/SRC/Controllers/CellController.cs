using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CellController : MonoBehaviour
{
    private bool canLock = false;
    private bool collected = false;
    private GUIController _Gui;
    private HintModel hintModel;
    private ItemStruct item;
    private NameModel names;
    private PickUpModel pickUpModel;
    private TagModel tags;


     void Awake()
     {
        tags = new TagModel();
        hintModel = new HintModel();
        names = new NameModel();
        pickUpModel = new PickUpModel();
     }



    // Start is called before the first frame update
    void Start()
    {
        GameObject GUIObject = GameObject.Find(names.GUI);
        _Gui = GUIObject.GetComponent<GUIController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    private void OnTriggerEnter(Collider other)
    {
        string _tag = other.tag;
        if (_tag == tags.Player)
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            item = new ItemStruct(pickUpModel.SCP, false, true);
            if (!playerController.HasItem(item))
                return;

            if (!collected)
            {
                if (_Gui == null)
                {
                   return;
                }
                try
                {
                _Gui.SetHint(hintModel.LockUPSCP);
                canLock = true;
                }
                catch(NullReferenceException e)
                {
                    Debug.Log(e);
                    return;
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
            canLock = false;
            }
            catch(NullReferenceException e)
            {
                Debug.Log(e);
                return;
            }
        }
    }
}
