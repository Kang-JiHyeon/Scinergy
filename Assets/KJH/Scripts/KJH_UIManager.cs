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
    //KJH_CameraMove cam;
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
        //cam = Camera.main.GetComponent<KJH_CameraMove>();
        curDate = DateTime.Now;
        obsDate = curDate;
        SetObsDateText();

        for (int i = 0; i < transform.childCount; i++)
        {
            // <UI_name, UI_gameObject>
            dict_UI.Add(transform.GetChild(i).name, transform.GetChild(i).gameObject);
            //transform.GetChild(i).gameObject.SetActive(false);
        }

        //// �⺻ UI ����
        //dict_UI["UI_Defalut"].SetActive(true);

        MoveDefalutUI(1f);
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
        if (Mathf.Abs(KJH_SolarSystem.instance.unitTimeScrolbar.value - 0.5f) >= 0.01f)
        {
            originScrollValue = KJH_SolarSystem.instance.unitTimeScrolbar.value;
            KJH_SolarSystem.instance.unitTimeScrolbar.value = 0.5f;
        }
    }
    public void PlayObservation()
    {
        if (Mathf.Abs(KJH_SolarSystem.instance.unitTimeScrolbar.value - 0.5f) < 0.01f)
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


    /* ��ư ���� �Լ� */
    // DefalutUI -> ControllTime
    public void OnClick_ControllObsTime()
    {
        MoveDefalutUI(-1f);
        MoveControllTimeUI(1f);
    }

    // ControllTime -> DefalutUI
    public void OnClick_Back()
    {
        //controlTimeUI.SetActive(false);
        //defalutUI.SetActive(true);
        //ChangeToScrollUI();
        MoveControllTimeUI(-1f);
        MoveDefalutUI(1f);
    }

    // õü �޴� �ݱ�
    public void OnClick_CloseInfoMenu()
    {
        GameObject go = dict_UI["UI_Info"];

        // ī�޶� ���������� �̵�
        //cam.targetPos = transform.right * 1.5f;
        ////cam.moveDir = -1f;
        //cam.isMove = true;


        Vector3 target = Camera.main.transform.position + Camera.main.transform.right * 2f;
        target.y = Camera.main.transform.position.y;
        iTween.MoveTo(Camera.main.gameObject, iTween.Hash("position", target));

        // ���� UI�� ����, �⺻ UI�� ���� �ʹ�.
        MoveCBInfoMenu(-1f);
        MoveDefalutUI(1f);
        Invoke("ViewCBInfoMenu", 2f);

        //StopCoroutine("IeGoSetActive");
        //StartCoroutine(IeGoSetActive(go));

    }

    // �⺻ UI �̵� �Լ�
    public void MoveDefalutUI(float sign)
    {
        Transform tr = dict_UI["UI_Defalut"].transform;
        tr.gameObject.SetActive(true);
        iTween.MoveTo(tr.GetChild(0).gameObject, iTween.Hash("x", tr.GetChild(0).position.x + 120f * sign, "Time", 2f));
        iTween.MoveTo(tr.GetChild(1).gameObject, iTween.Hash("y", tr.GetChild(1).position.y + 120f * sign, "Time", 2f));
    }
    // �ð� ���� UI �̵� �Լ�
    public void MoveControllTimeUI(float sign)
    {
        iTween.MoveTo(controlTimeUI, iTween.Hash("y", controlTimeUI.transform.position.y + 300f * sign, "Time", 2f));
    }

    // �༺ ���� �޴� �̵�
    public void MoveCBInfoMenu(float sign)
    {
        iTween.MoveTo(dict_UI["UI_Info"], iTween.Hash("x", 425f * sign, "time", 2f));
    }

    //public void ChangeToScrollUI()
    //{
    //    //defalutUI.SetActive(false);
    //    if(controlTimeUI.activeSelf == false)
    //    {
    //        MoveDefalutUI(-120, -120);
    //        controlTimeUI.SetActive(true);
    //        iTween.MoveTo(controlTimeUI, iTween.Hash("y", controlTimeUI.transform.position.y + 300f, "Time", 2f));
    //    }
    //    else
    //    {
    //        MoveDefalutUI(120, 120);
    //        iTween.MoveTo(controlTimeUI, iTween.Hash("y", controlTimeUI.transform.position.y - 300f, "Time", 2f));
    //        controlTimeUI.SetActive(false);
    //    }
    //}





    //IEnumerator IeGoSetActive(GameObject Go_enable)
    //{
    //    yield return new WaitForSeconds(1f);
    //    Go_enable.SetActive(false);
    //    // �⺻ �޴� ���·� ��ȯ
    //    ViewCBInfoMenu();
    //    MoveDefalutUI(1f);
    //}

    // ���� UI ���� �Լ�
    public void ViewCBInfoMenu()
    {
        SetActiveUI("UI_InfoMenu");
    }

    // �༺ ���� ����
    public void ViewCBInfo()
    {
        SetActiveUI("UI_ViewCBInfo");
    }

    // õü ���� ���� ����
    public void ViewCBStructure()
    {
        SetActiveUI("UI_ViewCBStructure");
    }

    // õü ���� UI �߿��� Ȱ��ȭ ����
    void SetActiveUI(string targetName)
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

}
