using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axisRotate : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(target.transform.rotation.eulerAngles.x, target.transform.rotation.eulerAngles.y, target.transform.rotation.eulerAngles.z);
        //transform.rotation = Quaternion.Euler(target.transform.rotation.x, target.transform.rotation.y, target.transform.rotation.z);
    }
}
