using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManagerController : MonoBehaviour
{
    [SerializeField] private Transform hTransform;
    private GameObject primary;
    private GameObject secondary;
    private PickUpModel pickUpModel;


    // Start is called before the first frame update
    void Start()
    {
        pickUpModel = new PickUpModel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            SwitchWeapon();
        }
    }


    public void AddWeapon(ItemStruct weapon)
    {
        //TODO add to hand.
        if (primary != null)
        {
            GameObject tempHolder;
            tempHolder = primary;
            SetAsSecondary(tempHolder);
        }
        SetAsPrimary(weapon.GetTransform.gameObject);


        Rigidbody rigidbody = primary.GetComponent<Rigidbody>();
         Destroy (rigidbody);

         weapon.GetTransform.rotation =  transform.rotation;

         if (weapon.Name == pickUpModel.TranquilizerGun)
         {
             GunController gunController = weapon.GetTransform.GetComponent<GunController>();
             gunController.enabled = true;
         }
         else if (weapon.Name == pickUpModel.Flamethrower)
         {
             weapon.GetTransform.RotateAround (weapon.GetTransform.position, transform.up, 180f);
             FlameThrowerController flameThrowerController = weapon.GetTransform.GetComponent<FlameThrowerController>();
             flameThrowerController.enabled = true;
         }
         
    }


    public void DropWeapon(string weaponName)
    {
        //TODO drop weapon from player's inventory.
    }


    private void SetAsPrimary(GameObject weapon)
    {
        primary = weapon;
        primary.transform.parent = hTransform;
        primary.transform.position = hTransform.position;
        primary.SetActive(true);
    }


    private void SetAsSecondary(GameObject weapon)
    {
        secondary = weapon;
        secondary.SetActive(false);
    }


    public void SwitchWeapon()
    {
        if (secondary == null && primary == null)
            return;
        if (secondary == null)
            return;
        GameObject tempHolder;
        tempHolder = this.primary;
        SetAsPrimary(secondary);
        SetAsSecondary(tempHolder);
    }
}
