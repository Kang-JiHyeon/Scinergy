using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLine : MonoBehaviour
{
    public LineRenderer starLine;
    public GameObject star1;
    public GameObject star2;
    public Material[] starLines;
    public bool isSelected;
    // Start is called before the first frame update
    void Start()
    {
        starLine = GetComponent<LineRenderer>();
        starLine.startWidth = 0.5f;
        starLine.endWidth = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            starLine.material = starLines[1];
        }
        else
        {
            starLine.material = starLines[0];
        }
        if(star1 && star2)
        {
            starLine.SetPosition(0, star1.transform.position);
            starLine.SetPosition(1, star2.transform.position);
            if(star1.GetComponent<Star>() && star2.GetComponent<Star>())
            {
                if (star1.GetComponent<Star>().StarState == Star.State.shootingStar || star2.GetComponent<Star>().StarState == Star.State.shootingStar)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
