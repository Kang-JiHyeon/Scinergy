using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SYA_Loading : MonoBehaviour
{
    public static SYA_Loading Instance;
    public bool setActive;
    string nextScene="";
    public GameObject loading;

    private void Awake()
    {
        Instance = this;
        //loading.SetActive(false);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == nextScene)
        {
            loading.SetActive(false);
        }
    }

    public void OnLoading(string nextScene_)
    {
        loading.SetActive(true);
        nextScene = nextScene_;
    }
}