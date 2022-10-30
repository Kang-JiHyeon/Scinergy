using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KJH_CameraTest2 : MonoBehaviour
{
    // ȸ�� ���� -> cameraTarget�� position
    public Transform pivot;
    // ��ġ �̵� ��� -> rotation
    public Transform cameraRotAxis;
    // ���� ī�޶� -> moveX��ŭ x�� �̵�
    public Camera camera;

    // ī�޶� ȸ��
    public float rotSpeed;
    float mouseX;
    float mouseY;
    float rotY;
    float rotX;

    // ī�޶� ����/�ܾƿ�
    Vector3 velocity = Vector3.zero;
    public float distance = 10f;
    public float zoomSmoothTime = 0.2f;
    public float wheelspeed = 50.0f;
    public float minDistance = 3f;
    public float maxDistance = 100f;

    // ī�޶� �̵�
    public Vector3 movePos;
    public KJH_UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        movePos = new Vector3(-2f, 0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        //pivot.position = camera.GetComponent<KJH_SelectPlanet>().camaraTarget.transform.position;
        cameraRotAxis.LookAt(pivot.position);
        Rotate();
        ZoomInOut();

        if (isCameraMoveX)
        {

            if (uiManager.isActiveInfo)
            {
                movePos = Vector3.left ;
            }
            else
            {
                movePos = Vector3.zero;
            }
            MoveXAxis();
        }
            camera.transform.forward = cameraRotAxis.forward;

    }

    // ���콺 ���� ��ư �Է¿� ���� ī�޶� ȸ��
    void Rotate()
    {
        if (Input.GetMouseButton(0))
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y");

            rotX = Mathf.Clamp(pivot.rotation.x + mouseY * rotSpeed, -89.5f, 89.5f);
            
            rotY = pivot.rotation.y + mouseX * rotSpeed;

            // ������ �ϰ� ���ߴ°� ������...!!
            pivot.rotation = Quaternion.Euler(new Vector3(rotX, rotY, 0));

            print("���콺 �Է� ȸ�� ��");
        }
    }

    // ���콺 �� �Է¿� ���� ī�޶� ����/�ܾƿ�
    void ZoomInOut()
    {

        // ���콺 �� �Է�
        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelspeed;

        if (distance < minDistance) distance = minDistance;
        if (distance > maxDistance) distance = maxDistance;
        Vector3 reverseDistance = new Vector3(0f, 0f, distance);

        //movePos = KJH_UIManager.instance.isActiveInfo ? transform.right * -1.5f : Vector3.zero;
        cameraRotAxis.position = Vector3.SmoothDamp(cameraRotAxis.position, pivot.transform.position - cameraRotAxis.rotation * reverseDistance, ref velocity, zoomSmoothTime);
    
    }

    // ī�޶� �̵�
    // 1. õü�� �̵�
    public bool isMovingToCB = false;
    public void MoveToCB()
    {
        distance = minDistance;

        if (Vector3.Distance(cameraRotAxis.position, pivot.position) < minDistance)
        {
            // ���� ui ����
            KJH_UIManager.instance.OpenInfoMenu();
            isMovingToCB = false;
            isCameraMoveX = true;
            
        }
        else
        {
            // ī�޶� ��ġ�� �༺���� �̵�
            cameraRotAxis.position = Vector3.Lerp(cameraRotAxis.position, pivot.position, Time.deltaTime);
        }
    }

    bool isCameraMoveX = false;
    // 2. ui ���� ��
    public void MoveXAxis()
    {
        // ���� ī�޶��� ��ġ�� �������� �̵��ϰ� �ʹ�.
        if (Vector3.Distance(camera.transform.localPosition, movePos) < 0.1f)
        {
            isCameraMoveX = false;
            camera.transform.localPosition = movePos;
        }

        camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, movePos, Time.deltaTime);
    }
}
