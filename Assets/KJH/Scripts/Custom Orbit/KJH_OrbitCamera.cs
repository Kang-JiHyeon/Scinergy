using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KJH_OrbitCamera : MonoBehaviour
{
    // 회전 기준 -> cameraTarget의 position
    public Transform pivot;
    Transform cam;

    // 카메라 회전
    public float rotSpeed;
    float mx;
    float my;
    float rotY;
    float rotX;


    // 카메라 줌인/줌아웃
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
        // 카메라 포커스 대상

        pivot.position = Vector3.zero;

        // 마우스 버튼 입력에 따른 카메라 회전 제어 변수
        if (Input.GetMouseButtonDown(0))
            isRot = true;

        if (Input.GetMouseButtonUp(0))
            isRot = false;

        // ui를 클릭하지 않을 때 카메라 회전 실행
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            ZoomInOut();

            // 마우스 왼쪽 버튼을 누르고 있을 때
            if (isRot)
            {
                // 이전 마우스 입력값
                preMouse.x = mx;
                preMouse.y = my;

                // 현재 마우스 입력값
                mx = Input.GetAxis("Mouse X");
                my = Input.GetAxis("Mouse Y");

                Rotate(mx, my);
            }
            // 마우스 왼쪽 버튼을 누르고 있지 않을 때
            else
            {
                Rotate(preMouse.x, preMouse.y);
                preMouse = Vector2.Lerp(preMouse, Vector2.zero, Time.deltaTime);
            }

        }
    }

    // 마우스 왼쪽 버튼 입력에 따른 카메라 회전
    void Rotate(float x, float y)
    {
        rotX += x * rotSpeed * Time.deltaTime;
        rotY += y * rotSpeed * Time.deltaTime;

        rotY = Mathf.Clamp(rotY, -90, 90);

        pivot.localEulerAngles = new Vector3(-rotY, rotX, 0);
    }


    void ZoomInOut()
    {

        // 마우스 휠 입력
        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelspeed;

        if (distance < minDistance) distance = minDistance;
        if (distance > maxDistance) distance = maxDistance;


        Vector3 reverseDistance = new Vector3(0f, 0f, distance);

        cam.position = Vector3.SmoothDamp(cam.position, pivot.position - cam.rotation * reverseDistance, ref velocity, zoomSmoothTime);


    }
}
