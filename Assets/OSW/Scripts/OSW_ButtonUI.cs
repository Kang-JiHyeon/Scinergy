using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSW_ButtonUI : MonoBehaviour
{
    public GameObject drawOnOff;
    public GameObject drawingTool;
    public GameObject lineDrawer;

    public OSW_LineDrawer osw_lineDrawer;
    
    void Start()
    {
        osw_lineDrawer = GetComponent<OSW_LineDrawer>();
    }


    public void DrawOnOff()
    {
        drawingTool.SetActive(!drawingTool.activeSelf);
        //lineDrawer.SetActive(!lineDrawer.activeSelf);
    }

    public void Drawing()
    {
        osw_lineDrawer.isDrawing = !osw_lineDrawer.isDrawing;
        //lineDrawer.SetActive(!lineDrawer.activeSelf);
    }

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

    public void AllDelete()
    {
        for(int i = 0; i < osw_lineDrawer.lineList.Count; i++)
        {
            Destroy(osw_lineDrawer.lineList[i].gameObject);
        }
        osw_lineDrawer.lineList.Clear();
    }
}
