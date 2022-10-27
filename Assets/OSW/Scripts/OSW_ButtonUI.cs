using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSW_ButtonUI : MonoBehaviour
{
    // 2022.10.26 승원 DrawOnOff 버튼/ LineDrawer 오브젝트 추가
    public GameObject drawOnOff;
    public GameObject lineDrawer;
    
    void Start()
    {
        lineDrawer.SetActive(false);
    }

    public void DrawOnOff()
    {
        lineDrawer.SetActive(!lineDrawer.activeSelf);
    }
}
