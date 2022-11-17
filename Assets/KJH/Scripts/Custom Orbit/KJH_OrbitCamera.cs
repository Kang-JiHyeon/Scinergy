using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KJH_OrbitCamera : MonoBehaviour
{
    // 회전 기준 -> cameraTarget의 position
    public Transform pivot;
    public Transform target;
    Transform cam;

    // 카메라 회전
    public float rotSpeed;
    float mx;
    float my;
    float rotY;
    float rotX;

    // 카메라 줌인/줌아웃
    Vector2 preMouse;

    public float distance = 10f;
    public float zoomSpeed = 50.0f;
    public float minDistance = 3f;
    public float maxDistance = 100f;

    bool isRot = false;
    bool isDraging = false;




    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        pivot.position = target.position;
        pivot.forward = target.forward;
    }

    // Update is called once per frame
    void Update()
    {

        // 마우스 버튼 입력에 따른 카메라 회전 제어 변수
        if (Input.GetMouseButtonDown(0))
        {
            isRot = true;
            ClickCelestials();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRot = false;
            isDraging = false;
        }

        // ui를 클릭하지 않을 때 카메라 회전 실행
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if(isDraging == false)
            {
                CameraZoom();

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
    }

    // 마우스 왼쪽 버튼 입력에 따른 카메라 회전
    void Rotate(float x, float y)
    {
        rotX += x * rotSpeed * Time.deltaTime;
        rotY += y * rotSpeed * Time.deltaTime;

        rotY = Mathf.Clamp(rotY, -90, 90);

        pivot.localEulerAngles = new Vector3(-rotY, rotX, 0);
    }

    void CameraZoom()
    {
        Vector3 dir = pivot.position - cam.position;
        dir.Normalize();
        float step = GetAxisRawScrollUniversal() * zoomSpeed;
        distance = Mathf.Clamp(distance - step, minDistance, maxDistance);

        Vector3 goalPos = pivot.position - (cam.rotation * Vector3.forward * distance);
        cam.position = Vector3.Lerp(cam.position, goalPos, Time.deltaTime * 3);

        if (Vector3.Distance(cam.position, goalPos) < 0.1f)
        {
            cam.position = goalPos;
        }
    }

    float GetAxisRawScrollUniversal()
    {
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll < 0) return -1;
        if (scroll > 0) return 1;
        return 0;
    }

    
    void ClickCelestials()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = Camera.main.farClipPlane;
        Vector3 dir = Camera.main.ScreenToWorldPoint(mouse);

        RaycastHit hitInfo;
        if(Physics.Raycast(Camera.main.transform.position, dir, out hitInfo, mouse.z)){
            if (hitInfo.transform.CompareTag("Celestial"))
            {
                isDraging = true;
            }
        }
    }
}
