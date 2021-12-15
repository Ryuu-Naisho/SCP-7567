using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfHandController : MonoBehaviour
{
    private TagModel tags;

    // Start is called before the first frame update
    void Start()
    {
        tags = new TagModel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        string wtag = other.gameObject.tag;
        if (wtag == tags.NPCNonInfected)
        {
            NINPCController ninpc = other.gameObject.GetComponent<NINPCController>();
            ninpc.Convert();
            NPCController _root = transform.root.GetComponent<NPCController>();
            _root.SwitchIdle();
        }
        else if (wtag == tags.Player)
        {
            //TODO player become infected.
        }
    }
}
