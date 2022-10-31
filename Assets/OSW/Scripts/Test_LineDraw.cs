using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_LineDraw : MonoBehaviour
{
    // brush ��ȣ
    public int brushNum = 1;

    public GameObject drawObj;
    public GameObject drawBoard, drawBoard_parent;
    

    // �� ���� ����
    public float lineWidth = 0.05f; // Line Renderer Width
    GameObject theTrail;
    Vector3 startPos; 
    Vector3 nextPos;

    // ���찳
    public bool isEraser;
    public GameObject drawObj_temp;
    public Material boardMaterial;

    // ����� �� ����
    public GameObject lineColor;

    // ���� ĵ���� ������ �׷������� �Ǵ�(�׷����ٰ� ���� ĵ���� ������ �׷����� ��츦 ����)
    bool drawOnCanvas;

    public int index = 0;
    int sortingOrder;
    public string drawObjName;

    // ����� ���� List�� ���� (2����)
    public List<List<GameObject>> lines = new List<List<GameObject>>();

    void Start()
    {
        lineColor = Resources.Load<GameObject>("YS/ColorPicker");
        lineColor.SetActive(false);

        drawObj = Resources.Load<GameObject>("YS/Brush");
        drawObjName = "YS/Brush";
        drawObj.GetComponent<LineRenderer>().useWorldSpace = false;

        // ����Ʈ�� ũ�� ����
        for(int i = 0; i < 8; i++)
        {
            lines.Add(new List<GameObject>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            brushNum = 1;
            drawObj = Resources.Load<GameObject>("YS/Brush");
            drawObjName = "YS/Brush";
        }

        // �����
        Drawing();

        // ���찳
        Eraser();

        // �������
        //CtrlZ();

        // �� ���μ����� �ǵ�����
        //CtrlY();

        // �� ��ü ����
        //AllDelete();
    }

    void Drawing()
    {
        Color eraser = boardMaterial.color;
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // ���콺 ���� ��ư�� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if ((Physics.Raycast(ray, out hitInfo) && hitInfo.collider.name == "Board") || hitInfo.collider.name == "uWC Window Object(Clone)")
            {
                if (hitInfo.transform.name == drawBoard.transform.name)
                {
                    Debug.Log(hitInfo.collider.name);
                    // Canvas���� �׸��� ����
                    drawOnCanvas = true;
                    
                    // ���� �귯���϶�
                    if(brushNum == 1)
                    {
                        // ����� �ϱ� �� Line width ����
                        drawObj.GetComponent<LineRenderer>().widthMultiplier = lineWidth;

                    }
                    // ����� �ϱ� �� Line �� ����
                    drawObj.GetComponent<LineRenderer>().startColor = lineColor.GetComponent<ColorPickerTest>().selectedColor;
                    drawObj.GetComponent<LineRenderer>().endColor = lineColor.GetComponent<ColorPickerTest>().selectedColor;
                    Color color = lineColor.GetComponent<ColorPickerTest>().selectedColor;

                    // ���찳
                    if (isEraser == true)
                    {
                        drawObj.GetComponent<LineRenderer>().startColor = eraser;
                        drawObj.GetComponent<LineRenderer>().endColor = eraser;
                        color = eraser;
                    }

                    // ���߿� �׷��� line�� ���� �ö�;���
                    sortingOrder++;
                    drawObj.GetComponent<LineRenderer>().sortingOrder = sortingOrder;

                    // line ����
                    theTrail = Instantiate(drawObj, Vector3.zero, Quaternion.identity);
                    theTrail.transform.SetParent(drawBoard_parent.transform, false);
                    theTrail.transform.localEulerAngles = -theTrail.transform.parent.localEulerAngles;

                    for (int i = 0; i < lines[index].Count; i++)
                    {
                        if (lines[index][i].activeSelf == false)
                        {
                            Destroy(lines[index][i].gameObject);
                            lines[index].RemoveAt(i);
                            i--;
                        }
                    }

                    // line ���� �� , ����Ʈ�� �־������
                    lines[index].Add(theTrail);
                    
                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        //startPos = hitInfo.point - theTrail.transform.parent.position;
                        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        theTrail.GetComponent<LineRenderer>().SetPosition(0, startPos);
                        theTrail.GetComponent<LineRenderer>().SetPosition(1, startPos);
                    }
                }
                else
                {
                    drawOnCanvas = false;
                }
            }
        }
        // ���콺 ���ʹ�ư�� ���� ����
        else if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if ((Physics.Raycast(ray, out hitInfo) && hitInfo.collider.name == "Board") || hitInfo.collider.name == "uWC Window Object(Clone)")
            {
                if (hitInfo.transform.name == drawBoard.transform.name)
                {
                    // ����� �ϴٰ� ĵ������ �����ٰ� �ٽ� ĵ������ ������, ���� �� �ڸ����� �ٽ� line ����
                    if (drawOnCanvas == false)
                    {
                        // line ����
                        theTrail = Instantiate(drawObj, Vector3.zero, Quaternion.identity);
                        theTrail.transform.SetParent(drawBoard_parent.transform, false);
                        theTrail.transform.localEulerAngles = -theTrail.transform.parent.localEulerAngles;

                        Color color = lineColor.GetComponent<ColorPickerTest>().selectedColor;

                        // ���찳
                        if (isEraser == true)
                        {
                            drawObj.GetComponent<LineRenderer>().startColor = eraser;
                            drawObj.GetComponent<LineRenderer>().endColor = eraser;
                            color = eraser;
                        }

                        for (int i = 0; i < lines[index].Count; i++)
                        {
                            if (lines[index][i].activeSelf == false)
                            {
                                Destroy(lines[index][i].gameObject);
                                lines[index].RemoveAt(i);
                                i--;
                            }
                        }
                        // line ���� �� , ����Ʈ�� �־������
                        lines[index].Add(theTrail);

                        if (Physics.Raycast(ray, out hitInfo))
                        {
                            startPos = hitInfo.point - theTrail.transform.parent.position;

                            theTrail.GetComponent<LineRenderer>().SetPosition(0, startPos);
                            theTrail.GetComponent<LineRenderer>().SetPosition(1, startPos);
                        }
                    }
                    // Canvas���� �׸��� ����
                    drawOnCanvas = true;

                    if(brushNum == 1)
                    {
                        nextPos = hitInfo.point - theTrail.transform.parent.position;
                        theTrail.GetComponent<LineRenderer>().positionCount++;
                        int positionIndex = theTrail.GetComponent<LineRenderer>().positionCount - 1;
                        theTrail.GetComponent<LineRenderer>().SetPosition(positionIndex, nextPos);
                    }

                }
                else
                {
                    drawOnCanvas = false;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {

        }
    }

    void Eraser()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(isEraser == false)
            {
                isEraser = true;
                drawObj_temp = drawObj;
                drawObj = Resources.Load<GameObject>("YS/Eraser");
                drawObjName = "YS/Eraser";
            }
            else if(isEraser == true)
            {
                isEraser = false;

                // ���찳�� ����ϱ� ������
                drawObj = drawObj_temp;
            }
        }
    }

    void CtrlZ()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            for (int i = lines[index].Count - 1; i >= 0; i--)
            {
                if (lines[index][i].activeSelf == true)
                {
                    lines[index][i].SetActive(false);
                    break;
                }
            }
        }
    }

    void CtrlY()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            for(int i = 0; i < lines[index].Count; i++)
            {
                if(lines[index][i].activeSelf == false)
                {
                    lines[index][i].SetActive(true);
                    break;
                }
            }
        }
    }

    void AllDelete()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            for(int i = 0; i < lines[index].Count; i++)
            {
                Destroy(lines[index][i].gameObject);
            }
            lines[index].Clear();
        }
    }
}
