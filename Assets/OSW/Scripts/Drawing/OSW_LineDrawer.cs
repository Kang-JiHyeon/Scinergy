using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSW_LineDrawer : MonoBehaviourPun
{
    // ���� ��
    public Color color = Color.black;

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

    int sortingOrder;

    // 1127 PlayerMove ��ũ��Ʈ �ޱ�
    PlayerMove playerMove;

    // �÷��̾ �޾�
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
       // 1127 playerMove �ޱ�
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
            // ���� ��ư�� ���ȴٸ� ����� ����!
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

        // ���콺 ���� ��ư�� ������ ����
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
                    // ������ �����Ѵ�.
                    newLine = new GameObject("Line" + lineList.Count);

                    // ���� ������ ��, ����Ʈ�� active�� false�� �͵��� ����
                    for (int i = 0; i < lineList.Count; i++)
                    {
                        if (lineList[i] == null) continue;
                        if (lineList[i].activeSelf == false)
                        {
                            Destroy(lineList[i].gameObject);
                            lineList.RemoveAt(i); // RemoveAt�� ����� �ǵ����� �ٽ� ���� �׾����� ���� �ȳ�.
                            i--;
                        }
                    }

                    //�׷����� ���ο� LineRenderer, Material, Color, Width�� �������ش�.
                    drawLine = newLine.AddComponent<LineRenderer>();

                    // ���� �׸��� ������ startColor, endColor�� �� ������ �Ұ�
                    // �׷��� Material�� ���� �����غ��� ���� �ٲ������. 
                    drawLine.material = Resources.Load<Material>("Color");
                    drawLine.material.color = color;

                    drawLine.startWidth = linewidth;
                    drawLine.endWidth = linewidth;
                }
            }
        }

        // ���콺 ���� ��ư�� ���� ����
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
                        // ���� ������ ��, ����Ʈ�� active�� false�� �͵��� ����
                        for (int i = 0; i < lineList.Count; i++)
                        {
                            if (lineList[i] == null) continue;
                            if (lineList[i].activeSelf == false)
                            {
                                
                                Destroy(lineList[i].gameObject);
                                lineList.RemoveAt(i); // RemoveAt�� ����� �ǵ����� �ٽ� ���� �׾����� ���� �ȳ�.
                                i--;
                            }
                        }

                        linePoints.Add(GetMousePosition());

                        if (drawLine == null) return;

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
                    // ��Ʈ��ũ ����ȭ
                    pv.RPC("RpcDrawing", RpcTarget.Others, linewidth, color.r, color.g, color.b, sortingOrder);

                    for (int i = 0; i < linePoints.Count; i++)
                    {
                        pv.RPC("SendLineVec", RpcTarget.Others, linePoints[i]);
                    }
                    // List ��� ��� ����
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
        print("���� ����: " + line);
        drawLine.positionCount++;
        drawLine.SetPosition(drawLine.positionCount - 1, line);
    }


    Vector3 GetMousePosition()
    {
        // ��ũ���� ���콺 ��ġ�κ��� Ray�� ����
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

    // ���찳 ���� �Ⱦ�!
    void Eraser()
    {
        // ���콺 ���� ��ư�� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.name == "TV" || hitInfo.collider.name == "uWC Window Object(Clone)")
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
                    if (hitInfo.collider.name == "TV" || hitInfo.collider.name == "uWC Window Object(Clone)")
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.name == "TV" || hitInfo.collider.name == "uWC Window Object(Clone)")
                {
                    Debug.LogWarning($"View id : {pv.ViewID}");

                    // ��Ʈ��ũ ����ȭ
                    pv.RPC("RpcDrawing", RpcTarget.Others, linewidth, color.r, color.g, color.b, sortingOrder);

                    for (int i = 0; i < linePoints.Count; i++)
                    {
                        pv.RPC("SendLineVec", RpcTarget.Others, linePoints[i]);
                    }
                    // List ��� ��� ����
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
