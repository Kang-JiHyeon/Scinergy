using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KJH_CameraTest2 : MonoBehaviour
{
    // 회전 기준 -> cameraTarget의 position
    public Transform pivot;
    // 위치 이동 담당 -> rotation
    public Transform cameraRotAxis;
    // 메인 카메라 -> moveX만큼 x축 이동
    public Transform cam;

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

    // 카메라 이동
    public Vector3 movePos;
    public KJH_UIManager uiManager;
    public KJH_SelectPlanet selectPlanet;

    Transform target;

    // 카메라 움직임 제어
    bool isStop = false;
    bool isRot = false;

    public enum CameraState
    {
        Idle,
        RightFocus
    }

    public CameraState cameraState = CameraState.Idle;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        movePos = new Vector3(-2f, 0, 0);
        selectPlanet = cam.GetComponent<KJH_SelectPlanet>();
        ChangeCenter(Screen.width * 0.7f);

        preCamPos = cam.position;
    }


    // Update is called once per frame
    void Update()
    {
        // B키 누르면 카메라 회전 및 줌인 비활성화
        if (Input.GetKeyDown(KeyCode.B))
            isStop = !isStop;

        if (isStop) return;

        // 카메라 포커스 대상
        if (selectPlanet.camaraTarget != null)
        {
            target = selectPlanet.camaraTarget;
            pivot.position = target.position;
        }

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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //tempImage.SetActive(true);
            float centerX = 600 + (Screen.width - 600) * 0.5f;
            ChangeCenter(centerX);
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

    Vector3 preCamPos;
    // 마우스 휠 입력에 따른 카메라 줌인/줌아웃
    void ZoomInOut()
    {
        //if(Input.mouseScrollDelta.y != 0)
        //{
        //    Vector3 dir = pivot.position - cam.position;
        //    dir.Normalize();
        //    cam.position += dir * Input.mouseScrollDelta.y * 0.5f;
        //    preCamPos = cam.position;

        // 마우스 휠 입력
        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelspeed;

        if (distance < minDistance) distance = minDistance;
        if (distance > maxDistance) distance = maxDistance;


        Vector3 reverseDistance = new Vector3(0f, 0f, distance);
        ////cam.position = Vector3.Lerp(cam.position, preCamPos, Time.deltaTime); 

        ////cam.position = Vector3.SmoothDamp(cam.position, dir * cam.localPosition.z, ref velocity, zoomSmoothTime);

        cam.position = Vector3.SmoothDamp(cam.position, pivot.position - cam.rotation * reverseDistance, ref velocity, zoomSmoothTime);


        //}
        //else
        //{
        //    cam.position = Vector3.Lerp(cam.position, preCamPos, Time.deltaTime);
        //}



        ////movePos = KJH_UIManager.instance.isActiveInfo ? transform.right * -1.5f : Vector3.zero;
        //Vector3 dir = pivot.position - cam.position;
        //dir.Normalize();

        //// 기본
        //Debug.DrawLine(cam.position, pivot.transform.position - (dir + reverseDistance), Color.red);

        //// ui 나온 상태
        //Debug.DrawLine(cam.position, (dir + reverseDistance), Color.green);

        //print(cam.rotation);
    }

    // 카메라 이동
    // 1. 천체로 이동
    public bool isMovingToCB = false;
    public void MoveToCB()
    {
        distance = minDistance;

        if (Vector3.Distance(pivot.position, target.position) < minDistance)
        {
            // 정보 ui 띄우기
            KJH_UIManager.instance.OpenInfoMenu();
            isMovingToCB = false;
            //movePos = Vector3.left;
            isCameraMoveX = true;

        }
        else
        {
            // 카메라 위치를 행성으로 이동
            pivot.position = Vector3.Lerp(pivot.position, target.position, Time.deltaTime);
        }
    }

    public bool isCameraMoveX = false;
    // 2. ui 나올 때
    public void MoveXAxis()
    {
        // 메인 카메라의 위치를 왼쪽으로 이동하고 싶다.
        if (Vector3.Distance(cam.transform.localPosition, movePos) < 0.005f)
        {
            isCameraMoveX = false;
            cam.transform.localPosition = movePos;
        }

        cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, movePos, Time.deltaTime * 2f);
    }


    public void ChangeCenter(float x)
    {
        Vector3 viewCenter = new Vector3(x, Screen.height * 0.5f, -cam.localPosition.z);
        Vector3 pos = Camera.main.ScreenToWorldPoint(viewCenter);
        Vector3 gap = selectPlanet.camaraTarget.transform.position - pos;

        cam.position += gap;
    }
}
