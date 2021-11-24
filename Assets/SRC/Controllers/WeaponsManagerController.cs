using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManagerController : MonoBehaviour
{
    private GameObject primary;
    private GameObject secondary;



    // Start is called before the first frame update
    void Start()
    {
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
    }


    public void DropWeapon(string weaponName)
    {
        //TODO drop weapon from player's inventory.
    }


    private void SetAsPrimary(GameObject weapon)
    {
        primary = weapon;
        primary.transform.parent = transform;
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
