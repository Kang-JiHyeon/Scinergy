using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_Loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float speed = 5;
    float length = 200;
    float runningTime;
    // Update is called once per frame
    void Update()
    {
        runningTime += Time.deltaTime;
        //벡터값으로 위치 저장
        Vector3 rot = GetComponent<RectTransform>().eulerAngles;
        //로테이션 Z값 SIN
        rot.z += -Time.deltaTime * length;
        //다시 로테이션으로 전환
        GetComponent<RectTransform>().eulerAngles = rot;
    }
}
