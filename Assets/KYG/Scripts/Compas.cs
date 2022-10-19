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

        //transform.rotation = Quaternion.Euler(90, 0, -StarGenerator.instance.CelestialSpehere.transform.rotation.eulerAngles.z);
        Vector3 northDirection = new Vector3(StarGenerator.instance.CelestialSpehere.transform.up.x, 0, StarGenerator.instance.CelestialSpehere.transform.up.z);
        //float northAngle = Vector3.Angle(transform.up, northDirection - transform.position);
        float northAngle = Mathf.Acos(Vector3.Dot(transform.up, northDirection - transform.up) / Vector3.Magnitude(transform.up) / Vector3.Magnitude(northDirection - transform.position)) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(90, 0, northAngle);
    }
}
