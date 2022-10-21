using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject compas;
    public GameObject starGenerator;
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

    public void OnTimeFlowBtn()
    {
        Clock.GetComponent<Clock>().timeFlow = !Clock.GetComponent<Clock>().timeFlow;
        
    }
}
