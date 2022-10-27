using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSW_ButtonUI : MonoBehaviour
{
    // 2022.10.26 �¿� DrawOnOff ��ư/ LineDrawer ������Ʈ �߰�
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
