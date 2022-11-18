using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 라이트 회전
// - 라이트, sun 위치
// 궤도 그리기
// - line renderer
public class KJH_Moon : MonoBehaviour
{
    public KJH_SolarSystem solarSystem;
    public Transform sun;
    public Transform light;
    Transform orbit;
    LineRenderer lr;
    int segment = 0;
    float radius = 0;
    Vector3[] points;

    // Start is called before the first frame update
    void Start()
    {
        orbit = transform.GetChild(0);
        lr = orbit.GetComponent<LineRenderer>();
        points = new Vector3[solarSystem.segment + 1];
        segment = solarSystem.segment;

        radius = Vector3.Distance(orbit.position, transform.position);

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = orbit.position + solarSystem.CalculatePosition((float)i / (float)segment, radius);
        }

        points[segment] = points[0];
        lr.positionCount = segment + 1;
        lr.SetPositions(points);

        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
    }
    Vector3 preDir;
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = orbit.position + solarSystem.CalculatePosition((float)i / (float)segment, radius);
        }

        points[segment] = points[0];
        lr.positionCount = segment + 1;
        lr.SetPositions(points);
    }
}
