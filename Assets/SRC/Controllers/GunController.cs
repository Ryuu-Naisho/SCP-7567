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
    [SerializeField] private int magazineCapacity;
    [SerializeField] private float reloadSpeed;
    private string bulletType;
    private bool canFire = true;
    private int currentChamberCount = 0;
    private InventoryUtil inventoryUtil;
    private PickUpModel pickUpModel;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        pickUpModel = new PickUpModel();
        SetItemType();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            Reload();
        }
    }


    void OnEnable()
    {
        playerController = transform.root.gameObject.GetComponent<PlayerController>();
        inventoryUtil = transform.root.gameObject.GetComponent<InventoryUtil>();
    }


    private int CheckInventory()
    {
        return inventoryUtil.GetCountByValue(bulletType);
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
        if (canFire)
            {
            canFire = false;
            int bulletsInInventory = CheckInventory();
            Debug.Log(bulletsInInventory);
            int ammountToChamber = 0;
            if (bulletsInInventory <= 0)
            {
                canFire = true;
                return;
            }
            else if (bulletsInInventory < magazineCapacity && bulletsInInventory > 0)
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

            Action startFire = ()=>
            {
                canFire = true;
            };


            StartCoroutine(Wait(reloadSpeed, startFire));
        }
    }


    private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }

}
