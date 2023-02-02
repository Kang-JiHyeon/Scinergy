using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KJH_DataItem : MonoBehaviour
{
    public KJH_Text infoText;
    RectTransform rt;
    float preferdHeight;

    // Start is called before the first frame update
    void Start()
    {
        infoText = GetComponent<KJH_Text>();
        rt = GetComponent<RectTransform>();
        infoText.onChangedSize = OnChangedTextSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Text 설정, Text 내용의 크기에 맞게 자신의 ContentSize를 변경
    public void SetText(string s)
    {
        infoText.text = s;
    }

    // 크기가 변경되면 prefer의 크기를 변경시키고 싶다.
    void OnChangedTextSize()
    {
        if (preferdHeight != infoText.preferredHeight)
        {
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, infoText.preferredHeight);
            preferdHeight = infoText.preferredHeight;
        }
    }
}
