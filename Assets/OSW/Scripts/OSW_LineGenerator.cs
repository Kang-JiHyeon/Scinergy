using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSW_LineGenerator : MonoBehaviour
{
    public GameObject linePrefab;

    OSW_Line activeLine;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newLine = Instantiate(linePrefab);
            activeLine = newLine.GetComponent<OSW_Line>();
        }

        if (Input.GetMouseButtonUp(0))
        {
            activeLine = null;
        }
        
        if(activeLine != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
        }
    }
}
