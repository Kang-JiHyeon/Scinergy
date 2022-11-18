using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KJH_EclipsDrag : MonoBehaviour
{
    Transform earth;
    Transform moon;
    KJH_RotateAround moonRotate;
    float radius;
    public float minDis = 5f; 
    public float maxDis = 8f; 

    void Start()
    {
        earth = GameObject.Find("Earth").transform;
        moon = GameObject.Find("Moon").transform;
        radius = Vector3.Distance(earth.position, moon.position);
        moonRotate = moon.GetComponent<KJH_RotateAround>();
    }

    private void Update()
    {

    }

    void OnMouseDrag()
    {
        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
        objPos.y = 0;

        if(gameObject.name == "Earth")
        {
            objPos.z = 0;
            objPos.x = Mathf.Clamp(objPos.x, minDis, maxDis);
            transform.position = objPos;
        }
        else if(gameObject.name == "Moon")
        {
            moonRotate.isStop = true;
            Vector3 dir = objPos - earth.position;

            Vector3 clampedDir = Mathf.Abs(dir.magnitude- radius) < 0.1f ? dir : dir.normalized * radius;

            transform.localPosition = clampedDir;
        }

    }

    private void OnMouseUp()
    {
        moonRotate.isStop = false;
    }

}
