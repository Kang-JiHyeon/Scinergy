using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// inputField로 값을 입력받아 태양과 지구의 질량을 변경하고 싶다.
// - 변경된 질량값, 
// play 버튼을 누르면 변경된 질량으로 움직인다.
// - 질량, 실행 제어
// reset 버튼을 누르면 초기 위치로 이동한다.
// - 초기 위치값, 

public class KJH_CustomOption : MonoBehaviour
{
    public GameObject customUI;
    public GameObject sunLight;

    public Transform celestials;
    public Transform tr_sun;
    public Transform tr_earth;
    public Transform camPivot;
    Transform target;

    Rigidbody rb_sun;
    Rigidbody rb_earth;

    public InputField input_sun;
    public InputField input_earth;

    float sunMassValue = 0f;
    float earthMassValue = 0f;

    Vector3 pivotPos;
    Vector3 originSunPos;
    Vector3 originEarthPos;

    public KJH_Orbit orbit;
    public bool isOrbitMove = false;

    List<TrailRenderer> trails = new List<TrailRenderer>();
    public float trailTime = 100f;
    //public Scrollbar scroll;
    //public Text text_distance;

    public KJH_OrbitCamera camara;


    // Start is called before the first frame update
    void Start()
    {
        target = camPivot;

        btn_custom.image.sprite = btn_sprites[1];

        // 천체들의 초기 위치
        originSunPos = tr_sun.position;
        originEarthPos = tr_earth.position;

        // 입력 종료되면 호출되는 함수
        // 질량 설정
        input_sun.onEndEdit.AddListener(ChangeSunMass);
        input_earth.onEndEdit.AddListener(ChangeEarthMass);

        for (int i = 0; i < celestials.childCount; i++)
        {
            TrailRenderer trail = celestials.GetChild(i).GetComponent<TrailRenderer>();

            if (trail != null)
            {
                trails.Add(trail);
                trail.time = trailTime;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isOrbitMove)
        {
            orbit.Gravity();
            camPivot.position = target.position;
        }

        sunLight.transform.LookAt(tr_earth.position);
        input_sun.enabled = !isOrbitMove;
        input_earth.enabled = !isOrbitMove;
    }

    void ChangeSunMass(string text)
    {
        if (text == "") return;
        sunMassValue = float.Parse(text);
    }

    void ChangeEarthMass(string text)
    {
        if (text == "") return;
        earthMassValue = float.Parse(text);
    }

    public void OnClickPlay()
    {
        // 두 천체의 질량이 모두 입력됬을 때 실행
        if (input_sun.text.Length <= 0 || input_earth.text.Length <= 0) return;

        // 질량 큰 행성 기준으로 카메라 회전
        target = sunMassValue > earthMassValue ? tr_sun : tr_earth;

        // 초기 위치 저장
        originSunPos = tr_sun.position;
        originEarthPos = tr_earth.position;

        // rigidbody 추가
        rb_sun = tr_sun.gameObject.AddComponent<Rigidbody>();
        rb_earth = tr_earth.gameObject.AddComponent<Rigidbody>();

        // 중력 영향 해제
        rb_sun.useGravity = false;
        rb_earth.useGravity = false;

        // 질량 변경
        rb_sun.mass = sunMassValue;
        rb_earth.mass = earthMassValue;

        // 궤도 그리기 활성화
        ChangeTrailTime(trailTime);

        // 초기 속도
        orbit.InitialVelocity();

        isOrbitMove = true;
    }

    public void OnClickStop()
    {
        isOrbitMove = false;
    }

    // 초기 위치로 이동
    public void OnClickReset()
    {
        isOrbitMove = false;
        
        // rigidbody 컴포넌트 제거
        Destroy(rb_sun);
        Destroy(rb_earth);

        // 위치 초기화
        tr_sun.position = originSunPos;
        tr_earth.position = originEarthPos;
        camPivot.position = tr_sun.position;

        // inputfield 값 초기화
        input_sun.text = "";
        input_earth.text = "";

        // 궤도 그리기 비활성화
        ChangeTrailTime(0f);
    }

    void ChangeTrailTime(float time)
    {
        for (int i = 0; i < trails.Count; i++)
        {
            trails[i].time = time;
        }
    }

    public Button btn_custom;
    public List<Sprite> btn_sprites;

    public void OnClick_Custom()
    {
        customUI.SetActive(!customUI.activeSelf);

        if (customUI.activeSelf)
        {
            btn_custom.image.sprite = btn_sprites[1];
        }
        else
        {
            btn_custom.image.sprite = btn_sprites[0];
        }

    }

    public void OnClickClose()
    {
        customUI.SetActive(false);
        btn_custom.image.sprite = btn_sprites[0];
    }

}
