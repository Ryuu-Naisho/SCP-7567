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


    private bool AllowThrough(string _tag)
    {
        bool _allow = false;
        if (_tag == tags.Player || _tag == tags.NpcInfected|| _tag == tags.NPCNonInfected)
        {
            _allow = true;
        }

        return _allow;

    }



    private void OnTriggerEnter(Collider other)
    {
        string _tag = other.tag;
        if (AllowThrough(_tag))
        {
            doorTransform.Rotate(0.0f, 90.0f, 0.0f, Space.World);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        string _tag = other.tag;
        if (AllowThrough(_tag))
        {
            doorTransform.Rotate(0.0f, -90.0f, 0.0f, Space.World);
        }
    }
}
