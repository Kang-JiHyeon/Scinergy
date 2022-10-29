using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public bool mapControl = false;
    public float rotSpeed = 205;
    public GameObject Clock;
    public GameObject worldMap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (Clock.GetComponent<Clock>().timeFlow || Clock.GetComponent<Clock>().timeReverse) transform.Rotate(Vector3.up * 360/Clock.GetComponent<Clock>().REAL_SECONDS_PER_INGAME_DAY * Time.deltaTime);
        if(mapControl)transform.rotation = Quaternion.identity * Quaternion.Euler(-worldMap.GetComponent<WorldMap>().latitudeAdjust, worldMap.GetComponent<WorldMap>().longitudeAdjust,0);
    }
}
