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

        // �׻� Ÿ�� �ٶ󺸵���
        

        //float wheel = Input.GetAxis("Mouse ScrollWheel");
        //if(wheel < 0 || wheel > 0)
        //{

        //distance -= wheel * wheelspeed;

        //if (isRotating == false && isMovingToCB == false)
        //{
            
        //}
        //}
        //}
        //RotateScreen();
        if(isActiveInfo == false)
        {
            transform.LookAt(target);
        }
        ZoomInOut();
    }

    // ���콺 �ٷ� ����/�ܾƿ�
    // - �༺���� �̵� ���̰ų� ���콺 Ŭ���ؼ� ȸ�� ���� ���� ������� �ʴ´�.
    Vector3 reverseDistance;
    void ZoomInOut()
    {
        // ���콺 �� �Է�
        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelspeed;

        if (distance < minDistance) distance = minDistance;
        if (distance > maxDistance) distance = maxDistance;



        reverseDistance = new Vector3(0f, 0f, distance);

        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position - transform.rotation * reverseDistance, ref velocity, zoomSmoothTime);




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
    void Rotate()
    {
        // ���콺 Ŭ�� ������ ���� �����ǰ� ���콺 Ŭ���� ���� ���� �������� ����
        // ī�޶� ���ư��ٰ� ������ ���� -> SmoothDamp
        if (!target) return;

        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
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
            isRotating = false;
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

    // õü�� �̵� --> õü�� Ŭ���� ��� õü�� �̵�
    public bool isMovingToCB = false;
    Vector3 stopPos;
    public void MoveToCB()
    {
        distance = minDistance;
        // ī�޶� ��ġ�� �༺���� �̵�
        //if (Vector3.Distance(transform.position, target.position) >= 5f)
        //{
        //    transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
        //    stopPos = transform.position;
        //}

        // ī�޶� �༺�� ���� �Ÿ� �ȿ� ������ stop
        if (Vector3.Distance(transform.position, target.position) < 5f)
        {
            stopPos = transform.position;

            // ���� ui ����
            KJH_UIManager.instance.MoveDefalutUI(-1f);
            KJH_UIManager.instance.MoveCBInfoMenu(1f);

            //Vector3 movePos = transform.position + transform.right * -2f;
            //movePos.y = transform.position.y;
            //iTween.MoveTo(gameObject, iTween.Hash("position", movePos));

            isActiveInfo = true;    // ui ����
            isMovingToCB = false;
        }
        else
        {
            // ī�޶� ��ġ�� �༺���� �̵�
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
        }



        //// ī�޶� ��ġ�� �༺�� ���� �Ÿ� �ȿ� ������
        //else
        //{
        //    transform.position = stopPos;
        //    isActiveInfo = true;

        //    KJH_UIManager.instance.MoveDefalutUI(-1f);
        //    KJH_UIManager.instance.MoveCBInfoMenu(1f);

        //    //iTween.MoveTo(gameObject, iTween.Hash("x", 5f, "time", 2f));

        //    // ī�޶� �������� �̵�
        //    //cam.targetPos =  transform.right * -1.5f;
        //    ////cam.moveDir = -1f;
        //    //cam.isMove = true;
        //    //transform.LookAt(clickTarget);



        //    isMovingToCB = false;
        //}

        // ���� ȸ��
        Vector3 dir = target.position - transform.position;
        if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(dir)) <= 0f)
        {
            transform.forward = dir;
        }
        else
        {
            transform.forward = Vector3.Lerp(transform.forward, dir, camRotSpeed * Time.deltaTime);
        }
    }

    // ī�޶� ���� ���� �̵�
    // ī�޶� ���� ���� �̵������� �༺ Ŭ���ص� ���̻� �������� �ʾƾ� �Ѵ�.
    // cameratarget�� �ְ�, Ÿ�ٰ� �����Ÿ� ���̸� �������� ���� �Ÿ� �̵�
    // cameratarget�� ������ ���������� ���� �Ÿ� �̵�
    bool isActiveInfo = false;
    void MoveHorizontal(Vector3 targetPos)
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            //isActiveInfo = false;
            transform.position = target.position;
        }
        else
        {
            // ���� ��ġ���� x�� �������� ���� �Ÿ���ŭ �̵�
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
        }
    }
}
