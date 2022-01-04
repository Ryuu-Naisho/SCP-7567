using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimOnStartSwapObjs : MonoBehaviour
{
    public List<GameObject> ObjsToDisable;
    public List<GameObject> ObjsToEnable;
    void Start()
    {
        foreach (GameObject OE in ObjsToEnable)
        {
            OE.SetActive(true);
        }

        foreach (GameObject OD in ObjsToDisable)
        {
            OD.SetActive(false);
        }
    }
}
