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

    

    //�̹����� Ŭ���ϸ� �̹����� ��������Ʈ�� �޾ƿ�, ����Ϸ� �����Ѵ�
    public void OnClickThumbnail()
    {
        Sprite sp = GetComponent<Image>().sprite;
        GetComponentInParent<SYA_Thumbnail>().thumbnail.sprite = sp;
    }
}
