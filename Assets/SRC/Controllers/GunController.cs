using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


internal enum EnumType
{
    Dart
}
public class GunController : MonoBehaviour
{
    [SerializeField] private EnumType ammoType;
    [SerializeField] private float cooldown_time;
    [SerializeField] private int magazineCapacity;
    [SerializeField] private Transform muzzleTransform;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float reloadSpeed;
    private string bulletType;
    private EventModel eventModel;
    private bool reloading = false;
    private bool coolingDown = false;
    private int currentChamberCount = 0;
    private InventoryUtil inventoryUtil;
    private PickUpModel pickUpModel;
    private GUIController _Gui;
    private NameModel names;

    // Start is called before the first frame update
    void Start()
    {
        pickUpModel = new PickUpModel();
        names = new NameModel();
        eventModel = new EventModel();
        SetItemType();
        GameObject GUIObject = GameObject.Find(names.GUI);
        _Gui = GUIObject.GetComponent<GUIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            Reload();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }


    void OnEnable()
    {
        inventoryUtil = transform.root.gameObject.GetComponent<InventoryUtil>();
    }


    private int CheckInventory()
    {
        return inventoryUtil.GetCountByValue(bulletType);
    }

    private void CoolDown()
    {
        coolingDown = true;
        Action finish_cooldown = ()=>{
            coolingDown = false;
        };

        StartCoroutine(Wait(cooldown_time, finish_cooldown));

    }


    private void Fire()
    {
        if (reloading || coolingDown)
        {
            return;
        }
        else if (currentChamberCount <= 0)
        {
            Reload();
            return;
        }


        Camera camera = Camera.main;
        Vector3 mousePos = Input.mousePosition;
        Vector3 aim = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, muzzleTransform.position.z));
        Vector3 projectile_position = new Vector3(muzzleTransform.position.x,  muzzleTransform.position.y, muzzleTransform.position.z);
        GameObject projectileObject = Instantiate(projectile, projectile_position, muzzleTransform.rotation);
        ProjectileController projectileController = projectileObject.GetComponent<ProjectileController>();
        projectileController.enabled = true;
        PickUpUtil pickUpUtil = projectileObject.GetComponent<PickUpUtil>();
        pickUpUtil.enabled = false;
        currentChamberCount --;
        FMODUnity.RuntimeManager.PlayOneShot(eventModel.Tranq_Gun_Shoot);
        CoolDown();
    }


    private void SetItemType()
    {
        switch(this.ammoType)
        {
            case EnumType.Dart:
                bulletType = pickUpModel.TranqDart;
                break;
            }
        }


    private void Reload()
    {
        if (!reloading)
            {
            reloading = true;
            int bulletsInInventory = CheckInventory();
            int ammountToChamber = 0;
            if (bulletsInInventory <= 0)
            {
                reloading = false;
                return;
            }
            else if (bulletsInInventory <= magazineCapacity && bulletsInInventory > 0)
            {
                ammountToChamber = bulletsInInventory;
            }
            else if (bulletsInInventory > magazineCapacity)
            {
                ammountToChamber = magazineCapacity;
            }
            currentChamberCount = ammountToChamber;
            ItemStruct item = new ItemStruct(bulletType);
            inventoryUtil.RemoveByCount(item, ammountToChamber);
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


    private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }

}
