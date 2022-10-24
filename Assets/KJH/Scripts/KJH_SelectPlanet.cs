using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 행성 위에 마우스가 위치하면 테두리를 활성화하고 싶다. (v)

// 2. 행성을 클릭하면 행성 주변에 [ ]가 나타나고, 오른쪽에 탐사하기 UI 버튼을 나타나게 하고 싶다.
// 2-1. 탐사하기 버튼을 누르면 왼쪽 행성 정보 UI가 나타난다.
// 2-2. 방문하기 버튼을 누르면 행성으로 다가간다.
// 2-3. 정보보기 버튼을 누르면 행성 정보 UI로 변경된다.
// 2-4. 내부구조 버튼을 누르면 내부구조 내용으로 바뀌고, 내부구조가 보이는 행성 오브젝트로 변환된다.

// 3. 탐사하기 버튼을 누르거나 더블 클릭하면 행성으로 다가가고 싶다.

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
        // 1. 행성 위에 마우스 위치할 경우 outline 활성화
        EnableOutLine();



        // 2. 행성을 클릭했을 때 포커스 이미지 활성화
        ClickPlanet();
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
        }

    }
    Transform target;
    private void ClickPlanet()
    {
        if (Input.GetMouseButton(0))  // 마우스가 클릭 되면
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

            // hit 한 대상의 outline.cs 를 가져와서 활성화한다.
            if (outlineScript != null)
                outlineScript.enabled = true;

        }
        else if (outlineScript != null)
        {
            outlineScript.enabled = false;
        }
    }
}
