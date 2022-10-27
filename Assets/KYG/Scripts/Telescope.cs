using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telescope : MonoBehaviour
{
    public float zoomSpeed = 2000.0f;
    public float zoomRate = 30;
    public bool maxZoom = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Camera.main.fieldOfView = zoomRate*2;
            Cursor.visible = false;
            gameObject.SetActive(false);
        }
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.fieldOfView -= scrollWheel * Time.deltaTime * zoomSpeed;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 30, 60);
        if(Camera.main.fieldOfView == 30)
        {
            maxZoom = true;
        }
        else
        {
            maxZoom = false;
        }
        StarCatch();
    }

    public void StarCatch()
    {

    }
}
