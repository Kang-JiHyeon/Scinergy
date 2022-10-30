using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KJH_CameraTest2 : MonoBehaviour
{
    // 회전 기준 -> cameraTarget의 position
    public Transform pivot;
    // 위치 이동 담당 -> rotation
    public Transform cameraRotAxis;
    // 메인 카메라 -> moveX만큼 x축 이동
    public Camera camera;

    // 카메라 회전
    public float rotSpeed;
    float mouseX;
    float mouseY;
    float rotY;
    float rotX;

    // 카메라 줌인/줌아웃
    Vector3 velocity = Vector3.zero;
    public float distance = 10f;
    public float zoomSmoothTime = 0.2f;
    public float wheelspeed = 50.0f;
    public float minDistance = 3f;
    public float maxDistance = 100f;

    // 카메라 이동
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

    // 마우스 왼쪽 버튼 입력에 따른 카메라 회전
    void Rotate()
    {
        if (Input.GetMouseButton(0))
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y");

            rotX = Mathf.Clamp(pivot.rotation.x + mouseY * rotSpeed, -89.5f, 89.5f);
            
            rotY = pivot.rotation.y + mouseX * rotSpeed;

            // 스무스 하게 멈추는건 다음에...!!
            pivot.rotation = Quaternion.Euler(new Vector3(rotX, rotY, 0));

            print("마우스 입력 회전 중");
        }
    }

    // 마우스 휠 입력에 따른 카메라 줌인/줌아웃
    void ZoomInOut()
    {

        // 마우스 휠 입력
        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelspeed;

        if (distance < minDistance) distance = minDistance;
        if (distance > maxDistance) distance = maxDistance;
        Vector3 reverseDistance = new Vector3(0f, 0f, distance);

        //movePos = KJH_UIManager.instance.isActiveInfo ? transform.right * -1.5f : Vector3.zero;
        cameraRotAxis.position = Vector3.SmoothDamp(cameraRotAxis.position, pivot.transform.position - cameraRotAxis.rotation * reverseDistance, ref velocity, zoomSmoothTime);
    
    }

    // 카메라 이동
    // 1. 천체로 이동
    public bool isMovingToCB = false;
    public void MoveToCB()
    {
        distance = minDistance;

        if (Vector3.Distance(cameraRotAxis.position, pivot.position) < minDistance)
        {
            // 정보 ui 띄우기
            KJH_UIManager.instance.OpenInfoMenu();
            isMovingToCB = false;
            isCameraMoveX = true;
            
        }
        else
        {
            // 카메라 위치를 행성으로 이동
            cameraRotAxis.position = Vector3.Lerp(cameraRotAxis.position, pivot.position, Time.deltaTime);
        }
    }

    bool isCameraMoveX = false;
    // 2. ui 나올 때
    public void MoveXAxis()
    {
        // 메인 카메라의 위치를 왼쪽으로 이동하고 싶다.
        if (Vector3.Distance(camera.transform.localPosition, movePos) < 0.1f)
        {
            isCameraMoveX = false;
            camera.transform.localPosition = movePos;
        }

        camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, movePos, Time.deltaTime);
    }
}
