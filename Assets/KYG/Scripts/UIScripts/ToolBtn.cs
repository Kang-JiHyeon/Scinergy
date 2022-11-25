using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ToolBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<Sprite> btnImage = new();
    public GameObject targetObject;
    public AudioSource clickSound;
    // Start is called before the first frame update
    void Start()
    {
        clickSound = KYG_AudioManager.instance.clickSound;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject.activeSelf)
        {
            GetComponent<Image>().sprite = btnImage[0];
        }
        //else
        //{
        //    GetComponent<Image>().sprite = btnImage[1];
        //}

    }
    public void OnActive()
    {
        clickSound.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = btnImage[0];
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = btnImage[1];
    }
}
