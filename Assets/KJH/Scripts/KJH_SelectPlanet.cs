using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    KJH_Focus focusScript;
    OrbitCamera orbitCamera;

    public float camRotSpeed = 0.2f;

    //Double Click 처리용 변수
    public float m_DoubleClickSecond = 0.25f;
    private bool m_IsOneClick = false;
    private double m_Timer = 0;

    bool isMove = false;

    // Start is called before the first frame update
    void Start()
    {
        orbitCamera = transform.GetComponent<OrbitCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 행성 위에 마우스 위치할 경우 outline 활성화
        EnableOutLine();

        // 2. 행성을 더블 클릭했을 때 포커스 이미지 활성화

        if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond))
        {
            m_IsOneClick = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //ClickPlanet();

            if (!m_IsOneClick)
            {
                m_Timer = Time.time;
                m_IsOneClick = true;
            }
            // 더블클릭
            else if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
            {
                ClickPlanet();
                isMove = true;
                m_IsOneClick = false;

            }
        }

        if (clickTarget && isMove)
        {
            if (UIs[1].activeSelf == false)
            {
                KJH_UIManager.instance.MoveDefalutUI(-120f, -120f);
                KJH_UIManager.instance.MoveCBInfoMenu(425f);

                //UIs[0].SetActive(false);
                //UIs[1].SetActive(true);

                //iTween.MoveTo(UIs[1], iTween.Hash("x", 425f, "time", 2f));

            }
            

            // 위치 이동
            if (Vector3.Distance(transform.position, clickTarget.position) > 5f)
            {
                transform.position = Vector3.Lerp(transform.position, clickTarget.position, Time.deltaTime);
            }
            else
            {
                isMove = false;
            }

            // 각도 회전
            Vector3 dir = clickTarget.position - transform.position;
            if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(dir)) <= 0f)
            {
                transform.forward = dir;
            }
            else
            {
                transform.forward = Vector3.Lerp(transform.forward, dir, camRotSpeed * Time.deltaTime);
            }
        }
    }


    // 행성 클릭하면 target 지정
    // 빈곳 클릭하면 target = null -> focus 축소
    // 마우스 멀어지면 focus 크기 확대
    // 마우스 행성 위에 있으면 focus 축소

    Transform clickTarget;
    private void ClickPlanet()
    {
        // 카메라가 보는 방향과, 시야를 가져온다.
        Vector3 mos = Input.mousePosition;
        mos.z = Camera.main.farClipPlane;

        // 월드의 좌표를 클릭했을 때 화면에 자신이 보고있는 화면에 맞춰 좌표를 바꿔준다.
        Vector3 dir = Camera.main.ScreenToWorldPoint(mos);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, mos.z))
        {
            print("Mouse Click Object : " + hit.transform.gameObject.name);

            clickTarget = hit.transform;
            orbitCamera.target = clickTarget;

            focusScript = clickTarget.GetComponent<KJH_Focus>();
            if (focusScript)
            {
                focusScript.ChangeFocusScale(0.3f);
            }
        }
        else if (focusScript || (clickTarget && (clickTarget != mouseTarget)))
        {
            focusScript.ChangeFocusScale(0f);
            focusScript = null;
            clickTarget = null;
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
            if ((mouseTarget == clickTarget) && focusScript)
            {
                focusScript.ChangeFocusScale(0);
                isChangeFocus = false;
                print("포커스 축소");
            }

        }
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
