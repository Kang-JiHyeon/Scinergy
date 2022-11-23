using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Eclipse Scene�� UI �Լ�
public class KJH_EclipseUI : MonoBehaviour
{
    public GameObject eclipseType;
    public GameObject LoadSceneAlert;
    public List<GameObject> LoadSceneAlerts;


    public void OnClick_Eclipse()
    {
        eclipseType.SetActive(!eclipseType.activeSelf);
    }

    public void OnClick_LoadScene_Yes()
    {
        KJH_SpaceSceneManager.instance.Load_SolarSystemScene();
    }

    public void OnClick_LoadScene_No()
    {
        LoadSceneAlert.transform.gameObject.SetActive(false);
    }
}