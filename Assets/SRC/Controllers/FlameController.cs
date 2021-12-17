using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameController : MonoBehaviour
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
        if (wtag == tags.NpcInfected)
        {
            NPCController n11r = other.gameObject.GetComponent<NPCController>();
            n11r.Burn();
        }
    }
}
