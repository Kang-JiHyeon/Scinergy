using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. ���콺 �ٷ� ����/�ܾƿ� (v)
// 2. ���콺 Ŭ������ ī�޶� �����¿� ȸ��

public class KJH_CameraMove : MonoBehaviour
{
    // �ٶ� ���
    public Transform target;

    public float distance = 10f;

    public float zoomSmoothTime = 0.2f;

    Vector3 velocity = Vector3.zero;

    public float wheelspeed = 10.0f;

    public float minDistance = 2f;
    public float maxDistance = 30f;

    KJH_SelectPlanet selectPlanet;

    // Start is called before the first frame update
    void Start()
    {
        selectPlanet = GetComponent<KJH_SelectPlanet>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectPlanet.clickTarget != null)
        {
            target = selectPlanet.clickTarget;
        }
        // �׻� Ÿ�� �ٶ󺸵���
        transform.LookAt(target);


        if (isPossibleZoom)
        {
            ZoomInOut();
        }
            RotateScreen();


    }

    void ZoomInOut()
    {
        // ���콺 �� �Է�
        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelspeed;

        if (distance < minDistance) distance = minDistance;
        if (distance > maxDistance) distance = maxDistance;  


        Vector3 reverseDistance = new Vector3(0f, 0f, distance);
        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position - transform.rotation * reverseDistance, ref velocity, zoomSmoothTime);
    }

    public Vector3 previousPosition;
    public float distanceFromSphere = 50f;
    public Vector3 direction;
    public Vector3 smoothVelocity = Vector3.zero;
    public float rotSmoothTime = 1f;
    public float smoothStopTime = 0.02f;
    Vector3 nextDirection;


    bool isPossibleZoom = true;
    void RotateScreen()
    {
        // ���콺 Ŭ�� ������ ���� �����ǰ� ���콺 Ŭ�� �� ���� ���� �������� ����
        // ī�޶� ���ư��ٰ� ������ ���� -> SmoothDamp
        if (!target) return;

        if (Input.GetMouseButtonDown(0))
        {
            isPossibleZoom = false ;
            previousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        }

        Vector3 currentSmoothVelocity = new Vector3();
        if (Input.GetMouseButton(0))
        {
            nextDirection = previousPosition - Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //currentSmoothVelocity = smoothVelocity;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isPossibleZoom = true;
            StartCoroutine(SmoothStop());
            //currentSmoothVelocity = smoothVelocity / 2;
        }

        direction = Vector3.SmoothDamp(direction, nextDirection, ref currentSmoothVelocity, rotSmoothTime);
        //transform.position = target.position;

        //transform.RotateAround(target, target.up, )

        //transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
        //transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
        //transform.Translate(new Vector3(0, 0, -distanceFromSphere));

        previousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }

    IEnumerator SmoothStop()
    {
        yield return new WaitForSeconds(smoothStopTime);
        nextDirection = Vector3.zero;
    }
}
