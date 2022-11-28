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
        //���Ͱ����� ��ġ ����
        Vector3 rot = GetComponent<RectTransform>().eulerAngles;
        //�����̼� Z�� SIN
        rot.z += -Time.deltaTime * length;
        //�ٽ� �����̼����� ��ȯ
        GetComponent<RectTransform>().eulerAngles = rot;
    }
}
