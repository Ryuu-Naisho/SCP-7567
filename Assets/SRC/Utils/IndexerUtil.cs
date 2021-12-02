using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexerUtil
{


    public Transform InView(Transform viewTransform, int FOV, int radius)
    {
        Transform tInView = null;
        RaycastHit hit;
        if (Physics.SphereCast(viewTransform.position, radius, viewTransform.forward, out hit, FOV))
        {
            Debug.Log(hit.transform.tag);
            tInView = hit.transform;
        }

        return tInView;
    }
}
