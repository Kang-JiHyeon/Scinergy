using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ZodiacInfo
{
    public string name;
    public string krName;
    public int starCount;
}
[System.Serializable]
public class StarInfo
{
    public string starName;
    public float ra;
    public float dec;
    public string brightness;
}


public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public List<ZodiacInfo> zodiacInfo;

    public List<StarInfo> starInfo;

    public GameObject ConstellationFactory;

    public GameObject starFactory;

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
        starInfo = CSV.instance.Parsing<StarInfo>("starInfo");
        int starIndex = 0;
        for(int i = 0; i<zodiacInfo.Count; i++)
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
                star.GetComponent<Star>().InfoSet(starInfo[j].starName, starInfo[j].ra, starInfo[j].dec, starFactory, brightness, 1);
                star.transform.parent = constellation.transform;
            }
            starIndex+= zodiacInfo[i].starCount;
            
        }
        //for(int i = 0; i<starInfo.Count; i++)
        //{
        //    GameObject star = Instantiate(starFactory);
        //    GameManager.instance.createdStarList[starInfo[i].starName] = star;
        //    star.GetComponent<Star>().InfoSet(starInfo[i].starName, starInfo[i].ra, starInfo[i].dec,starFactory, brightness, 1);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }  
}
