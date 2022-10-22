using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 행성 공전


public class KJH_SolarSystem : MonoBehaviour
{
    List<Transform> planets = new List<Transform>();
    public Transform go_planets;
    public List<float> yearPeriods;
    public List<float> AUs;
    public List<float> scales;
    public List<float> axisTilts;
    float rotAngle = 0f;

    // 행성 궤도 그리기 위한 변수
    LineRenderer lr;
    float radius;        // 기준점과의 거리(반지름)
    public int segment;         // 라인 꼭짓점 개수

    // 슬라이더 바에 따른 초당 단위시간 변경
    public Scrollbar unitTimeScrolbar;

    public enum UnitTime
    {
        year = 1,
        month = year * 12,
        day = month * 30,
        hour = day * 24,
        //min = hour * 60
    }

    public UnitTime unitTime = UnitTime.year;

    // Start is called before the first frame update
    void Start()
    {
        // 행성 리스트에 추가
        for (int i=0; i < go_planets.childCount; i++)
        {
            planets.Add(go_planets.GetChild(i).transform);
            // 행성 자전축 기울기 초기화
            planets[i].GetChild(1).localRotation = Quaternion.Euler(axisTilts[i], 0, 0);

            // 행성 크기 초기화
            planets[i].localScale = Vector3.one * scales[i] * 0.1f;

            // 태양-행성 간의 거리 초기화
            planets[i].localPosition = new Vector3(1, 0, 1) * AUs[i] * 10;

            

            // 행성 궤도 그리기
            lr = planets[i].GetChild(0).GetComponent<LineRenderer>();
            radius = Vector3.Distance(transform.position, planets[i].position);

            DrawOrbit();
        }

    }

    
    // 0.02초 마다 update => 1초에 50번 호출
    private void FixedUpdate()
    {
        for(int i = 0; i<planets.Count; i++)
        {
            // 단위 시간 당 공전 주기에 따른 rot angle 설정
            rotAngle = 360f / yearPeriods[i] / (float)unitTime;

            // 각도만큼 회전
            planets[i].RotateAround(transform.position, -transform.up, rotAngle * Time.fixedDeltaTime);

            // 라이트 방향 설정
            planets[i].Find("OrbitAxis").GetChild(0).forward = planets[i].position - transform.position;
        }

    }


    // 행성 공전 궤도 그리는 함수
    void DrawOrbit()
    {
        Vector3[] points = new Vector3[segment + 1];
        //float angle = 360f / segment;

        for (int i = 0; i < points.Length; i++)
        {
            //points[i] = orbitAxis.position + CalculatePosition((float)i / (float)segment);
            points[i] = transform.position + CalculatePosition((float)i / (float)segment);
        }

        points[segment] = points[0];
        lr.positionCount = segment + 1;
        lr.SetPositions(points);
    }
    
    Vector3 CalculatePosition(float t)
    {
        float angle = Mathf.Deg2Rad * 360f * t;
        float x = Mathf.Sin(angle) * radius;
        float z = Mathf.Cos(angle) * radius;
        return new Vector3(x, 0, z);
    }

    public void SetUnitTime()
    {

    }

}
