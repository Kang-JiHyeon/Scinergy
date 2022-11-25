using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ZodiacInfo
{
    public string name;
    public int starCount;
}
[System.Serializable]
public class StarInfo
{
    public string constellationName;
    public string starName;
    public float apparentMagnitude;
    public float distance;
    public float ra;
    public float dec;
    public string brightness;
    public string starLine;
}

[System.Serializable]
public class StarTest
{
    public string constellationName;
    public string starName;
    public float apparentMagnitude;
    public float distance;
    public float ra;
    public float dec;
    public string brightness;
    //public string starLine;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public List<ZodiacInfo> zodiacInfo;

    public List<StarInfo> starInfo;

    public List<StarTest> starTests;

    public GameObject ConstellationFactory;

    public GameObject starFactory;

    public GameObject starLineFactory;

    public List<GameObject> brightnessList = new();
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    } 
    // Start is called before the first frame update
    void Start()
    {
        zodiacInfo = CSV.instance.Parsing<ZodiacInfo>("Zodiac");
        starInfo = CSV.instance.Parsing<StarInfo>("StarList");
        //starTests = CSV.instance.Parsing<StarTest>("starTest");
        int starIndex = 0;
        for (int i = 0; i < zodiacInfo.Count; i++)
        {
            GameObject constellation = Instantiate(ConstellationFactory);
            constellation.transform.parent = GameManager.instance.CelestialSphere.transform;
            constellation.name = zodiacInfo[i].name;
            GameManager.instance.createdConstellationList[name] = constellation;
            for (int j = starIndex; j < zodiacInfo[i].starCount + starIndex; j++)
            {
                GameObject star = Instantiate(starFactory);
                GameManager.instance.createdStarList[starInfo[j].starName] = star;
                GameObject brightness = brightnessList.Find(x => x.name == starInfo[j].brightness);
                star.GetComponent<Star>().InfoSet(starInfo[j].starName, starInfo[j].ra, starInfo[j].dec, starFactory, brightness, starInfo[j].apparentMagnitude, 1);
                star.transform.parent = constellation.transform;
            }
            starIndex += zodiacInfo[i].starCount;
        }
        for (int k = 0; k < starInfo.Count; k++)
        {
            string[] starLineList = starInfo[k].starLine.Split(":");
            for (int m = 0; m < starLineList.Length; m++)
            {
                GameObject line = Instantiate(starLineFactory);
                StarLine starLine = line.GetComponent<StarLine>();
                starLine.star1 = GameObject.Find(starInfo[k].starName);
                starLine.star2 = GameObject.Find(starLineList[m]);
                line.transform.parent = starLine.star1.transform.parent.transform;
            }
        }
    }   

    // Update is called once per frame
    void Update()
    {
        
    }  
}
