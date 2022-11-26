using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UI;

public class SYA_Thumbnail : MonoBehaviour
{
    // 이미지 적용할 곳
    public RawImage thumbnail;
    //이미지 배열 저장하는 곳
    byte[] byteTexture;
    //이미지 스프라이트 소스, 목록
    public List<Texture2D> thumbnails = new List<Texture2D>();
    //생성되는 곳 부모
    public Transform content;
    //생성할 아이템
    public GameObject item;
    //완료 버튼 누르면 창닫기

    public void OnClickTh()
    {
        gameObject.SetActive(false);
    }

// Start is called before the first frame update
    void Start()
    {
        SetTh();
    }

    //소스리스트대로 변경
    void SetTh()
    {
        for(int i=0; i<thumbnails.Count; ++i)
        {
            GameObject go = Instantiate(item, content);
            go.GetComponent<RawImage>().texture = thumbnails[i];
        }
    }

    public GameObject fileGo;
    //버튼을 누르면 폴더 창이 열린다
    public void OnClickFile()
    {
        fileGo.SetActive(!fileGo.activeSelf);
    }

    /*    // Update is called once per frame
        void Update()
        {

        }*/
}
