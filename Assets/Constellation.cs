using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constellation : MonoBehaviour
{
    public bool isSelected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (gameObject.transform.GetChild(i).GetComponent<StarLine>())
                {
                    gameObject.transform.GetChild(i).GetComponent<StarLine>().isSelected = true;
                }
            }
        }
        else
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (gameObject.transform.GetChild(i).GetComponent<StarLine>())
                {
                    gameObject.transform.GetChild(i).GetComponent<StarLine>().isSelected = false;
                }
            }
        }
    }
}
