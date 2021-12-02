using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathUtil
{


    public Vector3 GetDestination()
    {
        Vector3 destination = Vector3.zero;
        Vector3 direction = GetDirection();
        int steps = GetSteps();
        destination = direction * steps;
        return destination;
    }



    private Vector3 GetDirection()
    {
        int index = Random.Range(1,5);
        Vector3 direction = Vector3.zero;
        switch(index)
        {
            case 1:
                direction = Vector3.forward;
                break;
            case 2:
                direction = Vector3.left;
                break;
            case 3:
                direction = Vector3.right;
                break;
            case 4:
                direction = Vector3.back;
                break;
        }
        return direction;
    }


    private int GetSteps()
    {
        int ceil = 15;
        int steps  = Random.Range(1,ceil + 1);
        return ceil;
    }
}
