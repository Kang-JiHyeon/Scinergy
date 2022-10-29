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
        if (gameObject.activeSelf)
        {
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            Camera.main.fieldOfView -= scrollWheel * Time.deltaTime * zoomSpeed;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 30, 60);
        }
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
        //if (maxZoom)
        //{
        //    for(int i = 0; i<GameManager.instance.createdStarList.Count; i++)
        //    {
        //        Vector3 viewPos = Camera.main.WorldToViewportPoint(GameManager.instance.createdStarList[i].transform.position);
        //        if(viewPos.x >=0 && viewPos.x<=1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        //        {
        //            GameManager.instance.createdStarList[i].GetComponent<Star>().starInfo.SetActive(true);
        //        }
        //        else
        //        {
        //            GameManager.instance.createdStarList[i].GetComponent<Star>().starInfo.SetActive(false);
        //        }
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < GameManager.instance.createdStarList.Count; i++)
        //    {
        //        GameManager.instance.createdStarList[i].GetComponent<Star>().starInfo.SetActive(false);
        //    }
        //}

        if (maxZoom)
        {
            foreach(KeyValuePair<string,GameObject> star in GameManager.instance.createdStarList)
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(star.Value.transform.position);
                if(Vector2.Distance(screenPos, new Vector2(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2))<500)
                {
                    star.Value.GetComponent<Star>().starInfo.SetActive(true);
                }
                else
                {
                    star.Value.GetComponent<Star>().starInfo.SetActive(false);
                }
            }
        }
        else
        {
            foreach (KeyValuePair<string, GameObject> star in GameManager.instance.createdStarList)
            {
                star.Value.GetComponent<Star>().starInfo.SetActive(false);
            }
        }
    }
}
