using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugTMP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetHint(string hint)
    {
        //TODO write hint
    }


    public void clearHint()
    {
        //TODO clear hint
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
}