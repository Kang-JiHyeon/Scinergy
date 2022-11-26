using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BasicConstellationList : MonoBehaviour
{
    public Transform basicConstellationListContent;
    public GameObject ConstellationItemFactory;
    public GameObject SelectedConstellation;
    public TextMeshProUGUI SelectedConstellationName;
    public GameObject SelectedConstellationItem;
    public GameObject Player;
    public GameObject CreatedConstellationListUI;
    // Start is called before the first frame update
    void Start()
    {
        foreach(KeyValuePair<string, GameObject> basicConstellation in GameManager.instance.basicConstellationList)
        {
            Init(basicConstellation.Key, basicConstellation.Value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(string constellationName, GameObject constellation)
    {
        GameObject constellationItem = Instantiate(ConstellationItemFactory, basicConstellationListContent);
        BasicConstellationItem item = constellationItem.GetComponent<BasicConstellationItem>();
        item.constellation = constellation;
        item.constellationName.text = constellationName;
    }
    public void OnConstellationLookBtn()
    {
        Vector3 ConstellationPosition = Vector3.zero;
        float averageX = 0;
        float averageY = 0;
        float averageZ = 0;
        int childStarCount = 0;
        for (int i = 0; i < SelectedConstellation.transform.childCount; i++)
        {
            Star childStar = SelectedConstellation.transform.GetChild(i).GetComponent<Star>();
            if (childStar)
            {
                childStarCount++;
                averageX += childStar.transform.position.x;
                averageY += childStar.transform.position.y;
                averageZ += childStar.transform.position.z;
            }
            ConstellationPosition = new Vector3(averageX / childStarCount, averageY / childStarCount, averageZ / childStarCount);
            print(childStarCount);
        }
        Player.GetComponent<PlayerRot>().StarSet(ConstellationPosition);
    }
}
