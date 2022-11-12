using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;
using System.IO;


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

// �� ��ũ�� ��
// �ٸ� ���� �ø��� ����
// �ٸ� �Ʒ��� ������ �ܾƿ�


public class KJH_UIManager : MonoBehaviour
{
    public KJH_CameraTest2 cam;
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

    public bool isActiveInfo = false;
    bool isActiveControl = false;

    Dictionary<string, GameObject> dict_UI = new Dictionary<string, GameObject>();

    public KJH_SelectPlanet selectPlanet;

    // �༺ ��� ��ư
    public RectTransform trContent_CBList;
    List<Button> buttons = new List<Button>();

    // �⺻ ��ư 
    [Header("Defalut Button")]
    public List<Button> button_defaluts;
    public List<Sprite> buttonSprite_defaluts;
    public List<Sprite> buttonSprite_clicks;

    public KJH_DataManager data;

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
        //SetObsDateText();

        for (int i = 0; i < transform.childCount; i++)
        {
            // <UI_name, UI_gameObject>
            dict_UI.Add(transform.GetChild(i).name, transform.GetChild(i).gameObject);
        }

        MoveDefalutUI(1f);

        for(int i = 0; i<trContent_CBList.childCount; i++)
        {
            buttons.Add(trContent_CBList.GetChild(i).GetComponent<Button>());
            //buttons[i].onClick.AddListener(OnClick_ButtonCBList);
        }

        // ���� ��ư��
        Transform trMainUI = dict_UI["UI_Main"].transform;
        for (int i=0; i < trMainUI.childCount; i++)
        {
            Button btn = trMainUI.GetChild(i).GetComponent<Button>();
            if (btn)
            {
                button_defaluts.Add(btn);
            }
        }

    }

    private void FixedUpdate()
    {
        if (KJH_SpaceSceneManager.instance.isSolar)
        {
            OnClick_OpenCBList();
            KJH_SpaceSceneManager.instance.isSolar = false;
        }


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

        //SetObsDateText();
    }

    //void SetObsDateText()
    //{
    //    // 2022
    //    yearText.text = obsDate.ToString("yyyy");
    //    // 10�� 23��
    //    dateText.text = obsDate.ToString("MM") + "�� " + obsDate.ToString("dd") + "��";
    //    // 01�� 15�� ����
    //    timeText.text = obsDate.ToString("HH") + " : " + obsDate.ToString("mm") + " " + obsDate.ToString("tt");

    //    // 2022�� 10�� 23�� 01�� 15�� ����
    //    obsDateText.text = yearText.text + "�� " + dateText.text + " " + timeText.text;
    //}

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
        //SetObsDateText();
    }


    /* ��ư ���� �Լ� */
    // DefalutUI -> ControllTime
    public void OnClick_ControllObsTime()
    {
        MoveDefalutUI(-1f);
        MoveControllTimeUI(1f);
        isActiveControl = true;
    }

    // ControllTime -> DefalutUI
    public void OnClick_Back()
    {
        MoveControllTimeUI(-1f);
        MoveDefalutUI(1f);
        isActiveControl = false;
    }

    // õü �޴� �ݱ�
    public void OnClick_CloseInfoMenu()
    {
        GameObject go = dict_UI["UI_Info"];

        // ���� UI�� ����, �⺻ UI�� ���� �ʹ�.
        if (isActiveInfo)
        {
            MoveDefalutUI(1f);
            MoveCBInfoMenu(-1f);
        }
        Invoke("ViewCBInfoMenu", 2f);

        SetActiveModel(true);

        cam.isViewNucleus = false;
        cam.isViewUI = false;
        cam.ChangeCenter(Screen.width * 0.5f);
    }

    // ���� �޴� UI
    public void OpenInfoMenu()
    {
        if (!isActiveInfo)
        {
            Transform tr = dict_UI["UI_Info"].transform;

            tr.GetChild(0).gameObject.SetActive(true);
            tr.GetChild(1).gameObject.SetActive(false);
            tr.GetChild(2).gameObject.SetActive(false);

            tr.gameObject.SetActive(true);

            
        }
        
        if (isActiveControl)
        {
            MoveControllTimeUI(-1f);
        }

        StopObservation();
    }

    // �⺻ UI �̵� �Լ�
    public void MoveDefalutUI(float sign)
    {
        //Transform tr = dict_UI["UI_Defalut"].transform;
        //iTween.MoveTo(tr.GetChild(0).gameObject, iTween.Hash("x", tr.GetChild(0).position.x - 120f * sign, "Time", 2f));
        //iTween.MoveTo(tr.GetChild(0).gameObject, iTween.Hash("y", tr.GetChild(0).position.y + 120f * sign, "Time", 2f));
    }
    // �ð� ���� UI �̵� �Լ�
    public void MoveControllTimeUI(float sign)
    {
        //isActiveControl = sign > 0 ? true : false;
        Transform tr = dict_UI["UI_ControlTime"].transform;
        iTween.MoveTo(tr.gameObject, iTween.Hash("y", tr.position.y + 300f * sign, "Time", 2f));
    }

    // �༺ ���� �޴� �̵�
    public void MoveCBInfoMenu(float sign)
    {
        isActiveInfo = sign > 0 ? true : false;
        iTween.MoveTo(dict_UI["UI_Info"], iTween.Hash("x", 425f * sign, "time", 2f));
    }

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
        // ui ����
        SetActiveUI("UI_ViewCBStructure");

        // �� ���� : ���� ���õ� ���� �θ��� 1��° �ڽ��� ����, 2��° �ڽ��� Ų��.
        SetActiveModel(false);

        // ī�޶� : �༺ back �������� �̵�
        cam.isViewNucleus = true;

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

    public void SetActiveModel(bool isActiveAppearance)
    {
        selectPlanet.camaraTarget.parent.GetChild(0).gameObject.SetActive(isActiveAppearance);
        selectPlanet.camaraTarget.parent.GetChild(1).gameObject.SetActive(!isActiveAppearance);
    }


    /// <summary>
    /// õü ����� ��ư�� ������ �� ����Ǵ� �Լ�
    /// - ī�޶� �̵� ��Ŵ
    /// - ī�޶��� cameraTarget�� �ش� transform ���� ����
    /// </summary>
    public void OnClick_CBList(Transform target)
    {
        selectPlanet.camaraTarget = target;
        selectPlanet.focusTarget = target;

        cam.pivot.position = target.position;
        cam.distance = target.localScale.x + 5;

        dict_UI["UI_Info"].SetActive(true);
        dict_UI["UI_Info"].transform.GetChild(0).gameObject.SetActive(true);

        dict_UI["UI_CelestialList"].SetActive(false);
        ChangeSprite(dict_UI["UI_CelestialList"], 3);
        data.ChangeInfo();
        //dict_UI["UI_CelestialList"].SetActive(false);
    }



    // �������� �޴��� x ��ư Ŭ��
    public void OnClick_Close(GameObject go)
    {
        go.SetActive(false);

        if (go.name == "UI_CelestialList")
        {
            ChangeSprite(go, 0);
            ChangeSprite(go, 3);
        }

        if (go.name == "UI_InfoMenu")
        {
            ChangeSprite(go, 0);
        }


        if(go.name == "UI_ControlTime")
        {
            SS_UI.SetActive(false);
            ChangeSprite(go, 0);
            ChangeSprite(go, 4);
        }

        if (go.name == "UI_ViewCBStructure")
        {
            SetActiveModel(true);
        }
    }

    // ��������
    public void OnClick_Info()
    {
        dict_UI["UI_Info"].transform.GetChild(1).gameObject.SetActive(true);
    }

    // ���α���
    public void OnClick_Structure()
    {
        dict_UI["UI_Info"].transform.GetChild(2).gameObject.SetActive(true);

        cam.isViewNucleus = true;
        SetActiveModel(false);
    }

    // �༺ ��� UI ����
    public void OnClick_OpenCBList()
    {
        GameObject go = dict_UI["UI_CelestialList"];
        
        go.SetActive(!go.activeSelf);

        SS_UI.SetActive(false);
        ChangeSprite(go, 3);

    }

    public void OnClick_ControlTime()
    {
        GameObject go = dict_UI["UI_ControlTime"];

        go.SetActive(!go.activeSelf);
        isActiveControl = go.activeSelf;

        if (dict_UI["UI_CelestialList"].activeSelf)
        {
            dict_UI["UI_CelestialList"].SetActive(false);
            ChangeSprite(dict_UI["UI_CelestialList"], 3);
        }
        ChangeSprite(go, 4);
    }


    public void OnClick_Custom()
    {
        KJH_SpaceSceneManager.instance.Load_CustomOrbitScene();
    }


    public GameObject SS_UI;
    public void OnClick_SolarSystem()
    {
        SS_UI.SetActive(!SS_UI.activeSelf);
        ChangeSprite(SS_UI, 0);

        GameObject go_list = dict_UI["UI_CelestialList"];
        GameObject go_time = dict_UI["UI_ControlTime"];

        if(SS_UI.activeSelf == false)
        {
            Transform tr = dict_UI["UI_Info"].transform;

            tr.gameObject.SetActive(false);
            tr.GetChild(0).gameObject.SetActive(true);
            tr.GetChild(1).gameObject.SetActive(false);
            tr.GetChild(2).gameObject.SetActive(false);

            if (go_list.activeSelf)
            {
                go_list.SetActive(false);
                ChangeSprite(go_list, 3);
            }

            go_time.SetActive(false);
            ChangeSprite(go_time, 4);

            SetActiveModel(true);
        }
    }
    void ChangeSprite(GameObject go, int index)
    {
        if (go.activeSelf)
        {
            // ��Ȳ��
            button_defaluts[index].image.sprite = buttonSprite_clicks[index];
        }
        else
        {
            button_defaluts[index].image.sprite = buttonSprite_defaluts[index];
        }
    }
}
