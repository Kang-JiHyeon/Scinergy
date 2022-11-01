using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject starGenerator;
    public GameObject starList;
    public GameObject telescope;
    public GameObject constellation;
    public GameObject compas;
    public GameObject Clock;
    public GameObject Map;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCompasBtn()
    {
        //compasActive = !compasActive;
        if (compas.activeSelf)
        {
            compas.SetActive(false);
        }
        else
        {
            compas.SetActive(true);
        }
    }
    public void OnStarGeneratorBtn()
    {
        if (starGenerator.activeSelf)
        {
            starGenerator.SetActive(false);
        }
        else
        {
            starGenerator.SetActive(true);
        }
    }
    public void OnStarListBtn()
    {
        if (starList.activeSelf)
        {
            starList.SetActive(false);
        }
        else
        {
            starList.SetActive(true);
        }
    }

    public void OnTelescopeBtn()
    {
        if (telescope.activeSelf)
        {
            telescope.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
            //Camera.main.fieldOfView = 30;
            Cursor.visible = false;
            telescope.SetActive(true);
        }
    }

    public void OnConstellationBtn()
    {
        if (constellation.activeSelf)
        {
            constellation.SetActive(false);
        }
        else
        {
            constellation.SetActive(true);
        }
    }
    public void OnClockBtn()
    {
        if (Clock.activeSelf)
        {
            Clock.SetActive(false);
        }
        else
        {
            Clock.SetActive(true);
        }
    }
    public void OnMapBtn()
    {
        if (Map.activeSelf)
        {
            Map.SetActive(false);
        }
        else
        {
            Map.SetActive(true);
        }
    }

    //public void OnTimeFlowBtn()
    //{
    //    Clock.GetComponent<Clock>().timeFlow = true;
    //    Clock.GetComponent<Clock>().timeReverse = false;
    //}
    //public void OnTimeReverseBtn()
    //{
    //    Clock.GetComponent<Clock>().timeFlow = false;
    //    Clock.GetComponent<Clock>().timeReverse = true;
    //}
    //public void OnTimeStopBtn()
    //{
    //    Clock.GetComponent<Clock>().timeFlow = false;
    //    Clock.GetComponent<Clock>().timeReverse = false;
    //}
}
