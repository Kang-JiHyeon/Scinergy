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

    // Text ����, Text ������ ũ�⿡ �°� �ڽ��� ContentSize�� ����
    public void SetText(string s)
    {
        infoText.text = s;
    }

    // ũ�Ⱑ ����Ǹ� prefer�� ũ�⸦ �����Ű�� �ʹ�.
    void OnChangedTextSize()
    {
        if (preferdHeight != infoText.preferredHeight)
        {
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, infoText.preferredHeight);
            preferdHeight = infoText.preferredHeight;
        }
    }
}
