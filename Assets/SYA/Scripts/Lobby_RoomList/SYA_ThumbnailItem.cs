using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SYA_ThumbnailItem : MonoBehaviour
{
    //���� ����Ʈ �迭
    byte[] byteTexture;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClickThumbnail);
    }

    //�̹����� Ŭ���ϸ� �̹����� �迭�� ��´�
    public void OnClickThumbnail()
    {
        transform.parent.
            transform.parent.
            transform.parent.
            transform.parent.GetComponent<SYA_Thumbnail>().thumbnail.texture = GetComponent<RawImage>().texture;
    }
}
