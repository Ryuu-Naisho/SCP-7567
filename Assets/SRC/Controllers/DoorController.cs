using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private TagModel tags;
    private Transform doorTransform;
    // Start is called before the first frame update
    void Start()
    {
        tags = new TagModel();
        doorTransform = this.gameObject.transform.Find("LabDoor");
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
            doorTransform.Rotate(0.0f, 90.0f, 0.0f, Space.World);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        string _tag = other.tag;
        if (_tag == tags.Player)
        {
            doorTransform.Rotate(0.0f, -90.0f, 0.0f, Space.World);
        }
    }
}
