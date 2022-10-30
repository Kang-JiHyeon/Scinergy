using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 마우스 휠로 줌인/줌아웃 (v)
// 2. 마우스 클릭으로 카메라 상하좌우 회전

public class KJH_CameraMove : MonoBehaviour
{
    // 바라볼 대상
    public Transform target;

    public float distance = 10f;

    public float zoomSmoothTime = 0.2f;

    Vector3 velocity = Vector3.zero;

    public float wheelspeed = 10.0f;

    public float minDistance = 2f;
    public float maxDistance = 30f;

    public float camRotSpeed = 0.2f;
    KJH_SelectPlanet selectPlanet;

    // Start is called before the first frame update
    void Start()
    {
        selectPlanet = GetComponent<KJH_SelectPlanet>();
        stopPos = transform.position;
        reverseDistance = new Vector3(0f, 0f, distance);
    }

    // Update is called once per frame
    void Update()
    {
        if (selectPlanet.camaraTarget != null)
        {
            target = selectPlanet.camaraTarget;
        }

        Vector3 targetPos = target.position;

        if (KJH_UIManager.instance.isActiveInfo)
        {
            targetPos += movePos;
        }
        transform.LookAt(targetPos);

        

        if(KJH_UIManager.instance.isActiveInfo && distance > 20f)
        {
            KJH_UIManager.instance.OnClick_CloseInfoMenu();
        }
        //else if(KJH_UIManager.instance.isActiveInfo == false && distance < 10f && target)
        //{
        //    KJH_UIManager.instance.OpenInfoMenu();
        //}


        ZoomInOut();
        //Rotate();
    }

    // 마우스 휠로 줌인/줌아웃
    // - 행성으로 이동 중이거나 마우스 클릭해서 회전 중일 경우는 실행되지 않는다.
    Vector3 reverseDistance;
    Vector3 movePos = Vector3.zero;
    void ZoomInOut()
    {
        // 마우스 휠 입력
        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelspeed;

        if (distance < minDistance) distance = minDistance;
        if (distance > maxDistance) distance = maxDistance;
        reverseDistance = new Vector3(0f, 0f, distance);

        movePos = KJH_UIManager.instance.isActiveInfo ? transform.right * -1.5f : Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + movePos - transform.rotation * reverseDistance, ref velocity, zoomSmoothTime);

    }

    public Vector3 previousPosition;
    public float distanceFromSphere = 50f;
    public Vector3 direction;
    public Vector3 smoothVelocity = Vector3.zero;
    public float rotSmoothTime = 1f;
    public float smoothStopTime = 0.02f;
    Vector3 nextDirection;


    public bool isRotating = false;

    // 마우스 클릭하고 있는 동안 움직임 정도에 따라 카메라 회전
    //void Rotate()
    //{
    //    // 마우스 클릭 시점의 현재 포지션과 마우스 클릭을 땠을 때의 포지션의 차이
    //    // 카메라가 돌아가다가 서서히 멈춤 -> SmoothDamp
    //    if (!target) return;

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        isRotating = true;
    //        previousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

    //    }

    //    Vector3 currentSmoothVelocity = new Vector3();
    //    if (Input.GetMouseButton(0))
    //    {
    //        nextDirection = previousPosition - Camera.main.ScreenToViewportPoint(Input.mousePosition);
    //        //currentSmoothVelocity = smoothVelocity;
    //    }
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        isRotating = false;
    //        StartCoroutine(SmoothStop());
    //        //currentSmoothVelocity = smoothVelocity / 2;
    //    }

    //    direction = Vector3.SmoothDamp(direction, nextDirection, ref currentSmoothVelocity, rotSmoothTime);
    //    transform.position = target.position;

    //    //transform.RotateAround(target, target.up, )

    //    transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
    //    transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
    //    transform.Translate(new Vector3(0, 0, -distanceFromSphere));

    //    previousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    //}

    IEnumerator SmoothStop()
    {
        yield return new WaitForSeconds(smoothStopTime);
        nextDirection = Vector3.zero;
    }

    // 천체로 이동 --> 천체를 클릭한 경우 천체로 이동
    public bool isMovingToCB = false;
    Vector3 stopPos;
    public void MoveToCB()
    {

        distance = minDistance;

        // 카메라가 행성과 일정 거리 안에 있으면 stop
        if (Vector3.Distance(transform.parent.position, target.position) < 5f)
        {
            stopPos = transform.parent.position;

            // 정보 ui 띄우기
            KJH_UIManager.instance.OpenInfoMenu();

            isMovingToCB = false;
        }
        else
        {
            // 카메라 위치를 행성으로 이동
            transform.parent.position = Vector3.Lerp(transform.parent.position, target.position, Time.deltaTime);
        }


        // 각도 회전
        Vector3 dir = target.position - transform.parent.position;
        if (Quaternion.Angle(transform.parent.rotation, Quaternion.LookRotation(dir)) <= 0f)
        {
            transform.parent.forward = dir;
        }
        else
        {
            transform.parent.forward = Vector3.Lerp(transform.parent.forward, dir, camRotSpeed * Time.deltaTime);
        }
    }

    // 카메라 시점 수평 이동
    // 카메라 시점 수평 이동했으면 행성 클릭해도 더이상 움직이지 않아야 한다.
    // cameratarget이 있고, 타겟과 일정거리 안이면 왼쪽으로 일정 거리 이동
    // cameratarget이 없으면 오른쪽으로 일정 거리 이동
    //public bool isActiveInfo = false;
    void MoveHorizontal(Vector3 targetPos)
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            isMovingToCB = false;
            transform.position = target.position;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
        }
    }
}
