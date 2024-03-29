using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugTMP;
    [SerializeField] private TextMeshProUGUI hintTMP;
    private bool showDebug = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showDebug)
        {
            showDebug = false;
            Action _clear = ()=>{
                clearDebug();
                showDebug = true;
            };
            StartCoroutine(Wait(3f, _clear));
        }
    }


    public void SetHint(string hint)
    {
        hintTMP.text = hint;
    }


    public void clearHint()
    {
        hintTMP.text = "";
    }


    public void writeHUD()
    {
        //TODO write HUD
    }


    public void writeDebug(string msg)
    {
        string debug_log = "";
        if (debugTMP.text == "")
        {
            debug_log = String.Concat("DEBUG: \n", debugTMP.text, "\n", msg, "\n");
        }
        else
        {
            debug_log = String.Concat(debugTMP.text, "\n", msg, "\n");
        }
        debugTMP.text = debug_log;
    }


    public void clearDebug()
    {
        debugTMP.text = "";
    }


    private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}