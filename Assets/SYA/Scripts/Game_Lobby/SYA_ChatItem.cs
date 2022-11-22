using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_ChatItem : MonoBehaviour
{
    //텍스트 컴포넌트
    XRText chatText;
    //렉트트렌스폼 컴포넌트
    RectTransform rt;
    float preferredH;

    //입력되는 텍스트를 기반으로 사이즈조절
    // Start is called before the first frame update
    void Awake()
    {
        chatText = GetComponent<XRText>();
        chatText.onChangedSize = OnChangedTextSize;
        rt = GetComponent<RectTransform>();
    }

    private void Update()
    {

    }

    //텍스트셋팅과 텍스트 내용의 크기에 맞게 자신의 컨텐츠사이즈변경
    public void SetText(string s)
    {
        chatText.text = s;
        //chatText.text의 크기에 맞게 컨텐트사이즈 변경
    }

    void OnChangedTextSize()
    {
        if (preferredH != chatText.preferredHeight)
        {
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, chatText.preferredHeight);
            preferredH = chatText.preferredHeight;
        }
    }
}
