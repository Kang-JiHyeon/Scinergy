using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSW_LineDrawer : MonoBehaviour
{
    List<Vector3> linePoints;
    float timer;
    public float timeDelay;
    GameObject newLine;
    LineRenderer drawLine;
    public float linewidth;

    // Start is called before the first frame update
    void Start()
    {
        linePoints = new List<Vector3>();
        timer = timeDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 왼쪽 버튼을 누르는 순간
        if (Input.GetMouseButtonDown(0))
        {
            // 라인을 생성한다.
            newLine = new GameObject("Line");

            // 그려지는 라인에 LineRenderer, Material, Color, Width를 설정해준다.
            drawLine = newLine.AddComponent<LineRenderer>();
            drawLine.material = new Material(Shader.Find("Sprites/Default"));
            drawLine.startColor = RandomColor();
            drawLine.endColor = RandomColor();
            drawLine.startWidth = linewidth;
            drawLine.endWidth = linewidth;
        }

        // 마우스 왼쪽 버튼을 누른 상태
        if (Input.GetMouseButton(0))
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                linePoints.Add(GetMousePosition());
                drawLine.positionCount = linePoints.Count;
                drawLine.SetPositions(linePoints.ToArray());

                timer = timeDelay;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            linePoints.Clear();
        }
    }

    Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return ray.origin + ray.direction * 10;
    }

    Color RandomColor()
    {
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
