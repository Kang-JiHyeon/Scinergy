using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class SYA_UwcNicname : MonoBehaviourPun
{
    public Text userName;
    float zP;
    // Start is called before the first frame update
    void Start()
    {
        userName.text = photonView.name;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }
}
