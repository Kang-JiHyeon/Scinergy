using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSW_LineDrawer : MonoBehaviour
{
    // 라인 색
    public Color color = Color.black;

    // brush 번호
    //public int brushNum = 1;

    List<Vector3> linePoints;
    float timer;
    public float timeDelay;
    public GameObject newLine;
    LineRenderer drawLine;

    // 라인 버튼이 눌렸는지 확인
    public bool isDrawing = false;

    // 지우개
    public bool isEraser; // 지우개인지 아닌지 판단하는 변수

    public float linewidth = 0.2f;

    // 생성된 라인을 리스트에 담을 변수
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
        // 라인 버튼이 눌렸다면 드로잉 시작!
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
        // 마우스 왼쪽 버튼을 누르는 순간
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.name == "Board" || hitInfo.collider.name == "uWC Window Object(Clone)")
                {
                    // 라인을 생성한다.
                    newLine = new GameObject("Line" + lineList.Count);

                    // 만약 생성될 때, 리스트에 active가 false인 것들은 삭제
                    for (int i = 0; i < lineList.Count; i++)
                    {
                        if (lineList[i].activeSelf == false)
                        {
                            Destroy(lineList[i].gameObject);
                            lineList.RemoveAt(i); // RemoveAt을 해줘야 되돌리고 다시 선을 그었을때 뻑이 안남.
                            i--;
                        }
                    }

                    //그려지는 라인에 LineRenderer, Material, Color, Width를 설정해준다.
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
        // 마우스 왼쪽 버튼을 누른 상태
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
                        // 만약 생성될 때, 리스트에 active가 false인 것들은 삭제
                        for (int i = 0; i < lineList.Count; i++)
                        {
                            if (lineList[i].activeSelf == false)
                            {
                                Destroy(lineList[i].gameObject);
                                lineList.RemoveAt(i); // RemoveAt을 해줘야 되돌리고 다시 선을 그었을때 뻑이 안남.
                                i--;
                            }
                        }

                        linePoints.Add(GetMousePosition());
                        drawLine.positionCount = linePoints.Count;
                        drawLine.SetPositions(linePoints.ToArray());

                        // 나중에 생긴 선은 위에 올라오게끔
                        sortingOrder++;
                        drawLine.GetComponent<LineRenderer>().sortingOrder = sortingOrder;

                        // 화면 공유된 오브젝트에 글씨를 쓰고 오브젝트를 움직이면 글씨가 그 오브젝트 자식으로 들어가서 같이 움직이게
                        drawLine.transform.parent = hitInfo.transform;
                        timer = timeDelay;
                    }
                }
            }
        }

        // 마무스 왼쪽 버튼을 뗀 상태
        if (Input.GetMouseButtonUp(0))
        {
            // List 요소 모두 제거
            linePoints.Clear();
        }
    }

    Vector3 GetMousePosition()
    {
        // 스크린의 마우스 위치로부터 Ray를 만들어냄
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
        // 마우스 왼쪽 버튼을 누르는 순간
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.name == "Board" || hitInfo.collider.name == "uWC Window Object(Clone)")
                {
                    // 라인을 생성한다.
                    newLine = new GameObject("Line" + lineList.Count);

                    // 만약 생성될 때, 리스트에 active가 false인 것들은 삭제
                    for (int i = 0; i < lineList.Count; i++)
                    {
                        if (lineList[i].activeSelf == false)
                        {
                            Destroy(lineList[i].gameObject);
                            lineList.RemoveAt(i); // RemoveAt을 해줘야 되돌리고 다시 선을 그었을때 뻑이 안남.
                            i--;
                        }
                    }

                    //그려지는 라인에 LineRenderer, Material, Color, Width를 설정해준다.
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
        // 마우스 왼쪽 버튼을 누른 상태
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
                        // 만약 생성될 때, 리스트에 active가 false인 것들은 삭제
                        for (int i = 0; i < lineList.Count; i++)
                        {
                            if (lineList[i].activeSelf == false)
                            {
                                Destroy(lineList[i].gameObject);
                                lineList.RemoveAt(i); // RemoveAt을 해줘야 되돌리고 다시 선을 그었을때 뻑이 안남.
                                i--;
                            }
                        }

                        linePoints.Add(GetMousePosition());
                        drawLine.positionCount = linePoints.Count;
                        drawLine.SetPositions(linePoints.ToArray());

                        // 나중에 생긴 선은 위에 올라오게끔
                        sortingOrder++;
                        drawLine.GetComponent<LineRenderer>().sortingOrder = sortingOrder;

                        // 화면 공유된 오브젝트에 글씨를 쓰고 오브젝트를 움직이면 글씨가 그 오브젝트 자식으로 들어가서 같이 움직이게
                        drawLine.transform.parent = hitInfo.transform;
                        timer = timeDelay;
                    }
                }
            }
        }

        // 마무스 왼쪽 버튼을 뗀 상태
        if (Input.GetMouseButtonUp(0))
        {
            // List 요소 모두 제거
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
