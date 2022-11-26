using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KJH_SpaceSceneManager : MonoBehaviour
{
    public static KJH_SpaceSceneManager instance;
    public bool isSolar = false;
    public bool isTime = false;

    public List<GameObject> go_options;
    string loadSceneName = "";
    [SerializeField]
    public int buttonIndex = 3;

    [Header("Button")]
    public List<Image> image_mainBtns;
    public List<Sprite> sprite_nomals;
    public List<Sprite> sprite_clicks;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 옵션 리스트
        Transform option = transform.Find("UI_Option");

        //if (!option) return;

        for(int i = 0; i<option.childCount; i++)
        {
            go_options.Add(option.GetChild(i).gameObject);
        }

        // 메인 버튼
        Transform mainBtn = transform.Find("UI_Main");

        //if (!mainBtn) return;

        for (int i = 0; i < mainBtn.childCount; i++)
        {
            image_mainBtns.Add(mainBtn.GetChild(i).GetComponent<Image>());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick_SolarSystemScene()
    {
        loadSceneName = "KJH_SolarSystemScene";
        buttonIndex = 0;
        //go_options[0].SetActive(!go_options[0].activeSelf);
        //go_options[1].SetActive(false);
        ChangeButtonSprite();

    }

    public void OnClick_CustomScene()
    {
        loadSceneName = "KJH_OrbitScene";
        buttonIndex = 1;
        //go_options[0].SetActive(false);
        //go_options[1].SetActive(!go_options[1].activeSelf);

        print(go_options);
        ChangeButtonSprite();
    }

    public void OnClick_EclipseScene()
    {
        loadSceneName = "KJH_EclipseScene";
        buttonIndex = 2;
        //go_options[2].SetActive(!go_options[2].activeSelf);

        ChangeButtonSprite();
    }

    public void OnClick_Guide()
    {
        buttonIndex = 3;
        ChangeButtonSprite();

    }


    public void OnClick_LoadScene_Yes()
    {
        if (SceneManager.GetActiveScene().name != loadSceneName)
        {
            SceneManager.LoadScene(loadSceneName);
        }
    }
    
    public void OnClick_LoadScene_No()
    {
        //go_options[loadSceneIndex].transform.gameObject.SetActive(false);
        ChangeButtonSprite();
    }

    public void OnClick_Close()
    {
        go_options[buttonIndex].SetActive(false);
        image_mainBtns[buttonIndex].sprite = sprite_nomals[buttonIndex];
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

    public void LoadScene(string sceneName)
    {
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void ChangeButtonSprite()
    {
        // 옵션의 활성화 상태 변경
        for(int i=0; i<go_options.Count; i++)
        {
            if(i == buttonIndex) go_options[buttonIndex].SetActive(!go_options[buttonIndex].activeSelf);
            else go_options[i].SetActive(false);
        }

        // loadSceneIndex 이외의 버튼의 sprite는 normal로 설정
        for(int i=0; i<image_mainBtns.Count; i++)
        {
            if (i == buttonIndex && go_options[buttonIndex].activeSelf)
                image_mainBtns[buttonIndex].sprite = sprite_clicks[buttonIndex];
            else 
                image_mainBtns[i].sprite = sprite_nomals[i]; 
        }
    }
}
