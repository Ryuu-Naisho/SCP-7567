using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexerUtil
{


    public Transform InView(Transform viewTransform, int FOV, float radius)
    {
        Transform tInView = null;
        RaycastHit hit;
        if (Physics.SphereCast(viewTransform.position, radius/2, viewTransform.forward, out hit, FOV))
        {
            tInView = hit.transform;
        }

        return tInView;
    }
}
