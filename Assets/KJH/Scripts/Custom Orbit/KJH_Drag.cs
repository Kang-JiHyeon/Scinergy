using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KJH_Drag : MonoBehaviour
{
    KJH_CustomOption custom;

    private void Start()
    {
        custom = GameObject.Find("OrbitManager").GetComponent<KJH_CustomOption>();
    }

    void OnMouseDrag()
    {
        if (custom.isOrbitMove == false)
        {
            float distance = Camera.main.WorldToScreenPoint(transform.position).z;

            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
            objPos.y = 0;

            transform.position = objPos;
        }
    }
}
