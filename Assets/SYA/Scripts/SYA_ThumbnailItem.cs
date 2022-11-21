using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_ThumbnailItem : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClickThumbnail);
    }

    

    //이미지를 클릭하며 이미지의 스프라이트를 받아와, 썸네일로 지정한다
    public void OnClickThumbnail()
    {
        Sprite sp = GetComponent<Image>().sprite;
        GetComponentInParent<SYA_Thumbnail>().thumbnail.sprite = sp;
    }
}
