using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSW_LineDrawer : MonoBehaviour
{
    // ���� ��
    public Color color = Color.black;

    // brush ��ȣ
    //public int brushNum = 1;

    List<Vector3> linePoints;
    float timer;
    public float timeDelay;
    public GameObject newLine;
    LineRenderer drawLine;

    // ���� ��ư�� ���ȴ��� Ȯ��
    public bool isDrawing = false;

    // ���찳
    public bool isEraser; // ���찳���� �ƴ��� �Ǵ��ϴ� ����

    public float linewidth = 0.2f;

    // ������ ������ ����Ʈ�� ���� ����
    public List<GameObject> lineList;

    int index = 0;
    int sortingOrder;
    void Start()
    {
        linePoints = new List<Vector3>();
        timer = timeDelay;
    }

    void Update()
    {
        // ���� ��ư�� ���ȴٸ� ����� ����!
        if(isDrawing == true)
        {
            Drawing();
        }

        if(isEraser == true)
        {
            Eraser();
        }

        //CtrlZ();
        //CtrlY();
        //AllDelete();
    }

    public void Drawing()
    {
        // ���콺 ���� ��ư�� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.name == "Board" || hitInfo.collider.name == "uWC Window Object(Clone)")
                {
                    // ������ �����Ѵ�.
                    newLine = new GameObject("Line" + lineList.Count);

                    // ���� ������ ��, ����Ʈ�� active�� false�� �͵��� ����
                    for (int i = 0; i < lineList.Count; i++)
                    {
                        if (lineList[i].activeSelf == false)
                        {
                            Destroy(lineList[i].gameObject);
                            lineList.RemoveAt(i); // RemoveAt�� ����� �ǵ����� �ٽ� ���� �׾����� ���� �ȳ�.
                            i--;
                        }
                    }

                    //�׷����� ���ο� LineRenderer, Material, Color, Width�� �������ش�.
                    drawLine = newLine.AddComponent<LineRenderer>();
                    //drawLine.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                    drawLine.material = Resources.Load<Material>("Color");
                    drawLine.material.color = color;
                    drawLine.startWidth = linewidth;
                    drawLine.endWidth = linewidth;
                    lineList.Add(newLine);
                }
            }

        }
        // ���콺 ���� ��ư�� ���� ����
        if (Input.GetMouseButton(0))
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.collider.name == "Board" || hitInfo.collider.name == "uWC Window Object(Clone)")
                    {
                        // ���� ������ ��, ����Ʈ�� active�� false�� �͵��� ����
                        for (int i = 0; i < lineList.Count; i++)
                        {
                            if (lineList[i].activeSelf == false)
                            {
                                Destroy(lineList[i].gameObject);
                                lineList.RemoveAt(i); // RemoveAt�� ����� �ǵ����� �ٽ� ���� �׾����� ���� �ȳ�.
                                i--;
                            }
                        }

                        linePoints.Add(GetMousePosition());
                        drawLine.positionCount = linePoints.Count;
                        drawLine.SetPositions(linePoints.ToArray());

                        // ���߿� ���� ���� ���� �ö���Բ�
                        sortingOrder++;
                        drawLine.GetComponent<LineRenderer>().sortingOrder = sortingOrder;

                        // ȭ�� ������ ������Ʈ�� �۾��� ���� ������Ʈ�� �����̸� �۾��� �� ������Ʈ �ڽ����� ���� ���� �����̰�
                        drawLine.transform.parent = hitInfo.transform;
                        timer = timeDelay;
                    }
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
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.name == "Board" || hitInfo.collider.name == "uWC Window Object(Clone)")
            {
                //Debug.Log(hitInfo.collider.name);
            }
        }
        return hitInfo.point;
    }

    void Eraser()
    {
        // ���콺 ���� ��ư�� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.name == "Board" || hitInfo.collider.name == "uWC Window Object(Clone)")
                {
                    // ������ �����Ѵ�.
                    newLine = new GameObject("Line" + lineList.Count);

                    // ���� ������ ��, ����Ʈ�� active�� false�� �͵��� ����
                    for (int i = 0; i < lineList.Count; i++)
                    {
                        if (lineList[i].activeSelf == false)
                        {
                            Destroy(lineList[i].gameObject);
                            lineList.RemoveAt(i); // RemoveAt�� ����� �ǵ����� �ٽ� ���� �׾����� ���� �ȳ�.
                            i--;
                        }
                    }

                    //�׷����� ���ο� LineRenderer, Material, Color, Width�� �������ش�.
                    drawLine = newLine.AddComponent<LineRenderer>();
                    //drawLine.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                    drawLine.material = Resources.Load<Material>("Color");
                    drawLine.material.color = color;
                    drawLine.startWidth = linewidth;
                    drawLine.endWidth = linewidth;
                    lineList.Add(newLine);
                }
            }

        }
        // ���콺 ���� ��ư�� ���� ����
        if (Input.GetMouseButton(0))
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.collider.name == "Board" || hitInfo.collider.name == "uWC Window Object(Clone)")
                    {
                        // ���� ������ ��, ����Ʈ�� active�� false�� �͵��� ����
                        for (int i = 0; i < lineList.Count; i++)
                        {
                            if (lineList[i].activeSelf == false)
                            {
                                Destroy(lineList[i].gameObject);
                                lineList.RemoveAt(i); // RemoveAt�� ����� �ǵ����� �ٽ� ���� �׾����� ���� �ȳ�.
                                i--;
                            }
                        }

                        linePoints.Add(GetMousePosition());
                        drawLine.positionCount = linePoints.Count;
                        drawLine.SetPositions(linePoints.ToArray());

                        // ���߿� ���� ���� ���� �ö���Բ�
                        sortingOrder++;
                        drawLine.GetComponent<LineRenderer>().sortingOrder = sortingOrder;

                        // ȭ�� ������ ������Ʈ�� �۾��� ���� ������Ʈ�� �����̸� �۾��� �� ������Ʈ �ڽ����� ���� ���� �����̰�
                        drawLine.transform.parent = hitInfo.transform;
                        timer = timeDelay;
                    }
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

    public void CtrlZ()
    {
        for (int i = lineList.Count - 1; i >= 0; i--)
        {
            if (lineList[i].activeSelf == true)
            {
                lineList[i].SetActive(false);
                break;
            }
        }
    }

    public void CtrlY()
    {
        for(int i = 0; i < lineList.Count; i++)
        {
            if (lineList[i].activeSelf == false)
            {
                lineList[i].SetActive(true);
                break;
            }
        }
    }

    public void AllDelete()
    {
        for (int i = 0; i < lineList.Count; i++)
        {
            Destroy(lineList[i].gameObject);
        }
        lineList.Clear();
    }
}
