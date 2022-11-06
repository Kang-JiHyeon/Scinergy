using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialSpere : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SphereCollider>().radius = GameManager.instance.celestialSphereRadius;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(target.transform.rotation.eulerAngles.x, target.transform.rotation.eulerAngles.y, target.transform.rotation.eulerAngles.z);
    }
}
