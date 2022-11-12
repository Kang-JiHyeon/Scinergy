using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 1. 행성 위에 마우스가 위치하면 테두리를 활성화하고 싶다. (v)

// 2. 행성을 클릭하면 행성 주변에 [ ]가 나타나고, 오른쪽에 탐사하기 UI 버튼을 나타나게 하고 싶다.
// 2-1. 탐사하기 버튼을 누르면 왼쪽 행성 정보 UI가 나타난다.
// 2-2. 방문하기 버튼을 누르면 행성으로 다가간다.
// 2-3. 정보보기 버튼을 누르면 행성 정보 UI로 변경된다.
// 2-4. 내부구조 버튼을 누르면 내부구조 내용으로 바뀌고, 내부구조가 보이는 행성 오브젝트로 변환된다.

// 3. 탐사하기 버튼을 누르거나 더블 클릭하면 행성으로 다가가고 싶다.

public class KJH_SelectPlanet : MonoBehaviour
{
    public List<GameObject> UIs;

    Outline outlineScript;
    Transform mouseTarget;
    public Transform camaraTarget;
    public Transform focusTarget;
    KJH_Focus focusScript;
    public KJH_CameraTest2 cam;

    public float camRotSpeed = 0.2f;

    //Double Click 처리용 변수
    public float m_DoubleClickSecond = 0.25f;
    private bool m_isOneClick = false;
    private double m_Timer = 0;

    bool isMove = false;

    // 천체이름
    public Text CBName;
    // 천체종류
    public Text CBType;
    public Text CBInfo;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ui를 클릭했을 때 실행되지 않도록 반환
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // 1. 행성 위에 마우스 위치할 경우 outline 활성화
        EnableOutLine();

        // 2. 행성을 더블 클릭했을 때 포커스 이미지 활성화
        if (m_isOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond))
        {
            m_isOneClick = false;
        }

        // 마우스 1번 클릭
        if (Input.GetMouseButtonDown(0))
        {
            if (!m_isOneClick)
            {
                m_Timer = Time.time;
                m_isOneClick = true;
                ClickPlanet();
            }
            // 마우스 2번 클릭
            else if (m_isOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
            {
                isMove = true;
                m_isOneClick = false;
                ClickPlanet();
            }
        }


        print("focusTarget : " + focusTarget);
        print("camaraTarget : " + camaraTarget);
    }

    // 행성 클릭하면 target 지정
    // 빈곳 클릭하면 target = null -> focus 축소
    // 마우스 멀어지면 focus 크기 확대
    // 마우스 행성 위에 있으면 focus 축소

    public KJH_DataManager dataManager;

    private void ClickPlanet()
    {
        // 카메라가 보는 방향과, 시야를 가져온다.
        Vector3 mos = Input.mousePosition;
        mos.z = Camera.main.farClipPlane;

        // 월드의 좌표를 클릭했을 때 화면에 자신이 보고있는 화면에 맞춰 좌표를 바꿔준다.
        Vector3 dir = Camera.main.ScreenToWorldPoint(mos);

        RaycastHit hit;
        //if (Physics.Raycast(transform.position, dir, out hit, mos.z))
        if (Physics.Raycast(transform.position, dir, out hit, mos.z))
        {
            // 1번 클릭
            if (m_isOneClick)
            {   
                // 기존의 타겟 포커싱 비활성화
                if(focusTarget != hit.transform && focusTarget != null)
                {
                    focusTarget.GetComponent<KJH_Focus>().ChangeFocusScale(0f);
                }

                // 포커스 타겟 설정
                focusTarget = hit.transform;
                // focus 타겟의 정보 내용으로 ui로 변경
                dataManager.ChangeInfo();

                // focus ui 크기 변경
                if(focusTarget != null)
                {
                    focusScript = focusTarget.GetComponent<KJH_Focus>();
                    if (focusScript)
                    {
                        focusScript.ChangeFocusScale(0.3f);
                    }
                }
            }
            // 2번 클릭
            else
            {
                if(camaraTarget != hit.transform)
                {
                    // 중심 target 설정
                    camaraTarget = hit.transform;
                    // 카메라 움직임
                    cam.isMovingToCB = true;
                }
                else
                {
                    if(KJH_UIManager.instance.isActiveInfo == false)
                    {
                        // 정보 ui 띄우기
                        KJH_UIManager.instance.OpenInfoMenu();
                    }
                }
            }

        }
        // 클릭한 대상이 null 일 때
        else
        {
            if (m_isOneClick)
            {
                if (focusScript)
                {
                    focusScript.ChangeFocusScale(0f);
                    focusTarget = null;
                    focusScript = null;
                }
            }
            else
                camaraTarget = null;
        }
    }

    public bool isChangeFocus = false;
    private void EnableOutLine()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            mouseTarget = hit.transform;
            outlineScript = mouseTarget.GetComponent<Outline>();

            // hit 한 대상의 outline.cs 를 가져와서 활성화한다.
            if (outlineScript != null)
                outlineScript.enabled = true;

            // 마우스가 행성 위에 있을 때, 포커스 를 작게한다.
            if ((mouseTarget == camaraTarget) && focusScript)
            {
                focusScript.ChangeFocusScale(0);
                isChangeFocus = false;
            }
        }
        // 마우스가 행성 위에 위치하고 있지 않을 때
        else
        {
            if (outlineScript != null)
            {
                outlineScript.enabled = false;
            }

            if (focusScript)
            {
                focusScript.ChangeFocusScale(0.3f);
            }
        }
    }
}
