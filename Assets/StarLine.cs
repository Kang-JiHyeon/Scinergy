using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLine : MonoBehaviour
{
    public LineRenderer starLine;
    public GameObject star1;
    public GameObject star2;
    // Start is called before the first frame update
    void Start()
    {
        starLine = GetComponent<LineRenderer>();
        starLine.startWidth = 1f;
        starLine.endWidth = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
        starLine.SetPosition(0, star1.transform.position);
        starLine.SetPosition(1, star2.transform.position);
    }
}
