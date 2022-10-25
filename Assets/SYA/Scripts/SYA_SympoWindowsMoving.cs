using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_SympoWindowsMoving : MonoBehaviour
{
    //window ������Ʈ�� ���콺�� Ŭ���ϰ� �ִ� ����
    public GameObject window;
    //���콺�� �̵����� �ݿ��Ͽ� �̵��ϱ�
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
        //���� ���콺 ������ Ŭ���ϸ�
       if (Input.GetMouseButtonDown(0))
        {
            //Ŭ���� ��ũ�� ��ġ�� ���� ��ǥ�� �޾ƿ´�
                fPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        }

            //���콺�� �Է¹޴µ���
        if (Input.GetMouseButton(0))
        {
            currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //�� ���� ��ǥ���� Z�������� ���̸� ���
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //���� ���� ������Ʈ�� ������
            if (Physics.Raycast(ray, out hit))
            {
                //�� ������Ʈ�� Windows���
                if (hit.collider.name == "uWC Window Object(Clone)")
                {
                    print(hit.collider.name);
                    //ī�޶� ȸ���� ���´�

                    //���콺�� ���� ��ǥ�� ��ȭ�ϴ� ����ŭ
                    Vector3 pos = currentPos - fPos;
                    print($"currentPos : {currentPos} / fPos : {fPos}");
                    //Windows������Ʈ�� �����δ�
                    hit.collider.transform.parent.gameObject.transform.position += pos;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {

            //���콺 �Է��� ����ϸ� 
            /*if(fPos != null)
                fPos=Vector3.zero ;*/
            //�ٽ� ī�޶� ȸ���� �ϵ��� �Ѵ�
        }
    }
}
