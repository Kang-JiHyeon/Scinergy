using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �༺�� �˵��� �׸��� �ʹ�.
// - Line Renderer, r, segments
// - r : �¾���� �Ÿ�, segments : ������ �׸� ���� ����

// �������� �߽����� ȸ���ϰ� �ʹ�.

public class KJH_Revolution : MonoBehaviour
{
    LineRenderer lr;
    public Transform orbitAxis; // ������
    public Transform planet;    // �༺
    public Transform sunLight;  // �¾��
    public int segment;         // ���� ������ ����
    public float radius;        // ���������� �Ÿ�(������)
    public float period;        // �����ֱ�
    public float rotSpeed;      // ȸ���ӵ�


    // Start is called before the first frame update
    void Start()
    {
        //lr = transform.GetComponent<LineRenderer>();
        lr = transform.GetChild(0).GetComponent<LineRenderer>();
        radius = Vector3.Distance(orbitAxis.position, planet.position);
        sunLight.LookAt(transform.position);

        //DrawOrbit();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(orbitAxis.position, -planet.up, period * rotSpeed);

    }


    void DrawOrbit()
    {
        Vector3[] points = new Vector3[segment+1];
        //float angle = 360f / segment;

        for(int i=0; i<points.Length; i++)
        {
            points[i] = orbitAxis.position + Evaluate((float)i / (float)segment);
        }

        points[segment] = points[0];
        lr.positionCount = segment + 1;
        lr.SetPositions(points);
    }

    public Vector3 Evaluate(float t)
    {
        float angle = Mathf.Deg2Rad * 360f * t;
        float x = Mathf.Sin(angle) * radius;
        float z = Mathf.Cos(angle) * radius;
        return new Vector3(x, 0, z);
    }
}
