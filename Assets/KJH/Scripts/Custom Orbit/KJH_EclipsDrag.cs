using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KJH_EclipsDrag : MonoBehaviour
{
    Transform earthPos;
    Transform moonPos;
    float radius;
    private void Start()
    {
        earthPos = GameObject.Find("Earth").transform;
        moonPos = GameObject.Find("Moon").transform;
        radius = Vector3.Distance(earthPos.position, moonPos.position);
    }


    void OnMouseDrag()
    {
        //// ui를 클릭했을 때 실행되지 않도록 반환
        //if (EventSystem.current.IsPointerOverGameObject())
        //    return;

        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
        objPos.y = 0;

        if(gameObject.name == "Earth")
        {
            objPos.z = 0;
            transform.position = objPos;
        }
        else if(gameObject.name == "Moon")
        {
            //float r = Mathf.Sqrt(objPos.x * objPos.x + objPos.y * objPos.y);
            //float theta = Mathf.Acos(objPos.x / r);

            //float x = transform.position.x + Mathf.Sign(theta) * Mathf.Rad2Deg;
            //float z = transform.position.z + Mathf.Cos(theta) * Mathf.Rad2Deg;

            //transform.position = new Vector3(x, 0, z);
            ////float dis = Vector3.Distance(objPos, earthPos.position);
            ////if (Mathf.Abs(dis - radius) < 0.1f)
            ////    transform.position = objPos;

            Vector3 dir = objPos - earthPos.position;

            Vector3 clampedDir = Mathf.Abs(dir.magnitude- radius) < 0.1f ? dir : dir.normalized * radius;

            transform.localPosition = clampedDir;



        }

    }
}
