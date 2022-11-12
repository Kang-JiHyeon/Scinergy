using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 자전하고 싶다.
public class KJH_Rotate : MonoBehaviour
{
    public KJH_CustomOption custom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (custom.isOrbitMove)
        {
            transform.Rotate(-transform.up, 0.5f);
        }
    }
}
