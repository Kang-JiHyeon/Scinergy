using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;


// (v) ���� �ð��� Text�� ��Ÿ���� �ʹ�. 
// - ��, ��, ��, �ð�, ��, ����/����


// (v) ���� ��ư
// ��ũ�ѹ��� ���� 0.5�� �����ϰ� �ʹ�.

// (v) ��� ��ư
// - ���� ������ ���� �ð����� �����ϰ� �ʹ�.

// ���� �ð� ��ư (����)
// - (v) ���� �ð��� ���� �ð����� �����ϰ� �ʹ�.
// - �༺���� ��ġ�� ���� �ð� �������� �ǵ����� �ʹ�.
// -- ���� ��ġ�� ��� ������ �� List�� �ʿ��ϴ�.
// ���� ��ġ�� �����صδ� ���� �ð�-> ��..����..?


public class KJH_UIManager : MonoBehaviour
{

    // ���� �ð� : ��� Update
    DateTime curDate;
    // ���� �ð� : ��ũ�ѹ� ���� ���� �����
    DateTime obsDate;

    public Text yearText;
    public Text dateText;
    public Text timeText;
    public Text obsDateText;

    public GameObject go_ControlTimeUI;
    public GameObject go_DefalutUI;

    float curTime = 0f;
    float updateTime = 1f;
    float originScrollValue = 0.75f;

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
        if (curTime > updateTime)
        {
            curTime = 0f;

            if (KJH_SolarSystem.instance.unitTimeNum != 0)
            {
                SetObsDate();
                SetObsDateText();
            }

        }
    }

    // ���� �ð� ���� �Լ�
    void SetObsDate()
    {
        switch (KJH_SolarSystem.instance.unitTime)
        {
            case KJH_SolarSystem.UnitTime.year:
                obsDate = obsDate.AddYears((int)KJH_SolarSystem.instance.unitTimeNum);
                break;
            case KJH_SolarSystem.UnitTime.month:
                obsDate = obsDate.AddMonths((int)KJH_SolarSystem.instance.unitTimeNum);
                break;
            case KJH_SolarSystem.UnitTime.day:
                obsDate = obsDate.AddDays(KJH_SolarSystem.instance.unitTimeNum);
                break;
            case KJH_SolarSystem.UnitTime.hour:
                obsDate = obsDate.AddHours(KJH_SolarSystem.instance.unitTimeNum);
                break;
        }
    }

    void SetObsDateText()
    {
        // 2022
        yearText.text = obsDate.ToString("yyyy");
        // 10�� 23��
        dateText.text = obsDate.ToString("MM") + "�� " + obsDate.ToString("dd") + "��";
        // 01�� 15�� ����
        timeText.text = obsDate.ToString("HH") + " : " + obsDate.ToString("mm") + " " + obsDate.ToString("tt");

        // 2022�� 10�� 23�� 01�� 15�� ����
        obsDateText.text = yearText.text + "�� " + dateText.text + " " + timeText.text;
    }

    public void StopObservation()
    {
        if (KJH_SolarSystem.instance.unitTimeScrolbar.value != 0.5f)
        {
            originScrollValue = KJH_SolarSystem.instance.unitTimeScrolbar.value;
            KJH_SolarSystem.instance.unitTimeScrolbar.value = 0.5f;
        }
    }

    public void PlayObservation()
    {
        if (KJH_SolarSystem.instance.unitTimeScrolbar.value == 0.5f)
        {
            KJH_SolarSystem.instance.unitTimeScrolbar.value = originScrollValue;
        }
    }
    // ��ġ �̵� ���Ѿ� ��
    public void ToCurTime()
    {
        obsDate = DateTime.Now;
        SetObsDateText();
    }

    public void GoBack()
    {
        go_ControlTimeUI.SetActive(false);
        go_DefalutUI.SetActive(true);
    }

    public void ChangeToScrollUI()
    {
        go_ControlTimeUI.SetActive(true);
        go_DefalutUI.SetActive(false);
    }

    

}
