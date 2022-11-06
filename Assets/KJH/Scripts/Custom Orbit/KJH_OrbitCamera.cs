using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KJH_OrbitCamera : MonoBehaviour
{
    // ȸ�� ���� -> cameraTarget�� position
    public Transform pivot;
    Transform cam;

    // ī�޶� ȸ��
    public float rotSpeed;
    float mx;
    float my;
    float rotY;
    float rotX;


    // ī�޶� ����/�ܾƿ�
    Vector2 preMouse;
    Vector3 preCameraPos;

    Vector3 velocity = Vector3.zero;
    public float distance = 10f;
    public float zoomSmoothTime = 0.2f;
    public float wheelspeed = 50.0f;
    public float minDistance = 3f;
    public float maxDistance = 100f;

    public KJH_SelectPlanet selectPlanet;
    public Transform target;
    bool isRot = false;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // ī�޶� ��Ŀ�� ���

        pivot.position = Vector3.zero;

        // ���콺 ��ư �Է¿� ���� ī�޶� ȸ�� ���� ����
        if (Input.GetMouseButtonDown(0))
            isRot = true;

        if (Input.GetMouseButtonUp(0))
            isRot = false;

        // ui�� Ŭ������ ���� �� ī�޶� ȸ�� ����
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            ZoomInOut();

            // ���콺 ���� ��ư�� ������ ���� ��
            if (isRot)
            {
                // ���� ���콺 �Է°�
                preMouse.x = mx;
                preMouse.y = my;

                // ���� ���콺 �Է°�
                mx = Input.GetAxis("Mouse X");
                my = Input.GetAxis("Mouse Y");

                Rotate(mx, my);
            }
            // ���콺 ���� ��ư�� ������ ���� ���� ��
            else
            {
                Rotate(preMouse.x, preMouse.y);
                preMouse = Vector2.Lerp(preMouse, Vector2.zero, Time.deltaTime);
            }

        }
    }

    // ���콺 ���� ��ư �Է¿� ���� ī�޶� ȸ��
    void Rotate(float x, float y)
    {
        rotX += x * rotSpeed * Time.deltaTime;
        rotY += y * rotSpeed * Time.deltaTime;

        rotY = Mathf.Clamp(rotY, -90, 90);

        pivot.localEulerAngles = new Vector3(-rotY, rotX, 0);
    }


    void ZoomInOut()
    {

        // ���콺 �� �Է�
        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelspeed;

        if (distance < minDistance) distance = minDistance;
        if (distance > maxDistance) distance = maxDistance;


        Vector3 reverseDistance = new Vector3(0f, 0f, distance);

        cam.position = Vector3.SmoothDamp(cam.position, pivot.position - cam.rotation * reverseDistance, ref velocity, zoomSmoothTime);


    }
}
