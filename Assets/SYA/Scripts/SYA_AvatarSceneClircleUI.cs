using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_AvatarSceneClircleUI : MonoBehaviour
{
    public GameObject[] clircle01 = new GameObject[2];
    public GameObject[] clircle02 = new GameObject[2];
    public GameObject[] clircle03 = new GameObject[2];
    public GameObject[] clircle04 = new GameObject[2];
    public GameObject[] clircle05 = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        SYA_AvartaSceneManager.Instance.WhiteClircle += WhiteClircleChange;
    }

    public void WhiteClircleChange(int num)
    {
        if (num == 0)
        {
            ClicleW(clircle01, false);
            ClicleW(clircle02, true);
            ClicleW(clircle03, true);
            ClicleW(clircle04, true);
            ClicleW(clircle05, true);
        }
        else if (num == 1)
        {
            ClicleW(clircle01, true);
            ClicleW(clircle02, false);
            ClicleW(clircle03, true);
            ClicleW(clircle04, true);
            ClicleW(clircle05, true);
        }
        else if (num == 2)
        {
            ClicleW(clircle01, true);
            ClicleW(clircle02, true);
            ClicleW(clircle03, false);
            ClicleW(clircle04, true);
            ClicleW(clircle05, true);
        }
        else if (num == 3)
        {
            ClicleW(clircle01, true);
            ClicleW(clircle02, true);
            ClicleW(clircle03, true);
            ClicleW(clircle04, false);
            ClicleW(clircle05, true);
        }
        else if (num == 4)
        {
            ClicleW(clircle01, true);
            ClicleW(clircle02, true);
            ClicleW(clircle03, true);
            ClicleW(clircle04, true);
            ClicleW(clircle05, false);
        }
    }

    void ClicleW(GameObject[] clircle, bool white)
    {
            clircle[0].SetActive(white);
            clircle[1].SetActive(!white);
    }
}
