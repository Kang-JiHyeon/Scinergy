using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. �༺ ���� ���콺�� ��ġ�ϸ� �׵θ��� Ȱ��ȭ�ϰ� �ʹ�. (v)

// 2. �༺�� Ŭ���ϸ� �༺ �ֺ��� [ ]�� ��Ÿ����, �����ʿ� Ž���ϱ� UI ��ư�� ��Ÿ���� �ϰ� �ʹ�.
// 2-1. Ž���ϱ� ��ư�� ������ ���� �༺ ���� UI�� ��Ÿ����.
// 2-2. �湮�ϱ� ��ư�� ������ �༺���� �ٰ�����.
// 2-3. �������� ��ư�� ������ �༺ ���� UI�� ����ȴ�.
// 2-4. ���α��� ��ư�� ������ ���α��� �������� �ٲ��, ���α����� ���̴� �༺ ������Ʈ�� ��ȯ�ȴ�.

// 3. Ž���ϱ� ��ư�� �����ų� ���� Ŭ���ϸ� �༺���� �ٰ����� �ʹ�.

public class KJH_SelectPlanet : MonoBehaviour
{
    Outline outlineScript;
    Transform hitObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1. �༺ ���� ���콺 ��ġ�� ��� outline Ȱ��ȭ
        EnableOutLine();



        // 2. �༺�� Ŭ������ �� ��Ŀ�� �̹��� Ȱ��ȭ
        ClickPlanet();
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
        }

    }
    Transform target;
    private void ClickPlanet()
    {
        if (Input.GetMouseButton(0))  // ���콺�� Ŭ�� �Ǹ�
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
                target = hit.transform;

            }

            //if (target)
            //{
            //    transform.position = Vector3.Lerp(transform.position, hit.point, Time.deltaTime);
            //}
        }
    }

    private void EnableOutLine()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            hitObject = hit.transform;
            

            outlineScript = hitObject.GetComponent<Outline>();

            // hit �� ����� outline.cs �� �����ͼ� Ȱ��ȭ�Ѵ�.
            if (outlineScript != null)
                outlineScript.enabled = true;

        }
        else if (outlineScript != null)
        {
            outlineScript.enabled = false;
        }
    }
}
