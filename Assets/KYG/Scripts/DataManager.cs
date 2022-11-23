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
}


public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public List<ZodiacInfo> zodiacInfo;

    public List<StarInfo> starInfo;

    public GameObject ConstellationFactory;

    public GameObject starFactory;

    public GameObject brightness;
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
        for(int i = 0; i<starInfo.Count; i++)
        {
            GameObject star = Instantiate(starFactory);
            GameManager.instance.createdStarList[starInfo[i].starName] = star;
            star.GetComponent<Star>().InfoSet(starInfo[i].starName, starInfo[i].ra, starInfo[i].dec,starFactory, brightness, 1);
        }
        for(int i = 0; i<zodiacInfo.Count; i++)
        {
            GameObject constellation = Instantiate(ConstellationFactory);
            constellation.transform.parent = GameManager.instance.CelestialSphere.transform;
            constellation.name = zodiacInfo[i].name;
            GameManager.instance.createdConstellationList[name] = constellation;
            //for(int j=0; j<zodiacInfo[i].starCount; j++)
            //{

            //}
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }  
}
