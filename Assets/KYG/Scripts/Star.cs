using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    //���̸�
    public string starName;
    //����
    public float ra;
    //����
    public float dec;
    //�� ����
    public GameObject starType;
    //�� ���
    public GameObject brightness;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    internal void InfoSet(string starNameInfo, float raInfo, float decInfo, GameObject starTypeInfo, GameObject brightnessInfo)
    {
        name = starName;
        starName = starNameInfo;
        ra = raInfo;
        dec = decInfo;
        starType = starTypeInfo;
        brightness = brightnessInfo;
        TransformSet();
    }
    internal void TransformSet()
    {
        dec = dec * (Mathf.PI / 180);
        dec = (Mathf.PI / 2) - dec;
        ra = ra * -15f * Mathf.PI / 180;
        var rr = StarGenerator.instance.celestialSphereRadius * Mathf.Sin(dec);
        float x = rr * Mathf.Sin(ra);
        float y = StarGenerator.instance.celestialSphereRadius * Mathf.Cos(dec);
        float z = rr * Mathf.Cos(ra);
        transform.localPosition = new Vector3(x, y, z);
    }
}
