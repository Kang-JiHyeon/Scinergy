using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 행성 공전

public class KJH_SolarSystem : MonoBehaviour
{
    public static KJH_SolarSystem instance;

    // << 행성 >>
    // 행성 부모
    public Transform go_planets;
    // 행성 
    public List<Transform> planets = new List<Transform>();
    // 태양과의 거리
    public List<float> AUs;
    // 행성 크기
    public List<float> scales;
    // 자전축
    public List<float> axisTilts;

    // << 공전 >>
    // 공전 연주기
    public List<float> yearPeriods;

    // << 자전 >>
    // 자전 일주기
    public List<float> dayRotPeriods;



    // 행성 공전 궤도 그리기 위한 변수
    LineRenderer lr;
    float radius;        // 기준점과의 거리(반지름)
    public int segment;         // 라인 꼭짓점 개수

    // 슬라이더 바에 따른 초당 단위시간 변경
    public Scrollbar unitTimeScrolbar;
    public Text unitTimeText;
    public Text pastFutureText;

    public enum UnitTime
    {
        year = 1,
        month = year * 12,
        day = month * 30,
        hour = day * 24,
        //min = hour * 60
    }

    public UnitTime unitTime = UnitTime.year;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 행성 리스트에 추가
        for (int i = 0; i < planets.Count; i++)
        {
            //planets.Add(go_planets.GetChild(i).transform);
            // 행성 자전축 기울기 초기화
            planets[i].GetChild(1).localRotation = Quaternion.Euler(axisTilts[i], 0, 0);

            // 행성 크기 초기화
            //planets[i].localScale = Vector3.one * scales[i];

            // 태양-행성 간의 거리 초기화
            //planets[i].localPosition = new Vector3(1, 0, 1) * AUs[i] * 15;

            // 라이트 각도 행성을 비추도록 초기화
            planets[i].Find("OrbitAxis").GetChild(0).forward = planets[i].position - transform.position;

            if (i > 7) break;
            // 행성 궤도 그리기
            lr = planets[i].GetChild(0).GetComponent<LineRenderer>();
            radius = Vector3.Distance(planets[i].GetChild(0).position, planets[i].position);

            DrawOrbit(planets[i].GetChild(0));
        }
    }

    // 0.02초 마다 update => 1초에 50번 호출
    private void FixedUpdate()
    {
        // 단위 시간에 따른 공전
        Revolution();

        // 단위 시간에 따른 자전
        PlanetRotation();

    }

    // 행성별 자전
    private void PlanetRotation()
    {
        for(int i=0; i< planets.Count; i++)
        {
            float rotAngle = (360 / dayRotPeriods[i] * 365 / (float)unitTime) * unitTimeNum;
            planets[i].GetChild(1).Rotate(0, -rotAngle * Time.fixedDeltaTime, 0);
        }
    }

    // 행성별 공전
    private void Revolution()
    {
        for (int i = 0; i < planets.Count; i++)
        {
            // 단위 시간 당 공전 주기에 따른 rot angle 설정
            //rotAngle = 360f / yearPeriods[i] / (float)unitTime;
            float rotAngle = (360f / yearPeriods[i] / (float)unitTime) * unitTimeNum;

            // 각도만큼 회전
            planets[i].RotateAround(planets[i].GetChild(0).position, -transform.up, rotAngle * Time.fixedDeltaTime);

            // 라이트 방향 설정
            planets[i].Find("OrbitAxis").GetChild(0).forward = planets[i].position - transform.position;
        }
    }


    // 행성 공전 궤도 그리는 함수
    private void DrawOrbit(Transform orbitAxis)
    {
        Vector3[] points = new Vector3[segment + 1];
        //float angle = 360f / segment;

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = orbitAxis.position + CalculatePosition((float)i / (float)segment);
            //points[i] = transform.position + CalculatePosition((float)i / (float)segment);
        }

        points[segment] = points[0];
        lr.positionCount = segment + 1;
        lr.SetPositions(points);
    }

    private Vector3 CalculatePosition(float t)
    {
        float angle = Mathf.Deg2Rad * 360f * t;
        float x = Mathf.Sin(angle) * radius;
        float z = Mathf.Cos(angle) * radius;
        return new Vector3(x, 0, z);
    }

    int month = 11;
    int day = 30;
    int hour = 23;
    float standard = 0.5f / 65f;
    public float unitTimeNum = 0f;
    //public float originScrollValue = 0.5f;
    float sign = 0;
    string unitTimeTxt = "";

    // 스크롤바의 값에 따라 단위시간을 결정하고 싶다.
    public void SetUnitTime()
    {
        if(unitTimeScrolbar.value > 0.5f)
        {
            sign = 1;
            pastFutureText.text = "미래로";
        }
        else
        {
            sign = -1;
            pastFutureText.text = "과거로";
        }

        //originScrollValue = unitTimeScrolbar.value;
        float value = Mathf.Abs(0.5f - unitTimeScrolbar.value) / standard;

        if (value < 1f)
        {
            unitTimeNum = 0;
            unitTimeText.text = "멈추기";
            pastFutureText.text = "멈추기";
        }
        else
        {
            if (value >= hour + day + month)
            {
                unitTimeNum = 1;
                unitTimeTxt = "년";

                unitTime = UnitTime.year;

            }
            else if (value > hour + day)
            {
                unitTimeNum = MathF.Truncate(value - (hour + day) * standard - (hour + day)) % (month + 1) + 1;
                unitTimeTxt = "월";

                unitTime = UnitTime.month;
            }
            else if (value > hour + 1)
            {
                unitTimeNum = MathF.Truncate(value - hour * standard - hour) % (day + 1) + 1;
                unitTimeTxt = "일";

                unitTime = UnitTime.day;
            }
            else
            {
                unitTimeNum = MathF.Truncate(value % (hour + 1));
                unitTimeTxt = "시간";

                unitTime = UnitTime.hour;

            }
            unitTimeNum *= sign;
            unitTimeText.text = unitTimeNum.ToString("+#;-#;0") + " " + unitTimeTxt + " / 초";
            
        }
    }
}
