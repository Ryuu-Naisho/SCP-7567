using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerController : MonoBehaviour
{
    [SerializeField] private float fuelLevel;
    [SerializeField] private float fuelBurnRate;
    private EventModel eventModel;
    private FMOD.Studio.EventInstance instance;
    private bool soundPlaying = false;



    // Start is called before the first frame update
    void Start()
    {
        eventModel = new EventModel();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (soundPlaying)
                StopSound();
        }


        Debug.Log(fuelLevel);
    }



    private void Fire()
    {
        if (fuelLevel <= 0)
            return;
        StartSound();
        StartCoroutine(BurnFuel(fuelBurnRate));
    }


    private void StartSound()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(eventModel.Flamethrower_Shoot);
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.gameObject));
        instance.start();
        soundPlaying = true;
    }


    private void StopSound()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
        soundPlaying = false;
    }


    private IEnumerator BurnFuel(float time)
    {
        yield return new WaitForSeconds(time);
        fuelLevel --;
        if (soundPlaying)
            StopSound();
    }
}
