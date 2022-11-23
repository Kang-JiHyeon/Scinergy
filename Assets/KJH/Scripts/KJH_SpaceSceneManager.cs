using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KJH_SpaceSceneManager : MonoBehaviour
{
    public static KJH_SpaceSceneManager instance;
    public bool isSolar = false;
    public bool isTime = false;

    public Dictionary<int, string> dic_SceneNames = new Dictionary<int, string>();

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
        dic_SceneNames.Add(0, "KJH_SolarSystemScene");
        dic_SceneNames.Add(1, "KJH_OrbitScene");
        dic_SceneNames.Add(2, "KJH_EclipseScene");

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

    public void Load_EclipseScene()
    {
        if (SceneManager.GetActiveScene().name != "KJH_EclipseScene")
        {
            SceneManager.LoadScene("KJH_EclipseScene");
        }
    }

    public void LoadScene(string )
    {
        if (SceneManager.GetActiveScene().name != "KJH_EclipseScene")
        {
            SceneManager.LoadScene("KJH_EclipseScene");
        }
    }
}
