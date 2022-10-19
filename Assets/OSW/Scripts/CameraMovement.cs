using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // �� �� Ÿ��
    public float zoomFigure = 10.0f; // �� ��ġ
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
        // �� ��/�ƿ�
        Vector3 targetDis = tr.position - target.position;
        targetDis = Vector3.Normalize(targetDis);
        tr.position -= (targetDis * Input.GetAxis("Mouse ScrollWheel") * zoomFigure);

        // Ÿ���� �������� ī�޶� Rotate
        if (Input.GetMouseButton(0))
        {
            xRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;
            yRotateMove = Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;

            Vector3 trPosition = target.transform.position;

            transform.RotateAround(trPosition, Vector3.right, -yRotateMove); // ����
            transform.RotateAround(trPosition, Vector3.up, xRotateMove); // �¿�
            // ���콺 ������ �������� ��� �����ؼ� ���� ���� �� ���� ���콺 ������ �����ǰ� ������ �������� ��
            // �I�� �� 1�ʵ��� �� �������� ��� �����ǰ�
        }
    }    
}