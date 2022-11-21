using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Temp : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject test = GameObject.Find("Canvas_DontDestroy");
            print(test.name);
            GraphicRaycaster gl = test.GetComponent<GraphicRaycaster>();
            if (gl == null)
            {
                print("캔버스 못찾음");
                return;
            }
            
            PointerEventData ped = new PointerEventData(null);


            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();

            gl.Raycast(ped, results);

            foreach (RaycastResult res in results)
            {
                print(res.gameObject.name);
            }
        }
    }
}
