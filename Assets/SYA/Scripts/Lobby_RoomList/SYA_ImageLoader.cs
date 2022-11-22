using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_ImageLoader : MonoBehaviour
{
    //이미지 정보를 출력하는 Panel
    [SerializeField]
    private GameObject panelLmageViewer;

    //파일이 나타내는 이미지 출력
    [SerializeField]
    private Image imageDrawTexture;
    //파일 이름, 해상도, 용량
    [SerializeField]
    private Text textFileData;

    //image ui 최대 크기
    private float maxwidth = 670;
    private float maxHeight = 385;

    //해당 파일내의 이미지 정보를 불러오는 곳
    public void OnLoad(FileInfo file)
    {
        //이미지 정보를 출력하는 Panel 활설화
        panelLmageViewer.SetActive(true);
        //파일로부터 bytes 데이터를 불러온다
        byte[] byteTexture = File.ReadAllBytes(file.FullName);
        //위의 텍스쳐에서 바이트 배열 정보를 바탕으로 Texture2D 이미지 파일 데이터 생성
        Texture2D texture2D = new Texture2D(0, 0);
        if(byteTexture.Length>0)//정보가 있으면->받아온 값이 있으면
        {
            texture2D.LoadImage(byteTexture);
        }

        //출력하는 이미지의 이미지UI의 크기 설정
        //원본이 최대 보다 크면
        //비율에 맞추어 줄여주기
        if(texture2D.width > maxwidth)
        {
            imageDrawTexture.rectTransform.sizeDelta = new Vector2(maxwidth, maxwidth / texture2D.width * texture2D.height);
        }
        else if(texture2D.height > maxHeight)
        { 
            imageDrawTexture.rectTransform.sizeDelta = new Vector2(maxHeight / texture2D.height * texture2D.width, maxHeight);
        }
        else
        {
            imageDrawTexture.rectTransform.sizeDelta = new Vector2(maxwidth, maxHeight);
        }

        //텍스쳐 2디를 스프라이트 형태로 바꾸어 주기
        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        //imageDrawTexture 이미지 UI에 보여지는 이미지를 위의 스프라이트로
        imageDrawTexture.sprite = sprite;

        //이미지 파일 정보 출력
        textFileData.text = $"{file.Name} ({texture2D.width} * {texture2D.height})";
    }

    public void OffLoad()
    {
        panelLmageViewer.SetActive(false);
    }
}
