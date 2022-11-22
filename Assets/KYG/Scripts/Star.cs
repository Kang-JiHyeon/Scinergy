
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Star : MonoBehaviourPun
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

    public GameObject starInfo;

    public int generatedType;

    #region shootingStar
    public float fallSpeed = 50f;
    public float removeTime = 3f;
    float currentTime = 0f;
    Vector3 fallDir;
    public float randX, randY;
    #endregion
    public enum State
    {
        normalStar,
        shootingStar,
    }

    public State StarState = State.normalStar;
    // Start is called before the first frame update
    void Start()
    {
        if (generatedType == 0) 
        { 
            InfoSet(starName, ra, dec, starType, brightness, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
    }

    public void StateMachine()
    {
        switch (StarState)
        {
            case State.normalStar:
                return;
            case State.shootingStar:
                ShootingStar();
                return;
        }

    }

    private void ShootingStar()
    {
        GetComponent<TrailRenderer>().enabled = true;
        currentTime += Time.deltaTime;
        if (currentTime > removeTime)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime);
            if ((transform.localScale - Vector3.one).magnitude < 0.1f)
            {
                Destroy(gameObject);
                currentTime = 0;
            }
        }
            fallDir.x = randX;
            fallDir.y = randY;
            transform.position += fallDir.normalized * fallSpeed * Time.deltaTime;
    }
    //int starNumber = 1;
    internal void InfoSet(string starNameInfo, float raInfo, float decInfo, GameObject starTypeInfo, GameObject brightnessInfo, int generateTypeNumber)
    {
        name = starNameInfo;
        starName = starNameInfo;
        ra = raInfo % 24;
        if (ra < 0) ra += 24;
        if (dec > 90)
        {
            dec = decInfo % 90;
        }
        else
        {
            dec = decInfo;
        }
        starType = starTypeInfo;
        brightness = brightnessInfo;
        generatedType = generateTypeNumber;
        TransformSet();
        BrightnessSet();
    }


    public virtual void TransformSet()
    {
        transform.parent = GameManager.instance.CelestialSphere.transform;
        float RadDec;
        float RadRa;
        RadDec = dec * (Mathf.PI / 180);
        RadDec = (Mathf.PI / 2) - RadDec;
        RadRa = ra * -15f * Mathf.PI / 180;
        var rr = GameManager.instance.celestialSphereRadius * Mathf.Sin(RadDec);
        float x = rr * Mathf.Sin(RadRa);
        float y = GameManager.instance.celestialSphereRadius * Mathf.Cos(RadDec);
        float z = rr * Mathf.Cos(RadRa);
        if(generatedType == 1 || generatedType ==3)
        {
            transform.localPosition = new Vector3(x, y, z);
        }else if(generatedType == 2)
        {
            transform.position = new Vector3(x, y, z);
        }
    }
    public virtual void BrightnessSet()
    {
        //GameObject starBrightness = Instantiate(brightness);
        GameObject starBrightness = Instantiate(brightness);
        starBrightness.transform.parent = gameObject.transform;
        starBrightness.transform.position = transform.position;
    }
}
