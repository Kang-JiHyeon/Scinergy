using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_LineDraw : MonoBehaviour
{
    // brush 번호
    public int brushNum = 1;

    public GameObject drawObj;
    public GameObject drawBoard, drawBoard_parent;
    

    // 선 관련 변수
    public float lineWidth = 0.05f; // Line Renderer Width
    GameObject theTrail;
    Vector3 startPos; 
    Vector3 nextPos;

    // 지우개
    public bool isEraser;
    public GameObject drawObj_temp;
    public Material boardMaterial;

    // 드로잉 색 설정
    public GameObject lineColor;

    // 선이 캔버스 위에서 그려지는지 판단(그려지다가 선이 캔버스 밖으로 그려지는 경우를 방지)
    bool drawOnCanvas;

    public int index = 0;
    int sortingOrder;
    public string drawObjName;

    // 드로잉 라인 List에 저장 (2차원)
    public List<List<GameObject>> lines = new List<List<GameObject>>();

    void Start()
    {
        lineColor = Resources.Load<GameObject>("YS/ColorPicker");
        lineColor.SetActive(false);

        drawObj = Resources.Load<GameObject>("YS/Brush");
        drawObjName = "YS/Brush";
        drawObj.GetComponent<LineRenderer>().useWorldSpace = false;

        // 리스트에 크기 지정
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

        // 드로잉
        Drawing();

        // 지우개
        Eraser();

        // 실행취소
        //CtrlZ();

        // 전 프로세스로 되돌리기
        //CtrlY();

        // 선 전체 삭제
        //AllDelete();
    }

    void Drawing()
    {
        Color eraser = boardMaterial.color;
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // 마우스 왼쪽 버튼을 누르는 순간
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if ((Physics.Raycast(ray, out hitInfo) && hitInfo.collider.name == "Board") || hitInfo.collider.name == "uWC Window Object(Clone)")
            {
                if (hitInfo.transform.name == drawBoard.transform.name)
                {
                    Debug.Log(hitInfo.collider.name);
                    // Canvas에서 그리고 있음
                    drawOnCanvas = true;
                    
                    // 만약 브러쉬일때
                    if(brushNum == 1)
                    {
                        // 드로잉 하기 전 Line width 설정
                        drawObj.GetComponent<LineRenderer>().widthMultiplier = lineWidth;

                    }
                    // 드로잉 하기 전 Line 색 설정
                    drawObj.GetComponent<LineRenderer>().startColor = lineColor.GetComponent<ColorPickerTest>().selectedColor;
                    drawObj.GetComponent<LineRenderer>().endColor = lineColor.GetComponent<ColorPickerTest>().selectedColor;
                    Color color = lineColor.GetComponent<ColorPickerTest>().selectedColor;

                    // 지우개
                    if (isEraser == true)
                    {
                        drawObj.GetComponent<LineRenderer>().startColor = eraser;
                        drawObj.GetComponent<LineRenderer>().endColor = eraser;
                        color = eraser;
                    }

                    // 나중에 그려진 line은 위에 올라와야함
                    sortingOrder++;
                    drawObj.GetComponent<LineRenderer>().sortingOrder = sortingOrder;

                    // line 생성
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

                    // line 생성 후 , 리스트에 넣어줘야함
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
        // 마우스 왼쪽버튼을 누른 상태
        else if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if ((Physics.Raycast(ray, out hitInfo) && hitInfo.collider.name == "Board") || hitInfo.collider.name == "uWC Window Object(Clone)")
            {
                if (hitInfo.transform.name == drawBoard.transform.name)
                {
                    // 드로잉 하다가 캔버스를 나갔다가 다시 캔버스로 들어오면, 들어온 그 자리부터 다시 line 생성
                    if (drawOnCanvas == false)
                    {
                        // line 생성
                        theTrail = Instantiate(drawObj, Vector3.zero, Quaternion.identity);
                        theTrail.transform.SetParent(drawBoard_parent.transform, false);
                        theTrail.transform.localEulerAngles = -theTrail.transform.parent.localEulerAngles;

                        Color color = lineColor.GetComponent<ColorPickerTest>().selectedColor;

                        // 지우개
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
                        // line 생성 후 , 리스트에 넣어줘야함
                        lines[index].Add(theTrail);

                        if (Physics.Raycast(ray, out hitInfo))
                        {
                            startPos = hitInfo.point - theTrail.transform.parent.position;

                            theTrail.GetComponent<LineRenderer>().SetPosition(0, startPos);
                            theTrail.GetComponent<LineRenderer>().SetPosition(1, startPos);
                        }
                    }
                    // Canvas에서 그리고 있음
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

                // 지우개를 사용하기 전으로
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
