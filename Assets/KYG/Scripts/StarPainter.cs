using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPainter : MonoBehaviour
{
    public GameObject lineFactory;
    public StarLine starLine;
    GameObject star1;
    GameObject star2;
    //LineRenderer starLine;
    //public Vector3 star1Pos, star2Pos;
    //public GameObject star2;
    // Start is called before the first frame update
    void Start()
    {
        //starLine = GetComponent<LineRenderer>();
        //starLine.startWidth = 1f;
        //starLine.endWidth = 1f;
        //star1Pos = gameObject.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        //starLine.SetPosition(0, star1Pos);
        //starLine.SetPosition(1, star2.transform.position);
        StarRay();
    }
    void StarRay()
    {
        Ray starRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit starInfo;
        
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(starRay, out starInfo))
            {
                if (starInfo.transform.GetComponent<Star>())
                {
                    print(starInfo.transform.name);
                    star1 = starInfo.transform.gameObject;
                              
                }
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (Physics.Raycast(starRay, out starInfo))
            {
                if (starInfo.transform.GetComponent<Star>())
                {
                    GameObject line = Instantiate(lineFactory);
                    star2 = starInfo.transform.gameObject;
                    starLine = line.GetComponent<StarLine>();
                    starLine.star1 = star1;
                    starLine.star2 = star2;
                }
            }
        }

    }
}
