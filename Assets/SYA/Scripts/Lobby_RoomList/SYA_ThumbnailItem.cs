using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SYA_ThumbnailItem : MonoBehaviour
{
    //받을 바이트 배열
    byte[] byteTexture;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClickThumbnail);
    }

    //이미지를 클릭하며 이미지의 배열을 얻는다
    public void OnClickThumbnail()
    {
        transform.parent.
            transform.parent.
            transform.parent.
            transform.parent.GetComponent<SYA_Thumbnail>().thumbnail.texture = GetComponent<RawImage>().texture;
    }
}
