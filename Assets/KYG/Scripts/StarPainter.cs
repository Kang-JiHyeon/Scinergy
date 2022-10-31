using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPainter : MonoBehaviour
{
    LineRenderer starLine;
    public Vector3 star1Pos, star2Pos;
    public GameObject star2;
    // Start is called before the first frame update
    void Start()
    {
        starLine = GetComponent<LineRenderer>();
        starLine.startWidth = 1f;
        starLine.endWidth = 1f;
        star1Pos = gameObject.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        starLine.SetPosition(0, star1Pos);
        starLine.SetPosition(1, star2.transform.position);
    }
}
