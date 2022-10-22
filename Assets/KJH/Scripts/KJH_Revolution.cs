using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 행성의 궤도를 그리고 싶다.
// - Line Renderer, r, segments
// - r : 태양과의 거리, segments : 라인을 그릴 점의 개수

// 기준점을 중심으로 회전하고 싶다.

public class KJH_Revolution : MonoBehaviour
{
    LineRenderer lr;
    public Transform orbitAxis; // 기준점
    public Transform planet;    // 행성
    public Transform sunLight;  // 태양빛
    public int segment;         // 라인 꼭짓점 개수
    public float radius;        // 기준점과의 거리(반지름)
    public float period;        // 공전주기
    public float rotSpeed;      // 회전속도


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
