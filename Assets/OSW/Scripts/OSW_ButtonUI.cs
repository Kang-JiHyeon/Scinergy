using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OSW_ButtonUI : MonoBehaviourPun
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
        osw_lineDrawer.isEraser = false;

        if (osw_lineDrawer.isDrawing)
        {
            osw_lineDrawer.color = button.colors.normalColor;
            colorPicker.SetActive(!colorPicker.activeSelf);
            osw_cursor.CursorChange();
        }
    }
    public void Eraser()
    {
        osw_lineDrawer.isDrawing = false;
        osw_lineDrawer.isEraser = true;
        osw_lineDrawer.color = Color.white;
        osw_lineDrawer.linewidth = 0.3f;

        osw_cursor.CursorChange();
    }

    // 선 두께 변경
    public void WitdhBtn(Button button)
    {
        if(button.name == "1") osw_lineDrawer.linewidth = 0.1f;
        else if(button.name == "2") osw_lineDrawer.linewidth = 0.2f;
        else if(button.name == "3") osw_lineDrawer.linewidth = 0.3f;
    }
    


    // 실행취소 버튼 클릭 시 호출
    public void CtrlZ()
    {
        // 네트워크 동기화
        PhotonView mine = SYA_SymposiumManager.Instance.GetMyPlayer();
        if (mine)
        {
            mine.RPC("RPCCtrlZ", RpcTarget.All);
        }
    }

    // 되돌리기 버튼 클릭 시 호출
    public void CtrlY()
    {
        // 네트워크 동기화
        PhotonView mine = SYA_SymposiumManager.Instance.GetMyPlayer();
        if(mine)
        {
            mine.RPC("RPCCtrlY", RpcTarget.All);
        }
        
    }

    // 전체 삭제 버튼 클릭 시 호출
    public void AllDelete()
    {
        PhotonView mine = SYA_SymposiumManager.Instance.GetMyPlayer();
        if (mine)
        {
            mine.RPC("RPCAllDelete", RpcTarget.All);
        }
    }
}
