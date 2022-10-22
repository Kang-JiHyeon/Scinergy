using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//½Ã°è

public class Clock : MonoBehaviour
{
    public bool timeFlow = false;
    public float REAL_SECONDS_PER_INGAME_DAY = 10f;
    private Transform hourHandTransform;
    private Transform minHandTransform;
    public float day;
    private void Awake()
    {
        hourHandTransform = transform.Find("HourHand");
        minHandTransform = transform.Find("MinuteHand");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeFlow)
        {
            day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;
        }
        else
        {
            return;
        }
        float dayNormalized = day % 1f;
        float rotationDegreePerDay = 720f;
        hourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreePerDay);

        float hoursPerDay = 24f;
        minHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreePerDay * hoursPerDay);


    }
}
