using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using uWindowCapture;

public class SYA_SympoUI : MonoBehaviour
{
    public GameObject window;
    public GameObject windowList;
    //move버튼 On / Off
    public Text moveOnOffstr;
    public GameObject moveOnOff;

    public GameObject spaceButton;
    public GameObject userData;


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

    public void OnUserList()
    {
        userData.SetActive(!userData.activeSelf);
    }

    public void OnSpaceChange()
    {
        spaceButton.SetActive(!spaceButton.activeSelf);
    }

    public void SolarSystemChange()
    {
        SceneManager.LoadScene("KJH_RevolutionScene");
        print("솔라 시스템으로 이동");
    }

    public void ConstellationChange()
    {
        //SceneManager.LoadScene("");
        print("별자리로 이동");
    }

}
