using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KJH_RotateAround : MonoBehaviour
{
    LineRenderer lr;
    public Transform earth;
    public float speed = 1f;
    public float radius = 5f;
    public int segment = 20;
    

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        radius = Vector3.Distance(transform.position, earth.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(earth.position, -earth.up, speed * Time.deltaTime);
        DrawOrbit();
    }

    void DrawOrbit()
    {
        Vector3[] points = new Vector3[segment + 1];
        //float angle = 360f / segment;

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
