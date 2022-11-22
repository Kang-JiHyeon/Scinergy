using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_Cloud : MonoBehaviour
{
    public GameObject cloud;
    public GameObject updraft;
    float currentTime = 0;
    public float createTime;

    void Start()
    {
        createTime = updraft.GetComponentInChildren<Test_Particle>().fadeTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime>=createTime-1)
        {
            currentTime = 0;
            cloud.SetActive(true);
        }
    }
}
