using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Compas : MonoBehaviour
{
    Quaternion northDirectonRotation;
    // Start is called before the first frame update
    private void Awake()
    {
        gameObject.name = "Compas" + PhotonNetwork.NickName;
        
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 northDirection = new Vector3(GameManager.instance.CelestialSphere.transform.up.x, 0, GameManager.instance.CelestialSphere.transform.up.z);
        if (northDirection != Vector3.zero)
        {
            northDirectonRotation = Quaternion.LookRotation(northDirection);
        }
        else
        {
            northDirectonRotation = Quaternion.identity;
        }
        transform.rotation = northDirectonRotation * Quaternion.Euler(90,0,0);
    }
}
