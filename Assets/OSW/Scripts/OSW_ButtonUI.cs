using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OSW_ButtonUI : MonoBehaviour
{
    public GameObject drawOnOff;
    public GameObject drawingTool;
    public GameObject lineDrawer;
    public GameObject colorPicker;
    public Color selectedColor;
    public List<GameObject> colors;

    // 스크립트 동적
    public OSW_LineDrawer osw_lineDrawer;
    public OSW_Cursor osw_cursor;
    
    void Start()
    {
        osw_lineDrawer = GetComponent<OSW_LineDrawer>();
        osw_cursor = GetComponent<OSW_Cursor>();

        for (int i = 0; i < colorPicker.transform.childCount; i++)
        {
            colors.Add(colorPicker.transform.GetChild(i).gameObject);
        }
    }

    public void DrawOnOff()
    {
        drawingTool.SetActive(!drawingTool.activeSelf);
    }

    public void Drawing()
    {
        // colorPicker On/Off
        colorPicker.SetActive(!colorPicker.activeSelf);
        osw_lineDrawer.isEraser = false;
    }

    public void ColorPicker(Button button)
    {
        osw_lineDrawer.isDrawing = true;

        if (osw_lineDrawer.isDrawing)
        {
            osw_lineDrawer.color = button.colors.normalColor;
            colorPicker.SetActive(!colorPicker.activeSelf);
            osw_cursor.CursorChange();
        }
        else
        {
            // 드로잉 On/Off 기능
            osw_lineDrawer.isDrawing = !osw_lineDrawer.isDrawing;
        }

    }
    public void Eraser()
    {
        osw_lineDrawer.isDrawing = false;
        osw_lineDrawer.isEraser = !osw_lineDrawer.isEraser;
        osw_lineDrawer.color = Color.white;
        osw_lineDrawer.linewidth = 0.3f;

        osw_cursor.CursorChange();
    }

    // 얇은 선 두께
    public void ThinWitdh()
    {
        osw_lineDrawer.linewidth = 0.1f;
    }

    // 중간 선 두께
    public void MiddleWitdh()
    {
        osw_lineDrawer.linewidth = 0.2f;
    }

    // 두꺼운 선 두께
    public void ThickWitdh()
    {
        osw_lineDrawer.linewidth = 0.3f;
    }


    // 실행취소 버튼 클릭 시 호출
    public void CtrlZ()
    {
        for(int i = osw_lineDrawer.lineList.Count - 1; i >= 0; i--)
        {
            if (osw_lineDrawer.lineList[i].activeSelf == true)
            {
                osw_lineDrawer.lineList[i].SetActive(false);
                break;
            }
        }

        // 네트워크 동기화
        osw_lineDrawer.photonView.RPC("RPCCtrlZ", RpcTarget.OthersBuffered);
    }

    // 되돌리기 버튼 클릭 시 호출
    public void CtrlY()
    {
        for(int i = 0; i < osw_lineDrawer.lineList.Count; i++)
        {
            if (osw_lineDrawer.lineList[i].activeSelf == false)
            {
                osw_lineDrawer.lineList[i].SetActive(true);
                break;
            }
        }

        // 네트워크 동기화
        osw_lineDrawer.photonView.RPC("RPCCtrlY", RpcTarget.OthersBuffered);
    }

    // 전체 삭제 버튼 클릭 시 호출
    public void AllDelete()
    {
        for(int i = 0; i < osw_lineDrawer.lineList.Count; i++)
        {
            Destroy(osw_lineDrawer.lineList[i].gameObject);
        }
        osw_lineDrawer.lineList.Clear();

        // 네트워크 동기화
        osw_lineDrawer.photonView.RPC("RPCAllDelete", RpcTarget.OthersBuffered);
    }
}
