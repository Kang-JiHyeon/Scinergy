using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 1. �༺ ���� ���콺�� ��ġ�ϸ� �׵θ��� Ȱ��ȭ�ϰ� �ʹ�. (v)

// 2. �༺�� Ŭ���ϸ� �༺ �ֺ��� [ ]�� ��Ÿ����, �����ʿ� Ž���ϱ� UI ��ư�� ��Ÿ���� �ϰ� �ʹ�.
// 2-1. Ž���ϱ� ��ư�� ������ ���� �༺ ���� UI�� ��Ÿ����.
// 2-2. �湮�ϱ� ��ư�� ������ �༺���� �ٰ�����.
// 2-3. �������� ��ư�� ������ �༺ ���� UI�� ����ȴ�.
// 2-4. ���α��� ��ư�� ������ ���α��� �������� �ٲ��, ���α����� ���̴� �༺ ������Ʈ�� ��ȯ�ȴ�.

// 3. Ž���ϱ� ��ư�� �����ų� ���� Ŭ���ϸ� �༺���� �ٰ����� �ʹ�.

public class KJH_SelectPlanet : MonoBehaviour
{
    public List<GameObject> UIs;

    Outline outlineScript;
    Transform mouseTarget;
    KJH_Focus focusScript;
    OrbitCamera orbitCamera;

    public float camRotSpeed = 0.2f;

    //Double Click ó���� ����
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
        // 1. �༺ ���� ���콺 ��ġ�� ��� outline Ȱ��ȭ
        EnableOutLine();

        // 2. �༺�� ���� Ŭ������ �� ��Ŀ�� �̹��� Ȱ��ȭ

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
            // ����Ŭ��
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
            

            // ��ġ �̵�
            if (Vector3.Distance(transform.position, clickTarget.position) > 5f)
            {
                transform.position = Vector3.Lerp(transform.position, clickTarget.position, Time.deltaTime);
            }
            else
            {
                isMove = false;
            }

            // ���� ȸ��
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


    // �༺ Ŭ���ϸ� target ����
    // ��� Ŭ���ϸ� target = null -> focus ���
    // ���콺 �־����� focus ũ�� Ȯ��
    // ���콺 �༺ ���� ������ focus ���

    Transform clickTarget;
    private void ClickPlanet()
    {
        // ī�޶� ���� �����, �þ߸� �����´�.
        Vector3 mos = Input.mousePosition;
        mos.z = Camera.main.farClipPlane;

        // ������ ��ǥ�� Ŭ������ �� ȭ�鿡 �ڽ��� �����ִ� ȭ�鿡 ���� ��ǥ�� �ٲ��ش�.
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

            // hit �� ����� outline.cs �� �����ͼ� Ȱ��ȭ�Ѵ�.
            if (outlineScript != null)
                outlineScript.enabled = true;

            // ���콺�� �༺ ���� ���� ��, ��Ŀ�� �� �۰��Ѵ�.
            if ((mouseTarget == clickTarget) && focusScript)
            {
                focusScript.ChangeFocusScale(0);
                isChangeFocus = false;
                print("��Ŀ�� ���");
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
