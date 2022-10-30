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

    //����
    public List<GameObject> LineList;

    void Start()
    {
        linePoints = new List<Vector3>();
        timer = timeDelay;
    }

    void Update()
    {
        // ���콺 ���� ��ư�� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if ((Physics.Raycast(ray, out hitInfo) && hitInfo.collider.name == "Board") || hitInfo.collider.name == "uWC Window Object(Clone)")
            {
                //Debug.Log(hitInfo.collider.name);

                // ������ �����Ѵ�.
                newLine = new GameObject("Line"+ LineList.Count);//���� + LineList.Count

                // �׷����� ���ο� LineRenderer, Material, Color, Width�� �������ش�.
                drawLine = newLine.AddComponent<LineRenderer>();
                drawLine.material = new Material(Shader.Find("Sprites/Default"));
                drawLine.startColor = RandomColor();
                drawLine.endColor = RandomColor();
                drawLine.startWidth = linewidth;
                drawLine.endWidth = linewidth;
                LineList.Add(newLine);//����
            }
                
        }
        //����
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Destroy(LineList[LineList.Count - 1]);
            LineList.RemoveAt(LineList.Count - 1);
        }
        //

        // ���콺 ���� ��ư�� ���� ����
        if (Input.GetMouseButton(0))
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if ((Physics.Raycast(ray, out hitInfo) && hitInfo.collider.name == "Board") || hitInfo.collider.name == "uWC Window Object(Clone)")
                {
                    //Debug.Log(hitInfo.collider.name);
                    linePoints.Add(GetMousePosition());
                    drawLine.positionCount = linePoints.Count;
                    drawLine.SetPositions(linePoints.ToArray());

                    // ȭ�� ������ ������Ʈ�� �۾��� ���� ������Ʈ�� �����̸� �۾��� �� ������Ʈ �ڽ����� ���� ���� �����̰�
                    drawLine.transform.parent = hitInfo.transform;
                    timer = timeDelay;
                }
            }
        }

        // ������ ���� ��ư�� �� ����
        if (Input.GetMouseButtonUp(0))
        {
            // List ��� ��� ����
            linePoints.Clear();
        }
    }

    Vector3 GetMousePosition()
    {
        // ��ũ���� ���콺 ��ġ�κ��� Ray�� ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if ((Physics.Raycast(ray, out hitInfo) && hitInfo.collider.name == "Board") || hitInfo.collider.name == "uWC Window Object(Clone)")
        {
            //Debug.Log(hitInfo.collider.name);
        }

        return hitInfo.point;
    }

    Color RandomColor()
    {
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
