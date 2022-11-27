using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OSW_Cursor : MonoBehaviour
{
    public Texture2D[] cursorArray = new Texture2D[1];
    public Texture2D cursorTexture;
    public OSW_LineDrawer lineDrawer;

    // 커서인지 아닌지 확인
    bool isCursor = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (lineDrawer == null)
        {
            lineDrawer = GetComponent<OSW_LineDrawer>();
        }
        if (SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName] == "Audience")
            CursorNull();
    }

    public void CursorChange()
    {
        if (SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName] == "Audience") return;

        if (lineDrawer.isDrawing)
        {
            cursorTexture = cursorArray[0];
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
            lineDrawer.isEraser = false;
        }
        else if (lineDrawer.isEraser)
        {
            //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            lineDrawer.isDrawing = false;
            cursorTexture = cursorArray[1];
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }

    public void CursorNull()
    {
        lineDrawer.isEraser = false;
        lineDrawer.isDrawing = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    
}
