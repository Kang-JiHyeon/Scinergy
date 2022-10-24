using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


public class OrbitCamera : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    public Vector3 previousPosition; 
    public float distanceFromSphere = 50f; 
    public Vector3 direction;
    public Vector3 smoothVelocity = Vector3.zero;
    public float smoothTime = 1.5f;
    public float smoothStopTime = 0.05f;
    Vector3 nextDirection;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        // 마우스 클릭 시점의 현재 포지션과 마우스 클릭 을 땠을 때의 포지션의 차이
        // 카메라가 돌아가다가 서서히 멈춤 -> SmoothDamp
        if (!target) return;

        if (Input.GetMouseButtonDown(0))
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);

        Vector3 currentSmoothVelocity = new Vector3();
        if (Input.GetMouseButton(0))
        {
            nextDirection = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);
            currentSmoothVelocity = smoothVelocity;
        }
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(SmoothStop());
            currentSmoothVelocity = smoothVelocity / 2;
        }

        direction = Vector3.SmoothDamp(direction, nextDirection, ref currentSmoothVelocity, smoothTime, 50, 10f * Time.deltaTime);
        cam.transform.position = target.position;
        cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
        cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
        cam.transform.Translate(new Vector3(0, 0, -distanceFromSphere));

        previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);

    }

    IEnumerator SmoothStop()
    {
        yield return new WaitForSeconds(smoothStopTime);
        nextDirection = Vector3.zero;
    }
}