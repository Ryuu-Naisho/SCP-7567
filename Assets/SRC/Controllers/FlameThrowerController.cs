using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerController : MonoBehaviour
{
    [SerializeField] private float fuelLevel;
    [SerializeField] private float fuelBurnRate;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }


        Debug.Log(fuelLevel);
    }



    private void Fire()
    {
        if (fuelLevel <= 0)
            return;
        StartCoroutine(BurnFuel(fuelBurnRate));
    }


    private IEnumerator BurnFuel(float time)
    {
        yield return new WaitForSeconds(time);
        fuelLevel --;
    }
}
