using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KJH_MoonRotate : MonoBehaviour
{
    LineRenderer lr;
    KJH_EclipsDrag drag;
    public Transform earth;
    public float speed = 1f;
    public float radius = 5f;
    public int segment = 20;
    public bool isStop = false;


    // Start is called before the first frame update
    void Start()
    {
        lr = transform.GetComponent<LineRenderer>();
        radius = Vector3.Distance(transform.position, earth.position);
        drag = GetComponent<KJH_EclipsDrag>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isStop = !isStop;
        }

        if(isStop == false && drag.isStop == false)
        {
            transform.RotateAround(earth.position, -earth.up, speed * Time.deltaTime);
        }

        DrawOrbit();

        transform.LookAt(earth);
    }

    void DrawOrbit()
    {
        Vector3[] points = new Vector3[segment + 1];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = earth.position + Evaluate((float)i / (float)segment);
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
