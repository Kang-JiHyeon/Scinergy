using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KJH_SolarSystemName : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 이름이 카메라 바라보도록
        transform.forward = Camera.main.transform.forward;
    }
}
