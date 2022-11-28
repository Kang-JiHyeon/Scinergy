using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Constellation : MonoBehaviour
{
    public bool isSelected;
    public GameObject ConstellationName;
    Vector3 constellationPosition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        ConstellationName.GetComponent<TextMeshProUGUI>().text = gameObject.name + "ÀÚ¸®";
        
        float averageX = 0;
        float averageY = 0;
        float averageZ = 0;
        int childStarCount = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            Star childstar = transform.GetChild(i).GetComponent<Star>();
            if (childstar)
            {
                childStarCount++;
                averageX += childstar.transform.position.x;
                averageY += childstar.transform.position.y;
                averageZ += childstar.transform.position.z;
            }
        }
        constellationPosition = new Vector3(averageX / childStarCount, averageY / childStarCount, averageZ / childStarCount);
    }

    // Update is called once per frame
    void Update()
    {
        //ConstellationName.transform.localPosition = constellationPosition;
        if (isSelected)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (gameObject.transform.GetChild(i).GetComponent<StarLine>())
                {
                    gameObject.transform.GetChild(i).GetComponent<StarLine>().isSelected = true;
                }
            }
            ConstellationName.SetActive(true);
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
            ConstellationName.SetActive(false);
        }
    }
}
