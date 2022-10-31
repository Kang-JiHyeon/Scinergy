using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSW_Cursor : MonoBehaviour
{
    public Texture2D[] cursorArray = new Texture2D[1];
    public Texture2D cursorTexture;
    public OSW_LineDrawer lineDrawer;

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
        if(lineDrawer == null)
        {
            lineDrawer = GetComponent<OSW_LineDrawer>();
        }

        //if(lineDrawer.brushNum != brushNum_temp)
        //{
        //    //StartCoroutine("MouseCursor");
        //    brushNum_temp = lineDrawer.brushNum;
        //}
    }


    //IEnumerator MouseCursor()
    //{
    //    yield return new WaitForEndOfFrame();

    //    Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    //}
}   
