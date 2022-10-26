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
    public static KJH_UIManager instance; 
    // ���� �ð� : ��� Update
    DateTime curDate;
    // ���� �ð� : ��ũ�ѹ� ���� ���� �����
    DateTime obsDate;

    public Text yearText;
    public Text dateText;
    public Text timeText;
    public Text obsDateText;

    float curTime = 0f;
    float updateTime = 1f;
    float originScrollValue = 0.75f;

    // UI
    public GameObject controlTimeUI;
    public GameObject defalutUI;

    Dictionary<string, GameObject> dict_UI = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        curDate = DateTime.Now;
        obsDate = curDate;
        SetObsDateText();

        for(int i=0; i<transform.childCount; i++)
        {
            // <UI_name, UI_gameObject>
            dict_UI.Add(transform.GetChild(i).name, transform.GetChild(i).gameObject);
            transform.GetChild(i).gameObject.SetActive(false);
        }

        // �⺻ UI ����
        dict_UI["UI_Defalut"].SetActive(true);

        MoveDefalutUI(120f, 120f);
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

        SetObsDateText();
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
        if (KJH_SolarSystem.instance.unitTimeScrolbar.value - 0.5f >= 0.01f)
        {
            originScrollValue = KJH_SolarSystem.instance.unitTimeScrolbar.value;
            KJH_SolarSystem.instance.unitTimeScrolbar.value = 0.5f;
        }
    }
    public void PlayObservation()
    {
        if (KJH_SolarSystem.instance.unitTimeScrolbar.value - 0.5f < 0.01f)
        {
            KJH_SolarSystem.instance.unitTimeScrolbar.value = originScrollValue;
        }
    }

    // ��ġ �̵� ���Ѿ� �� (����)
    public void ToCurTime()
    {
        obsDate = DateTime.Now;
        SetObsDateText();
    }

    public void GoBack()
    {
        controlTimeUI.SetActive(false);
        defalutUI.SetActive(true);
    }

    public void ChangeToScrollUI()
    {
        controlTimeUI.SetActive(true);
        //defalutUI.SetActive(false);
    }

    // �༺ ���� �޴� �̵�

    public void MoveCBInfoMenu(float moveX)
    {
        dict_UI["UI_Info"].SetActive(true);
        iTween.MoveTo(dict_UI["UI_Info"], iTween.Hash("x", moveX, "time", 2f));
    }

    public void ViewCBInfoMenu()
    {
        SetActiveObject("UI_InfoMenu");
    }

    // �༺ ���� ����
    public void ViewCBInfo()
    {
        SetActiveObject("UI_ViewCBInfo");
    }

    // õü ���� ���� ����
    public void ViewCBStructure()
    {
        SetActiveObject("UI_ViewCBStructure");
    }

    void SetActiveObject(string targetName)
    {
        Transform tr = dict_UI["UI_Info"].transform;
        for (int i = 1; i < tr.childCount - 3; i++)
        {
            if (tr.GetChild(i).name == targetName)
                tr.GetChild(i).gameObject.SetActive(true);
            else
                tr.GetChild(i).gameObject.SetActive(false);
        }
    }


    // õü �޴� �ݱ�
    public void CloseCBInfoMenu()
    {
        GameObject go = dict_UI["UI_Info"];

        MoveCBInfoMenu(-425f);
        // ���� UI�� ����, �⺻ UI�� ���� �ʹ�.
        StopCoroutine("IeGoSetActive");
        StartCoroutine(IeGoSetActive(go));

    }
    IEnumerator IeGoSetActive(GameObject Go_enable)
    {
        yield return new WaitForSeconds(1f);
        Go_enable.SetActive(false);
        // �⺻ �޴� ���·� ��ȯ
        ViewCBInfoMenu();
        MoveDefalutUI(120f, 120f);
    }

    // �⺻ UI Ȱ��ȭ
    public void MoveDefalutUI(float moveX, float moveY)
    {
        Transform tr = dict_UI["UI_Defalut"].transform;
        tr.gameObject.SetActive(true);
        iTween.MoveTo(tr.GetChild(0).gameObject, iTween.Hash("x", tr.GetChild(0).position.x + moveX, "Time", 2f));
        iTween.MoveTo(tr.GetChild(1).gameObject, iTween.Hash("y", tr.GetChild(1).position.y + moveY, "Time", 2f));
    }

}
