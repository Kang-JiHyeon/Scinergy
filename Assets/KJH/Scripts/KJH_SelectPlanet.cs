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
    //KJH_CameraMove cam;
    public List<GameObject> UIs;

    Outline outlineScript;
    Transform mouseTarget;
    public Transform clickTarget;
    KJH_Focus focusScript;
    OrbitCamera orbitCamera;

    public float camRotSpeed = 0.2f;

    //Double Click 처리용 변수
    public float m_DoubleClickSecond = 0.25f;
    private bool m_IsOneClick = false;
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
        //UpdateCBInfoAction += ChangeInfo;
        //cam = GetComponent<KJH_CameraMove>();
        orbitCamera = GetComponent<OrbitCamera>();
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
            // 카메라 위치를 행성으로 이동
            if (Vector3.Distance(transform.position, clickTarget.position) > 5f)
            {
                transform.position = Vector3.Lerp(transform.position, clickTarget.position, Time.deltaTime);
            }
            // 카메라 위치 이동 끝나면
            else
            {
                isMove = false;

                // ui 변경
                //if (UIs[1].activeSelf == false)
                //{
                    KJH_UIManager.instance.MoveDefalutUI(-1f);
                    KJH_UIManager.instance.MoveCBInfoMenu(1f);

                    //iTween.MoveTo(gameObject, iTween.Hash("x", 5f, "time", 2f));

                    // 카메라 왼쪽으로 이동
                    //cam.targetPos =  transform.right * -1.5f;
                    ////cam.moveDir = -1f;
                    //cam.isMove = true;
                    //transform.LookAt(clickTarget);
                    Vector3 target = transform.position + transform.right * -2f;
                    target.y = transform.position.y;
                    iTween.MoveTo(gameObject, iTween.Hash("position", target));
                //}
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

    public KJH_CSVTest infoData;
    public GameObject DBInfoFactory;
    void ChangeInfo()
    {
        int index = infoData.cbNames.FindIndex(x => x == clickTarget.name);

        if(infoData.infos.Count > index && index > 0)
        {
            CBName.text = infoData.infos[index][0];
            CBType.text = infoData.infos[index][1];

            // Scroll View의 Content 추가

        }

    }

    // 행성 클릭하면 target 지정
    // 빈곳 클릭하면 target = null -> focus 축소
    // 마우스 멀어지면 focus 크기 확대
    // 마우스 행성 위에 있으면 focus 축소

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

            ChangeInfo();

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
