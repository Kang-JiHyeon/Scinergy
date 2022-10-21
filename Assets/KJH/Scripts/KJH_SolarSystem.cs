using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 행성 공전 시킴

public class KJH_SolarSystem : MonoBehaviour
{
    List<Transform> planets = new List<Transform>();
    public Transform go_planets;
    public List<float> yearPeriods;
    public List<float> AUs;
    public List<float> scales;
    public List<float> axisTilts;

    public enum UnitTime
    {
        year = 1,
        month = year * 12,
        day = month * 30,
        hour = day * 24,
        min = hour * 60
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
            planets[i].localScale = Vector3.one * scales[i];
        }
    }

    float rotAngle = 0f;
    public float time = 1f;
    // 0.02초 마다 update => 1초에 50번 호출
    private void FixedUpdate()
    {
        for(int i = 0; i<planets.Count; i++)
        {
            // 단위 시간 당 공전 주기에 따른 rot angle 설정
            rotAngle = 360f / yearPeriods[i] / (float)unitTime;

            // 공전 각도만큼 회전
            planets[i].RotateAround(transform.position, -transform.up, rotAngle * Time.fixedDeltaTime);
        }

    }
}
