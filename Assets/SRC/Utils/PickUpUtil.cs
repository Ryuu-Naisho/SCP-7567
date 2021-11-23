using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpUtil : MonoBehaviour
{
    private bool canPickUp = false;
    private PlayerController playerController;
    private TagModel tags;
    private NameModel names;
    // Start is called before the first frame update
    void Start()
    {
        tags = new TagModel();
        names = new NameModel();
        GameObject playerObject = GameObject.Find(names.Player);
        playerController = playerObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickUp)
        {
            if (Input.GetButtonDown("e"))
            {
                playerController.PickUpItem(name);
            }
        }   
    }


    private void OnTriggerEnter(Collider other)
    {
        string _tag = other.tag;
        if (_tag == tags.Player)
        {
            canPickUp = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        string _tag = other.tag;
        if (_tag == tags.Player)
        {
            //TODO display on GUi to press e.
            canPickUp = false;
        }
    }
}
