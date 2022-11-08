using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_ChatItem : MonoBehaviour
{
    //�ؽ�Ʈ ������Ʈ
    XRText chatText;
    //��ƮƮ������ ������Ʈ
    RectTransform rt;
    float preferredH;

    //�ԷµǴ� �ؽ�Ʈ�� ������� ����������
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

    //�ؽ�Ʈ���ð� �ؽ�Ʈ ������ ũ�⿡ �°� �ڽ��� �������������
    public void SetText(string s)
    {
        chatText.text = s;
        //chatText.text�� ũ�⿡ �°� ����Ʈ������ ����
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
