using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_SympoWindowsMoving : MonoBehaviour
{
    //window 오브젝트를 마우스로 클릭하고 있는 동안
    public GameObject window;
    //마우스의 이동값을 반영하여 이동하기
    Vector3 fPos;
    Vector3 currentPos;
    Vector3 dis;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.cullingMask = ~ (1 << 18);
        
    }

            Ray ray;
            RaycastHit hit;
        bool click=false;
    // Update is called once per frame
    void Update()
    {
        //만약 마우스 왼쪽을 클릭하면
       if (Input.GetMouseButtonDown(0))
        {
            //클릭한 스크린 위치를 월드 좌표로 받아온다
                fPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        }

            //마우스가 입력받는동안
        if (Input.GetMouseButton(0))
        {
            currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //그 월드 좌표에서 Z방향으로 레이를 쏜다
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //레이 끝에 오브젝트가 닿으면
            if (Physics.Raycast(ray, out hit))
            {
                //그 오브젝트가 Windows라면
                if (hit.collider.name == "uWC Window Object(Clone)")
                {
                    print(hit.collider.name);
                    //카메라 회전을 막는다

                    //마우스의 월드 좌표가 변화하는 값만큼
                    Vector3 pos = currentPos - fPos;
                    print($"currentPos : {currentPos} / fPos : {fPos}");
                    //Windows오브젝트가 움직인다
                    hit.collider.transform.parent.gameObject.transform.position += pos;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {

            //마우스 입력을 취소하면 
            /*if(fPos != null)
                fPos=Vector3.zero ;*/
            //다시 카메라 회전을 하도록 한다
        }
    }
}
