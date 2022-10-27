using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_SympoWindowsMoving : MonoBehaviour
{
    //window ������Ʈ�� ���콺�� Ŭ���ϰ� �ִ� ����
    public GameObject window;
    //���콺�� �̵����� �ݿ��Ͽ� �̵��ϱ�
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
            //���� ���� ������Ʈ�� ������
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //�� ������Ʈ�� Windows���
                if (hit.collider.name == "uWC Window Object(Clone)")
                {
                    hitPos = hit.point - hit.collider.gameObject.transform.position;
                }
            }
        }

            //���콺�� �Է¹޴µ���                                           
        if (Input.GetMouseButton(0))
        {
            Camera.main.transform.GetComponentInParent<SYA_PlayerRot>().enabled = false;
            Ray ray;
            //�� ���� ��ǥ���� Z�������� ���̸� ���
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //���� ���� ������Ʈ�� ������
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //�� ������Ʈ�� Windows���
                if (hit.collider.name == "uWC Window Object(Clone)")
                {
                    print(hit.collider.name);
                    //ī�޶� ȸ���� ���´�

                    //���콺�� ���� ��ǥ�� ��ȭ�ϴ� ����ŭ
                    //Vector3 pos = currentPos - fPos;
                    //Vector3 pos = new Vector3(currentPos.x, currentPos.y, 0) - new Vector3(fPos.x, fPos.y, 0);
                    //pos.Normalize();
                    //Windows������Ʈ�� �����δ�
                    
                    hit.collider.gameObject.transform.position =new Vector3(hit.point.x - hitPos.x, hit.point.y - hitPos.y, window.transform.position.z);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {

            //���콺 �Է��� ����ϸ� 
            /*if(fPos != null)
                fPos=Vector3.zero ;*/
            //�ٽ� ī�޶� ȸ���� �ϵ��� �Ѵ�
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
