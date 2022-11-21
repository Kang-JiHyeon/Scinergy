using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ZodiacInfo
{
    public string name;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public List<ZodiacInfo> zodiacInfos;

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
        zodiacInfos = CSV.instance.Parsing<ZodiacInfo>("Zodiac");
    }

    // Update is called once per frame
    void Update()
    {
        
    }  
}
