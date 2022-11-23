using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class SYA_SympoSit : MonoBehaviourPun
{

    public Transform TV;

    private void Start()
    {
        transform.LookAt(TV);
    }


}
