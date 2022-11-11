using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KJH_SpaceSceneManager : MonoBehaviour
{
    public static KJH_SpaceSceneManager instance;
    public bool isSolar = false;
    public bool isTime = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Load_SolarSystemScene()
    {
        if(SceneManager.GetActiveScene().name != "KJH_SolarSystemScene")
        {
            SceneManager.LoadScene("KJH_SolarSystemScene");
        }

    }

    public void Load_CustomOrbitScene()
    {
        if (SceneManager.GetActiveScene().name != "KJH_OrbitScene")
        {
            SceneManager.LoadScene("KJH_OrbitScene");
        }
    }
}
