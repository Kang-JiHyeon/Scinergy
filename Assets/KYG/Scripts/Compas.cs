using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion northDirecton = Quaternion.LookRotation(new Vector3(GameManager.instance.CelestialSpehere.transform.up.x, 0, GameManager.instance.CelestialSpehere.transform.up.z));
        transform.rotation = northDirecton * Quaternion.Euler(90,0,0);
    }
}
