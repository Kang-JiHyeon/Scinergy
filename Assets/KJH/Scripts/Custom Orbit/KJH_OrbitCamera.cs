using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KJH_OrbitCamera : MonoBehaviour
{
    // ȸ�� ���� -> cameraTarget�� position
    public Transform pivot;
    public Transform target;
    Transform cam;

    // ī�޶� ȸ��
    public float rotSpeed;
    float mx;
    float my;
    float rotY;
    float rotX;

    // ī�޶� ����/�ܾƿ�
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

        // ���콺 ��ư �Է¿� ���� ī�޶� ȸ�� ���� ����
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

        // ui�� Ŭ������ ���� �� ī�޶� ȸ�� ����
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if(isDraging == false)
            {
                CameraZoom();

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
    }

    // ���콺 ���� ��ư �Է¿� ���� ī�޶� ȸ��
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
