using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    bool isRotate = true;
    public float rotSpeed = 205;
    public GameObject Clock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Clock.GetComponent<Clock>().timeFlow) transform.Rotate(Vector3.up * 360/Clock.GetComponent<Clock>().REAL_SECONDS_PER_INGAME_DAY * Time.deltaTime);
    }
    public void OnRotateBtn()
    {
        isRotate = !isRotate;
    }
}
