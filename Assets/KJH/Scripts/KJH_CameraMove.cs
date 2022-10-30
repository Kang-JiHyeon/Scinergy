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

    // ���콺 �ٷ� ����/�ܾƿ�
    // - �༺���� �̵� ���̰ų� ���콺 Ŭ���ؼ� ȸ�� ���� ���� ������� �ʴ´�.
    Vector3 reverseDistance;
    Vector3 movePos = Vector3.zero;
    void ZoomInOut()
    {
        // ���콺 �� �Է�
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

    // ���콺 Ŭ���ϰ� �ִ� ���� ������ ������ ���� ī�޶� ȸ��
    //void Rotate()
    //{
    //    // ���콺 Ŭ�� ������ ���� �����ǰ� ���콺 Ŭ���� ���� ���� �������� ����
    //    // ī�޶� ���ư��ٰ� ������ ���� -> SmoothDamp
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

    // õü�� �̵� --> õü�� Ŭ���� ��� õü�� �̵�
    public bool isMovingToCB = false;
    Vector3 stopPos;
    public void MoveToCB()
    {

        distance = minDistance;

        // ī�޶� �༺�� ���� �Ÿ� �ȿ� ������ stop
        if (Vector3.Distance(transform.parent.position, target.position) < 5f)
        {
            stopPos = transform.parent.position;

            // ���� ui ����
            KJH_UIManager.instance.OpenInfoMenu();

            isMovingToCB = false;
        }
        else
        {
            // ī�޶� ��ġ�� �༺���� �̵�
            transform.parent.position = Vector3.Lerp(transform.parent.position, target.position, Time.deltaTime);
        }


        // ���� ȸ��
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

    // ī�޶� ���� ���� �̵�
    // ī�޶� ���� ���� �̵������� �༺ Ŭ���ص� ���̻� �������� �ʾƾ� �Ѵ�.
    // cameratarget�� �ְ�, Ÿ�ٰ� �����Ÿ� ���̸� �������� ���� �Ÿ� �̵�
    // cameratarget�� ������ ���������� ���� �Ÿ� �̵�
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
