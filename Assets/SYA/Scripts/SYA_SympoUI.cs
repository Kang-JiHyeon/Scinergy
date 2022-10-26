using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_SympoUI : MonoBehaviour
{
    public GameObject window;
    public GameObject windowList;
    //move¹öÆ° On / Off
    public Text moveOnOffstr;
    public GameObject moveOnOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UwcOnOff()
    {
        window.SetActive(!window.activeSelf);
        windowList.SetActive(!windowList.activeSelf);
        moveOnOff.SetActive(!moveOnOff.activeSelf);
    }

    public void UwcMoveOnOff()
    {
        window.GetComponent<SYA_SympoWindowsMoving>().enabled = !window.GetComponent<SYA_SympoWindowsMoving>().enabled;
        moveOnOffstr.text = $"MOVE : {window.GetComponent<SYA_SympoWindowsMoving>().enabled}";
        Camera.main.transform.parent.GetComponentInParent<SYA_PlayerRot>().enabled = true;
    }
}
