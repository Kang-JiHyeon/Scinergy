using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OSW_Cursor : MonoBehaviour
{
    public Texture2D[] cursorArray = new Texture2D[1];
    public Texture2D cursorTexture;
    public OSW_LineDrawer lineDrawer;

    // 커서인지 아닌지 확인
    bool isCursor = false;

    //public int brushNum_temp;
    void Start()
    {
        cursorTexture = cursorArray[0];
        //StartCoroutine("MouseCursor");

        //brushNum_temp = lineDrawer.brushNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (lineDrawer == null)
        {
            lineDrawer = GetComponent<OSW_LineDrawer>();
        }
    }

    public void CursorChange()
    {
        isCursor = !isCursor;

        if (isCursor)
        {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }
        else
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
