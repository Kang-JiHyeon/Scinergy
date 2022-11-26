using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OSW_ButtonUI : MonoBehaviourPun
{
    public GameObject drawOnOff;
    public GameObject drawingTool;
    //public GameObject lineDrawer;
    public GameObject colorPicker;
    public Color selectedColor;
    public List<GameObject> colors;

    // 11.23 ��ư pressed ���� ǥ���� ���� �ڵ�
    public List<Button> btn;
    public List<Sprite> ChangeImg; // �ٲ� �̹���
    public List<Sprite> OriginImg; // ���� �̹���

    // ��ũ��Ʈ ����
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
        //drawingTool.SetActive(!drawingTool.activeSelf);
        if (!drawingTool.activeSelf)
        {
            drawingTool.SetActive(true);
            drawOnOff.GetComponent<Image>().sprite = ChangeImg[1];
        }
        else
        {
            drawingTool.SetActive(false);
            drawOnOff.GetComponent<Image>().sprite = OriginImg[1];
        }
    }

    public void Drawing()
    {
        // colorPicker On/Off
        colorPicker.SetActive(!colorPicker.activeSelf);
        osw_lineDrawer.isEraser = false;
    }

    public void ColorPicker(Button button)
    {
        osw_lineDrawer.isDrawing = !osw_lineDrawer.isDrawing;

        if (osw_lineDrawer.isDrawing)
        {
            osw_lineDrawer.color = button.colors.normalColor;
            colorPicker.SetActive(!colorPicker.activeSelf);
            osw_cursor.CursorChange();
            btn[0].transform.GetChild(0).GetComponent<Image>().sprite = ChangeImg[0];
        }
        else if (!osw_lineDrawer.isDrawing)
        {
            btn[0].transform.GetChild(0).GetComponent<Image>().sprite = OriginImg[0];
        }
    }
    
    // ���찳 ���� �Ⱦ�
    public void Eraser()
    {
        osw_lineDrawer.isDrawing = false;
        osw_lineDrawer.isEraser = true;
        osw_lineDrawer.color = Color.white;
        osw_lineDrawer.linewidth = 0.3f;

        osw_cursor.CursorChange();
    }

    // �� �β� ����
    public void WitdhBtn(Button button)
    {
        if(button.name == "1") osw_lineDrawer.linewidth = 0.1f;
        else if(button.name == "2") osw_lineDrawer.linewidth = 0.2f;
        else if(button.name == "3") osw_lineDrawer.linewidth = 0.3f;
    }
    


    // ������� ��ư Ŭ�� �� ȣ��
    public void CtrlZ()
    {
        // ��Ʈ��ũ ����ȭ
        PhotonView mine = SYA_SymposiumManager.Instance.GetMyPlayer();
        if (mine)
        {
            mine.RPC("RPCCtrlZ", RpcTarget.All);
        }
    }

    // �ǵ����� ��ư Ŭ�� �� ȣ��
    public void CtrlY()
    {
        // ��Ʈ��ũ ����ȭ
        PhotonView mine = SYA_SymposiumManager.Instance.GetMyPlayer();
        if(mine)
        {
            mine.RPC("RPCCtrlY", RpcTarget.All);
        }
        
    }

    // ��ü ���� ��ư Ŭ�� �� ȣ��
    public void AllDelete()
    {
        PhotonView mine = SYA_SymposiumManager.Instance.GetMyPlayer();
        if (mine)
        {
            mine.RPC("RPCAllDelete", RpcTarget.All);
        }
    }
}
