using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // 줌 할 타겟
    public float zoomFigure = 10.0f; // 줌 수치
    public float rotateSpeed = 500.0f; 

    private float xRotateMove, yRotateMove;
    private Transform tr;
    private Rigidbody rb;

    private void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }
    

    private void Update()
    {
        // 줌 인/아웃
        Vector3 targetDis = tr.position - target.position;
        targetDis = Vector3.Normalize(targetDis);
        tr.position -= (targetDis * Input.GetAxis("Mouse ScrollWheel") * zoomFigure);

        // 타겟을 기준으로 카메라 Rotate
        if (Input.GetMouseButton(0))
        {
            xRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;
            yRotateMove = Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;

            Vector3 trPosition = target.transform.position;

            transform.RotateAround(trPosition, Vector3.right, -yRotateMove); // 상하
            transform.RotateAround(trPosition, Vector3.up, xRotateMove); // 좌우
            // 마우스 포인터 포지션을 계속 저장해서 손을 뗏을 때 현재 마우스 포인터 포지션과 저장한 포지션을 빼
            // 똈을 때 1초동안 그 방향으로 계속 유지되게
        }
    }    
}