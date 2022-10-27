using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_SympoWindowsMoving : MonoBehaviour
{
    //window 오브젝트를 마우스로 클릭하고 있는 동안
    public GameObject window;
    //마우스의 이동값을 반영하여 이동하기
    Vector3 currentPos;
    Vector3 dis;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.cullingMask = ~ (1 << 18);
        
    }


    Vector3 hitPos;
    // Update is called once per frame
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //레이 끝에 오브젝트가 닿으면
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //그 오브젝트가 Windows라면
                if (hit.collider.name == "uWC Window Object(Clone)")
                {
                    hitPos = hit.point - hit.collider.gameObject.transform.position;
                }
            }
        }

            //마우스가 입력받는동안                                           
        if (Input.GetMouseButton(0))
        {
            Camera.main.transform.GetComponentInParent<SYA_PlayerRot>().enabled = false;
            Ray ray;
            //그 월드 좌표에서 Z방향으로 레이를 쏜다
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //레이 끝에 오브젝트가 닿으면
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //그 오브젝트가 Windows라면
                if (hit.collider.name == "uWC Window Object(Clone)")
                {
                    print(hit.collider.name);
                    //카메라 회전을 막는다

                    //마우스의 월드 좌표가 변화하는 값만큼
                    //Vector3 pos = currentPos - fPos;
                    //Vector3 pos = new Vector3(currentPos.x, currentPos.y, 0) - new Vector3(fPos.x, fPos.y, 0);
                    //pos.Normalize();
                    //Windows오브젝트가 움직인다
                    
                    hit.collider.gameObject.transform.position =new Vector3(hit.point.x - hitPos.x, hit.point.y - hitPos.y, window.transform.position.z);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {

            //마우스 입력을 취소하면 
            /*if(fPos != null)
                fPos=Vector3.zero ;*/
            //다시 카메라 회전을 하도록 한다
            Camera.main.transform.parent.GetComponentInParent<SYA_PlayerRot>().enabled = true;
        }
    }

/*    Vector3 HitToPos(Transform hitPoint)
    {
        Matrix4x4 matrix4 = hitPoint.worldToLocalMatrix;
        Vector3 pos = matrix4.GetPosition();
        pos.z = window.transform.position.z;
        print(pos);
        return pos;
    }*/
}
