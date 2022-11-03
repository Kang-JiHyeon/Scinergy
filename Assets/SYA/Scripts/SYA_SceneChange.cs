using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SYA_SceneChange : MonoBehaviour
{
    public static SYA_SceneChange Instance;

    private void Awake()
    {
        Instance = this;
    }

    float currentTime = 0;
    public float fadeTime = 2;

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "LoadingScene")
        {
            currentTime += Time.deltaTime;
            if (currentTime >= fadeTime)
            {
                currentTime = 0;
                StartScene();
            }
        }
    }
    
    public void LoadScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void StartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void LoginScene()
    {
        SceneManager.LoadScene("LoginScene");
    }

    public void NameSettingScene()
    {
        SceneManager.LoadScene("NameSettingScene");
    }

    public void AvartaScene()
    {
        SceneManager.LoadScene("AvartaScene");
    }

    public void PlayScene()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void SymposiumScene()
    {
        SceneManager.LoadScene("SymposiumScene");
    }

    public void SymposiumRoomMake()
    {
        SceneManager.LoadScene("SYA_SymposiumRoomMake");
    }

    public void Quit()
    {
        Application.Quit();
        print("Á¾·á");
    }
}
