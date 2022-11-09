using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSW_LineDrawer : MonoBehaviourPun
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

    // �÷��̾ �޾�
    PhotonView pv = null;
    
    void Start()
    {
    }

    void Update()
    {
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
            if(SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].IsMine)
            {
                if (isDrawing == true)
                {
                    Drawing();
                }
            }

            if (isEraser == true)
            {
                Eraser();
            }
        }
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
                print(hitInfo.collider.name);
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

                    // ���� �׸��� ������ startColor, endColor�� �� ������ �Ұ�
                    // �׷��� Material�� ���� �����غ��� ���� �ٲ������. 
                    drawLine.material = Resources.Load<Material>("Color");
                    drawLine.material.color = color;

                    drawLine.startWidth = linewidth;
                    drawLine.endWidth = linewidth;

                    // ����Ʈ�� �߰�
                    lineList.Add(newLine);

                    // ��Ʈ��ũ
                    //pv.RPC("RPCDrawing", RpcTarget.OthersBuffered, linewidth, color, sortingOrder, linePoints);
                    pv.RPC("RpcDraw", RpcTarget.OthersBuffered);
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

                        // 1109 ���� üũ
                        if (drawLine == null) return;

                        drawLine.positionCount = linePoints.Count;
                        drawLine.SetPositions(linePoints.ToArray());

                        // ���߿� ���� ���� ���� �ö���Բ�
                        sortingOrder++;
                        drawLine.GetComponent<LineRenderer>().sortingOrder = sortingOrder;

                        // ��Ʈ��ũ
                        //pv.RPC("RPCDrawing", RpcTarget.OthersBuffered, linewidth, color, sortingOrder, linePoints);
                        pv.RPC("RpcDrawing", RpcTarget.OthersBuffered, linePoints, sortingOrder);

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

    [PunRPC]
    void RpcDraw()
    {
        lineList.Add(newLine);
    }

    [PunRPC]
    void RpcDrawing(List<Vector3> _linePoints, int _sortingOrder)
    {
        drawLine.positionCount = _linePoints.Count;
        drawLine.SetPositions(_linePoints.ToArray());

        _sortingOrder++;
        drawLine.GetComponent<LineRenderer>().sortingOrder = _sortingOrder;
    }

    //[PunRPC]
    //void RPCDrawing(float _linewidth, Color color, int _sortingOrder, List<Vector3> _linePoints)
    //{
    //    //Color color = new Color(r, g, b);

    //    // ������ �����Ѵ�.
    //    newLine = new GameObject("Line" + lineList.Count);

    //    /*// ���� ������ ��, ����Ʈ�� active�� false�� �͵��� ����
    //    for (int i = 0; i < lineList.Count; i++)
    //    {
    //        if (lineList[i].activeSelf == false)
    //        {
    //            Destroy(lineList[i].gameObject);
    //            lineList.RemoveAt(i); // RemoveAt�� ����� �ǵ����� �ٽ� ���� �׾����� ���� �ȳ�.
    //            i--;
    //        }
    //    }*/

    //    //�׷����� ���ο� LineRenderer, Material, Color, Width�� �������ش�.
    //    drawLine = newLine.AddComponent<LineRenderer>();

    //    // ���� �׸��� ������ startColor, endColor�� �� ������ �Ұ�
    //    // �׷��� Material�� ���� �����غ��� ���� �ٲ������. 
    //    drawLine.material = Resources.Load<Material>("Color");
    //    drawLine.material.color = color;

    //    drawLine.startWidth = _linewidth;
    //    drawLine.endWidth = _linewidth;

    //    // ����Ʈ�� �߰�
    //    lineList.Add(newLine);

    //    _linePoints.Add(GetMousePosition());
    //    drawLine.positionCount = _linePoints.Count;
    //    drawLine.SetPositions(_linePoints.ToArray());

    //    // ���߿� ���� ���� ���� �ö���Բ�
    //     _sortingOrder++;
    //    drawLine.GetComponent<LineRenderer>().sortingOrder = _sortingOrder;
    //}


    Vector3 GetMousePosition()
    {
        // ��ũ���� ���콺 ��ġ�κ��� Ray�� ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.name == "Board" || hitInfo.collider.name == "uWC Window Object(Clone)")
            {
                Debug.Log(hitInfo.collider.name);
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

                    // ��Ʈ��ũ
                    pv.RPC("RPCDrawing", RpcTarget.OthersBuffered, linewidth, color, sortingOrder, linePoints);
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

                        // ��Ʈ��ũ
                        pv.RPC("RPCDrawing", RpcTarget.OthersBuffered, linewidth, color, sortingOrder, linePoints);
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

    [PunRPC]
    void RPCCtrlZ()
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

    [PunRPC]
    void RPCCtrlY()
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

    [PunRPC]
    public void RPCAllDelete()
    {
        for (int i = 0; i < lineList.Count; i++)
        {
            Destroy(lineList[i].gameObject);
        }
        lineList.Clear();
    }
}
