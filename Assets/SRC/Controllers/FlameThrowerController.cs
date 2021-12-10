using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerController : MonoBehaviour
{
    [SerializeField] private float fuelLevel;
    [SerializeField] private float fuelBurnRate;
    [SerializeField] private float reloadSpeed;
    private EventModel eventModel;
    private bool firing = false;
    private bool burningFuel = false;
    private GUIController _Gui;
    private FMOD.Studio.EventInstance instance;
    private InventoryUtil inventoryUtil;
    private float maxFuelLevel;
    private NameModel names;
    private PickUpModel pickUpModel;
    private bool reloading = false;
    private bool soundPlaying = false;



    // Start is called before the first frame update
    void Start()
    {
        eventModel = new EventModel();
        pickUpModel = new PickUpModel(); 
        maxFuelLevel = fuelLevel;
        fuelLevel = 0;
        names = new NameModel();
        GameObject GUIObject = GameObject.Find(names.GUI);
        _Gui = GUIObject.GetComponent<GUIController>();
    }


    void OnEnable()
    {
        inventoryUtil = transform.root.gameObject.GetComponent<InventoryUtil>();
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
            firing = false;
        }
        else if (Input.GetKeyDown("r"))
        {
            Reload();
        }


        if (firing)
        {
            if (fuelLevel <= 0)
            {
                ceaseFire();
            }
            else
            {
                if (!burningFuel)
                {
                    Action BurnFuel = ()=>
                    {
                        fuelLevel --;
                        burningFuel = false;
                    };
                    StartCoroutine(Wait(fuelBurnRate, BurnFuel));
                    burningFuel = true;
                }
            }
        }
    }


    private void ceaseFire()
    {
        if (soundPlaying)
            StopSound();
        firing = false;
    }


    private int CheckInventory()
    {
        return inventoryUtil.GetCountByValue(pickUpModel.FlamethrowerCanister);
    }



    private void Fire()
    {
        if (fuelLevel <= 0)
        {
            Reload();
            return;
        }
        else if (reloading)
        {
            return;
        }

        if (!soundPlaying)
            StartSound();
        if (!firing)
            firing = true;
    }


    public float GetFuelLevel()
    {
        return fuelLevel;
    }


    private void Reload()
    {
        if (!reloading)
            {
            reloading = true;
            int canistersInInventory = CheckInventory();
            if (canistersInInventory <= 0)
            {
                reloading = false;
                return;
            }
            else 
            {
                fuelLevel = maxFuelLevel;
            }
            ItemStruct item = new ItemStruct(pickUpModel.FlamethrowerCanister);
            inventoryUtil.RemoveOne(item);
            FMODUnity.RuntimeManager.PlayOneShot(eventModel.Tranq_Gun_Reload);

            Action startFire = ()=>
            {
                reloading = false;
                _Gui.writeDebug("Reloading complete.");
            };
            _Gui.writeDebug("Reloading...");
            StartCoroutine(Wait(reloadSpeed, startFire));
        }
    }


    private void StartSound()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(eventModel.Flamethrower_Shoot);
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.root.gameObject));
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, transform.root, transform.root.GetComponent<Rigidbody>());
        instance.start();
        soundPlaying = true;
    }


    private void StopSound()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
        soundPlaying = false;
    }



    private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}
