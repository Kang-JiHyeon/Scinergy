using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;



// (V) 관측 시간을 Text로 나타내고 싶다. 
// - 년, 월, 일, 시간, 분, 오전/오후


// 정지 버튼
// 스크롤바의 값을 0.5로 지정하고 싶다.

// 재생 버튼
// - 정지 직전의 단위 시간으로 지정하고 싶다.


// 현재 시각 버튼
// - 관측 시각을 현재 시각으로 지정하고 싶다.
// - 행성들의 위치도 현재 시각 기준으로 되돌리고 싶다.


public class KJH_ObservationTime : MonoBehaviour
{
    public KJH_SolarSystem solarSystem;

    // 현재 시간 : 계속 Update
    DateTime curDate;
    // 관측 시간 : 스크롤바 값에 따라 변경됨
    DateTime obsDate; 

    public Text yearText;
    public Text DateText;
    public Text TimeText;

    float curTime = 0f;
    float updateTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        curDate = DateTime.Now;
        obsDate = curDate;
        SetObsDateText();
    }


    private void FixedUpdate()
    {
        // 현재 시각 Update
        curDate = DateTime.Now;

        // 1초마다 관측 시간 갱신
        curTime += Time.fixedDeltaTime;
        if(curTime > updateTime)
        {
            curTime = 0f;

            if (solarSystem.unitTimeNum != 0)
            {
                SetObsDate();
                SetObsDateText();
            }

        }
    }

    // 관측 시간 설정 함수
    void SetObsDate()
    {
        switch (solarSystem.unitTime)
        {
            case KJH_SolarSystem.UnitTime.year:
                obsDate = obsDate.AddYears((int)solarSystem.unitTimeNum);
                break;
            case KJH_SolarSystem.UnitTime.month:
                obsDate = obsDate.AddMonths((int)solarSystem.unitTimeNum);
                break;
            case KJH_SolarSystem.UnitTime.day:
                obsDate = obsDate.AddDays(solarSystem.unitTimeNum);
                break;
            case KJH_SolarSystem.UnitTime.hour:
                obsDate = obsDate.AddHours(solarSystem.unitTimeNum);
                break;
        }

    }

    void SetObsDateText()
    {
        // 2022
        yearText.text = obsDate.ToString("yyyy");
        // 10월 23일
        DateText.text = obsDate.ToString("MM") + "월 " + obsDate.ToString("dd") + "일";
        // 01시 15분 오전
        TimeText.text = obsDate.ToString("HH") + " : " + obsDate.ToString("mm") + " " + obsDate.ToString("tt");
    }
}
