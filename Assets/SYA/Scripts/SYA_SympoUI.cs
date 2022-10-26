using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uWindowCapture;

public class SYA_SympoUI : MonoBehaviour
{
    public GameObject window;
    public GameObject windowList;
    //move��ư On / Off
    public Text moveOnOffstr;
    public GameObject moveOnOff;

    public int WindowSize=3;

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

    //��ư�� ������, �ڽ��� â ũ�⸦ �����Ѵ�
}
