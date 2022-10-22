using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    //별이름
    public string starName;
    //적경
    public float ra;
    //적위
    public float dec;
    //별 종류
    public GameObject starType;
    //별 밝기
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
        name = starNameInfo;
        starName = starNameInfo;
        ra = raInfo;
        dec = decInfo;
        starType = starTypeInfo;
        brightness = brightnessInfo;
        TransformSet();
        brightnessSet();
    }


    public virtual void TransformSet()
    {
        transform.parent =StarGenerator.instance.CelestialSpehere.transform;
        float RadDec;
        float RadRa;
        RadDec = dec * (Mathf.PI / 180);
        RadDec = (Mathf.PI / 2) - RadDec;
        RadRa = ra * -15f * Mathf.PI / 180;
        var rr = StarGenerator.instance.celestialSphereRadius * Mathf.Sin(RadDec);
        float x = rr * Mathf.Sin(RadRa);
        float y = StarGenerator.instance.celestialSphereRadius * Mathf.Cos(RadDec);
        float z = rr * Mathf.Cos(RadRa);
        transform.localPosition = new Vector3(x, y, z);
    }
    public virtual void brightnessSet()
    {
        GameObject starBrightness = Instantiate(brightness);
        starBrightness.transform.parent = gameObject.transform;
        starBrightness.transform.position = transform.position;
    }
}
