using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 기본 버튼들을 제어하고 싶다.
public class KJH_DefalutButtons : MonoBehaviour
{
    //public List<Button> buttons;
    public GameObject customUI;
    public Button btn_custom;
    public List<Sprite> btn_sprites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick_SolarSystem()
    {
        KJH_SpaceSceneManager.instance.isSolar = true;
        KJH_SpaceSceneManager.instance.Load_SolarSystemScene();
    }

    public void OnClick_ControlTime()
    {
        KJH_SpaceSceneManager.instance.Load_SolarSystemScene();
    }



    public void OnClick_Custom()
    {
        customUI.SetActive(!customUI.activeSelf);

        if (customUI.activeSelf)
        {
            btn_custom.image.sprite = btn_sprites[1];
        }
        else
        {
            btn_custom.image.sprite = btn_sprites[0];
        }

    }

    public void OnClickClose()
    {
        customUI.SetActive(false);
        btn_custom.image.sprite = btn_sprites[0];
    }


}
