using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class SYA_UwcNicname : MonoBehaviourPun
{
    public Text userName;

    // Start is called before the first frame update
    void Start()
    {
        userName.text = "¼Ò¿µ¾Æ";//photonView.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
