using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KJH_SolarSystemName : MonoBehaviour
{
    float originY = 0f;
    // Start is called before the first frame update

    void Start()
    {
        originY = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        // 이름이 카메라 바라보도록
        transform.parent.forward = Camera.main.transform.forward;

        float distance = Vector3.Distance(transform.parent.position, Camera.main.transform.position) / 10f;

        if(distance > 2f)
        {
            transform.localScale = Vector3.one * distance;
            transform.localPosition = new Vector3(0f, originY + distance / 4f, 0f);
        }
    }
}
