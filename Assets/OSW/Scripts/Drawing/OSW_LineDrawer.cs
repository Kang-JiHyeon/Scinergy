using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSW_LineDrawer : MonoBehaviourPun
{
    // 라인 색
    public Color color = Color.black;

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

    int sortingOrder;

    // 1127 PlayerMove 스크립트 받기
    PlayerMove playerMove;

    // 플레이어를 받아
    PhotonView pv = null;

    Camera cam;

    public static OSW_LineDrawer Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
    }

    void Update()
    {
       // 1127 playerMove 받기
        if(playerMove == null)
        {
            playerMove = FindObjectOfType<PlayerMove>();
        }

        if (pv == null)
        {
            pv = SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName];
            if (pv)
            {
                linePoints = new List<Vector3>();
                timer = timeDelay;
            }
        }

        if (SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].IsMine)
        {
            // 라인 버튼이 눌렸다면 드로잉 시작!
            if (SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].IsMine)
            {
                if (isDrawing == true)
                {
                    Drawing(SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName]);
                }
            }

            if (isEraser == true)
            {
                Eraser();
            }
        }
    }

    
    
    public void Drawing(PhotonView pv)
    {

        // 마우스 왼쪽 버튼을 누르는 순간
        if (Input.GetMouseButtonDown(0))
        {
            if (playerMove.fullScreenMode)
            {
                cam = GameObject.Find("TV").transform.GetChild(0).GetComponent<Camera>();
            }
            else
            {
                cam = Camera.main;
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            //if (Physics.Raycast(fullRay, out hitInfo))
            if (Physics.Raycast(ray, out hitInfo))
            {
                print(hitInfo.collider.name);
                if (hitInfo.collider.name == "TV" || hitInfo.collider.name == "uWC Window Object(Clone)")
                {
                    // 라인을 생성한다.
                    newLine = new GameObject("Line" + lineList.Count);

                    // 만약 생성될 때, 리스트에 active가 false인 것들은 삭제
                    for (int i = 0; i < lineList.Count; i++)
                    {
                        if (lineList[i] == null) continue;
                        if (lineList[i].activeSelf == false)
                        {
                            Destroy(lineList[i].gameObject);
                            lineList.RemoveAt(i); // RemoveAt을 해줘야 되돌리고 다시 선을 그었을때 뻑이 안남.
                            i--;
                        }
                    }

                    //그려지는 라인에 LineRenderer, Material, Color, Width를 설정해준다.
                    drawLine = newLine.AddComponent<LineRenderer>();

                    // 내가 그리는 라인은 startColor, endColor로 색 지정이 불가
                    // 그래서 Material의 색을 변경해보니 색이 바뀌어진다. 
                    drawLine.material = Resources.Load<Material>("Color");
                    drawLine.material.color = color;

                    drawLine.startWidth = linewidth;
                    drawLine.endWidth = linewidth;
                }
            }
        }

        // 마우스 왼쪽 버튼을 누른 상태
        if (Input.GetMouseButton(0))
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                //if (Physics.Raycast(fullRay, out hitInfo))
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.collider.name == "TV" || hitInfo.collider.name == "uWC Window Object(Clone)")
                    {
                        // 만약 생성될 때, 리스트에 active가 false인 것들은 삭제
                        for (int i = 0; i < lineList.Count; i++)
                        {
                            if (lineList[i] == null) continue;
                            if (lineList[i].activeSelf == false)
                            {
                                
                                Destroy(lineList[i].gameObject);
                                lineList.RemoveAt(i); // RemoveAt을 해줘야 되돌리고 다시 선을 그었을때 뻑이 안남.
                                i--;
                            }
                        }

                        linePoints.Add(GetMousePosition());

                        if (drawLine == null) return;

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
            if (playerMove.fullScreenMode)
            {
                cam = GameObject.Find("TV").transform.GetChild(0).GetComponent<Camera>();
            }
            else
            {
                cam = Camera.main;
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            //if (Physics.Raycast(fullRay, out hitInfo))
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.name == "TV" || hitInfo.collider.name == "uWC Window Object(Clone)")
                {
                    Debug.LogWarning($"View id : {pv.ViewID}");

                    lineList.Add(newLine);
                    // 네트워크 동기화
                    pv.RPC("RpcDrawing", RpcTarget.Others, linewidth, color.r, color.g, color.b, sortingOrder);

                    for (int i = 0; i < linePoints.Count; i++)
                    {
                        pv.RPC("SendLineVec", RpcTarget.Others, linePoints[i]);
                    }
                    // List 요소 모두 제거
                    linePoints.Clear();
                }
            }
        }
    }

    public void NetDraw(float _linewidth, float r, float g, float b, int _sortingOrder)
    {
        Color color = new Color(r, g, b);

        Debug.LogWarning("2222222222");
        newLine = new GameObject("Line" + lineList.Count);

        drawLine = newLine.AddComponent<LineRenderer>();
        drawLine.material = Resources.Load<Material>("Color");
        drawLine.material.color = color;
        drawLine.startWidth = _linewidth;
        drawLine.endWidth = _linewidth;
        lineList.Add(newLine);
        _sortingOrder++;
        drawLine.GetComponent<LineRenderer>().sortingOrder = _sortingOrder;
        drawLine.positionCount = 0;
    }

    public void AddLine(Vector3 line)
    {
        print("라인 벡터: " + line);
        drawLine.positionCount++;
        drawLine.SetPosition(drawLine.positionCount - 1, line);
    }


    Vector3 GetMousePosition()
    {
        // 스크린의 마우스 위치로부터 Ray를 만들어냄
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.name == "TV" || hitInfo.collider.name == "uWC Window Object(Clone)")
            {
                Debug.Log(hitInfo.collider.name);
            }
        }
        return hitInfo.point;
    }

    // 지우개 이제 안씀!
    void Eraser()
    {
        // 마우스 왼쪽 버튼을 누르는 순간
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.name == "TV" || hitInfo.collider.name == "uWC Window Object(Clone)")
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
                    if (hitInfo.collider.name == "TV" || hitInfo.collider.name == "uWC Window Object(Clone)")
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.name == "TV" || hitInfo.collider.name == "uWC Window Object(Clone)")
                {
                    Debug.LogWarning($"View id : {pv.ViewID}");

                    // 네트워크 동기화
                    pv.RPC("RpcDrawing", RpcTarget.Others, linewidth, color.r, color.g, color.b, sortingOrder);

                    for (int i = 0; i < linePoints.Count; i++)
                    {
                        pv.RPC("SendLineVec", RpcTarget.Others, linePoints[i]);
                    }
                    // List 요소 모두 제거
                    linePoints.Clear();
                }
            }
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
        for (int i = 0; i < lineList.Count; i++)
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
