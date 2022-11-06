using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// inputField�� ���� �Է¹޾� �¾�� ������ ������ �����ϰ� �ʹ�.
// - ����� ������, 
// play ��ư�� ������ ����� �������� �����δ�.
// - ����, ���� ����
// reset ��ư�� ������ �ʱ� ��ġ�� �̵��Ѵ�.
// - �ʱ� ��ġ��, 

public class KJH_SetMass : MonoBehaviour
{
    public Transform celestials;

    public Transform tr_sun;
    public Transform tr_earth;

    Rigidbody rb_sun;
    Rigidbody rb_earth;

    public InputField input_sun;
    public InputField input_earth;

    float sunMassValue = 0f;
    float earthMassValue = 0f;

    Vector3 originSunPos;
    Vector3 originEarthPos;

    public KJH_Orbit orbit;
    bool isOrbitMove = false;

    List<TrailRenderer> trails = new List<TrailRenderer>();

    public Text text_distance;


    // Start is called before the first frame update
    void Start()
    {
        //// Rigidbody
        //rb_sun = tr_sun.GetComponent<Rigidbody>();
        //rb_earth = tr_earth.GetComponent<Rigidbody>();


        // õü���� �ʱ� ��ġ
        originSunPos = tr_sun.position;
        originEarthPos = tr_earth.position;

        // �Է� ����Ǹ� ȣ��Ǵ� �Լ�
        // ���� ����
        input_sun.onEndEdit.AddListener(ChangeSunMass);
        input_earth.onEndEdit.AddListener(ChangeEarthMass);

        for (int i = 0; i < celestials.childCount; i++)
        {
            TrailRenderer trail = celestials.GetChild(i).GetComponent<TrailRenderer>();

            if (trail != null)
            {
                trails.Add(trail);
                trail.time = 0f;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        text_distance.text = Vector3.Distance(tr_sun.position, tr_earth.position).ToString();

        if (isOrbitMove)
        {
            orbit.Gravity();
        }
    }

    void ChangeSunMass(string text)
    {
        sunMassValue = int.Parse(text);
    }

    void ChangeEarthMass(string text)
    {
        earthMassValue = int.Parse(text);
    }

    public void OnClickPlay()
    {
        isOrbitMove = true;

        // rigidbody �߰�
        rb_sun = tr_sun.gameObject.AddComponent<Rigidbody>();
        rb_earth = tr_earth.gameObject.AddComponent<Rigidbody>();

        rb_sun.useGravity = false;
        rb_earth.useGravity = false;

        // ���� ����
        rb_sun.mass = sunMassValue;
        rb_earth.mass = earthMassValue;

        // �ʱ� ��ġ ����
        originSunPos = tr_sun.position;
        originEarthPos = tr_earth.position;

        // �˵� �׸��� Ȱ��ȭ
        ChangeTrailTime(100f);

        orbit.InitialVelocity();
    }

    public void OnClickStop()
    {
        isOrbitMove = false;
    }

    // �ʱ� ��ġ�� �̵�
    public void OnClickReset()
    {
        isOrbitMove = false;
        
        // rigidbody ������Ʈ ����
        Destroy(rb_sun);
        Destroy(rb_earth);

        // ��ġ �ʱ�ȭ
        tr_sun.position = originSunPos;
        tr_earth.position = originEarthPos;

        // �˵� �׸��� ��Ȱ��ȭ
        ChangeTrailTime(0f);

    }

    void ChangeTrailTime(float time)
    {
        for (int i = 0; i < trails.Count; i++)
        {
            trails[i].time = time;
        }
    }
}
