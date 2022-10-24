using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;



// (V) ���� �ð��� Text�� ��Ÿ���� �ʹ�.
// - ��, ��, ��, �ð�, ��, ����/����


// ���� ��ư
// ��ũ�ѹ��� ���� 0.5�� �����ϰ� �ʹ�.

// ���� ��ư
// - ���� ������ ���� �ð����� �����ϰ� �ʹ�.


// ���� �ð� ��ư
// - ���� �ð��� ���� �ð����� �����ϰ� �ʹ�.
// - �༺���� ��ġ�� ���� �ð� �������� �ǵ����� �ʹ�.


public class KJH_ObservationTime : MonoBehaviour
{
    public KJH_SolarSystem solarSystem;

    // ���� �ð� : ���� Update
    DateTime curDate;
    // ���� �ð� : ��ũ�ѹ� ���� ���� ������
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
        // ���� �ð� Update
        curDate = DateTime.Now;

        // 1�ʸ��� ���� �ð� ����
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

    // ���� �ð� ���� �Լ�
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
        // 10�� 23��
        DateText.text = obsDate.ToString("MM") + "�� " + obsDate.ToString("dd") + "��";
        // 01�� 15�� ����
        TimeText.text = obsDate.ToString("HH") + " : " + obsDate.ToString("mm") + " " + obsDate.ToString("tt");
    }
}
