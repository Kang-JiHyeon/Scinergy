using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//파일의 속성 (폴더, 파일) - 아이콘 출력용
public enum DataType {Diretory=0, File }

public class SYA_Data : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    //아이콘에 적용할 수 있는 스프라이트 아이콘
    [SerializeField]
    private Sprite[] spriteIcon;


    //파일의 속성에 따라 아이콘 출력
    private Image imageIcon;
    //파일의 이름 출력
    private Text textDataName;

    //파일의 속성
    private DataType dataType;

    //파일 이름
    private string fileName;
    //외부에서 확인하기 위한 GET프로퍼티
    public string FileName => fileName;

    //파일 이름의 최대 길이
    private int maxFileNameLength = 20;

    private SYA_DirectoryController directoryController;

    //받아온 데이터 정보로 데이터 설정
    public void SetUp(SYA_DirectoryController controller ,string fileName, DataType dataType)
    {
        directoryController = controller;
        //만약 판넬데이터의 오브젝트의 이미지 컴포넌트가 삭제되지 않았다면 판넬데이터 겟
        imageIcon = GetComponentInChildren<Image>();
        textDataName = GetComponentInChildren<Text>();

        this.fileName = fileName;
        this.dataType = dataType;

        //아이콘 이미지 설정
        imageIcon.sprite = spriteIcon[(int)this.dataType];

        //파일 이름 출력
        textDataName.text = this.fileName;
        //파일 이름의 최대 길이 maxFileNameLength를 넘어가면 이름의 뒷부분 자르고 "..."추가
        if(fileName.Length>=maxFileNameLength)
        {
            textDataName.text = fileName.Substring(0, maxFileNameLength);
            textDataName.text += "...";
        }

        //파일이름 색상설정
        SetTextColor();
    }

    private void SetTextColor()
    {
        if(dataType == DataType.Diretory)
        {
            textDataName.color = Color.blue;
        }
        else
        {
            textDataName.color = Color.black;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textDataName.color = Color.red;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        directoryController.UpdateInput(fileName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetTextColor();
    }
}
