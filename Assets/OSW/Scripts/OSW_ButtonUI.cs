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
        //lineDrawer.SetActive(!lineDrawer.activeSelf);
    }

    public void Drawing()
    {
        // 드로잉 On/Off 기능
        osw_lineDrawer.isDrawing = !osw_lineDrawer.isDrawing;

        // colorPicker On/Off
        colorPicker.SetActive(!colorPicker.activeSelf);

        osw_cursor.CursorChange();
    }

    public void ColorPicker(Button button)
    {
        osw_lineDrawer.color = button.colors.normalColor;
        //selectedColor = button.colors.normalColor;
        //print(selectedColor);
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
    }

    // 전체 삭제 버튼 클릭 시 호출
    public void AllDelete()
    {
        for(int i = 0; i < osw_lineDrawer.lineList.Count; i++)
        {
            Destroy(osw_lineDrawer.lineList[i].gameObject);
        }
        osw_lineDrawer.lineList.Clear();
    }
}
