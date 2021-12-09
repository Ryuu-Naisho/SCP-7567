using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FootController : MonoBehaviour
{
    private TagModel tags;
    private float distance = 0.12f;
    private EventModel eventModel;
    private bool isStepping = false;
    private FMOD.Studio.EventInstance instance;
    private float yOffset = 0.0005f;
    private float footRadius = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        eventModel = new EventModel();
        tags = new TagModel();
    }


    // Update is called once per frame
void Update()
{
    string hitTag = DetectGround(Vector3.zero);
    if (hitTag != null)
    {
        OnFound(hitTag);
        return;
    }

    const int rays = 10;
    for (int i = 0; i < rays; ++i)
    {
        float angle = (360.0f / rays) * i;
        Vector3 posOffset = Quaternion.AngleAxis(angle, Vector3.up) * (Vector3.forward * footRadius);

        hitTag = DetectGround(posOffset);
        if (hitTag != null)
        {
            OnFound(hitTag);
            return;
        }
        else
        {
            isStepping = false;
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance.release();
        }
    }
}

void OnFound(string tag)
{
    if (isStepping)
        return;
    if(tag == tags.Floor)
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(eventModel.ConcreteStep);
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.root.gameObject));
        instance.start();
        isStepping = true;
    }
}

string DetectGround(Vector3 posOffset)
{
    RaycastHit hit;
    Ray footstepRay = new Ray(transform.position + posOffset + (Vector3.up * yOffset), Vector3.down);

    if(Physics.Raycast(footstepRay, out hit, distance + yOffset, LayerMask.GetMask("Ground", "Platform")))
    {
        return hit.collider.tag;
    }
    return null;
}
}