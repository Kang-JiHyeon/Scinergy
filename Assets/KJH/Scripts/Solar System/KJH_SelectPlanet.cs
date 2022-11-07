using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public Transform camaraTarget;
    public Transform focusTarget;
    KJH_Focus focusScript;
    public KJH_CameraTest2 cam;

    public float camRotSpeed = 0.2f;

    //Double Click ó���� ����
    public float m_DoubleClickSecond = 0.25f;
    private bool m_isOneClick = false;
    private double m_Timer = 0;

    bool isMove = false;

    // õü�̸�
    public Text CBName;
    // õü����
    public Text CBType;
    public Text CBInfo;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ui�� Ŭ������ �� ������� �ʵ��� ��ȯ
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // 1. �༺ ���� ���콺 ��ġ�� ��� outline Ȱ��ȭ
        EnableOutLine();

        // 2. �༺�� ���� Ŭ������ �� ��Ŀ�� �̹��� Ȱ��ȭ
        if (m_isOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond))
        {
            m_isOneClick = false;
        }

        // ���콺 1�� Ŭ��
        if (Input.GetMouseButtonDown(0))
        {
            if (!m_isOneClick)
            {
                m_Timer = Time.time;
                m_isOneClick = true;
                ClickPlanet();
            }
            // ���콺 2�� Ŭ��
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

    // �༺ Ŭ���ϸ� target ����
    // ��� Ŭ���ϸ� target = null -> focus ���
    // ���콺 �־����� focus ũ�� Ȯ��
    // ���콺 �༺ ���� ������ focus ���

    public KJH_DataManager dataManager;

    private void ClickPlanet()
    {
        // ī�޶� ���� �����, �þ߸� �����´�.
        Vector3 mos = Input.mousePosition;
        mos.z = Camera.main.farClipPlane;

        // ������ ��ǥ�� Ŭ������ �� ȭ�鿡 �ڽ��� �����ִ� ȭ�鿡 ���� ��ǥ�� �ٲ��ش�.
        Vector3 dir = Camera.main.ScreenToWorldPoint(mos);

        RaycastHit hit;
        //if (Physics.Raycast(transform.position, dir, out hit, mos.z))
        if (Physics.Raycast(transform.position, dir, out hit, mos.z))
        {
            // 1�� Ŭ��
            if (m_isOneClick)
            {   
                // ������ Ÿ�� ��Ŀ�� ��Ȱ��ȭ
                if(focusTarget != hit.transform && focusTarget != null)
                {
                    focusTarget.GetComponent<KJH_Focus>().ChangeFocusScale(0f);
                }

                // ��Ŀ�� Ÿ�� ����
                focusTarget = hit.transform;
                // focus Ÿ���� ���� �������� ui�� ����
                dataManager.ChangeInfo();

                // focus ui ũ�� ����
                if(focusTarget != null)
                {
                    focusScript = focusTarget.GetComponent<KJH_Focus>();
                    if (focusScript)
                    {
                        focusScript.ChangeFocusScale(0.3f);
                    }
                }
            }
            // 2�� Ŭ��
            else
            {
                if(camaraTarget != hit.transform)
                {
                    // �߽� target ����
                    camaraTarget = hit.transform;
                    // ī�޶� ������
                    cam.isMovingToCB = true;
                }
                else
                {
                    if(KJH_UIManager.instance.isActiveInfo == false)
                    {
                        // ���� ui ����
                        KJH_UIManager.instance.OpenInfoMenu();
                    }
                }
            }

        }
        // Ŭ���� ����� null �� ��
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

            // hit �� ����� outline.cs �� �����ͼ� Ȱ��ȭ�Ѵ�.
            if (outlineScript != null)
                outlineScript.enabled = true;

            // ���콺�� �༺ ���� ���� ��, ��Ŀ�� �� �۰��Ѵ�.
            if ((mouseTarget == camaraTarget) && focusScript)
            {
                focusScript.ChangeFocusScale(0);
                isChangeFocus = false;
            }
        }
        // ���콺�� �༺ ���� ��ġ�ϰ� ���� ���� ��
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
