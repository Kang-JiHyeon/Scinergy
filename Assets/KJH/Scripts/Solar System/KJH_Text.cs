using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KJH_Text : Text
{
    // 크기가 변경되었을 때 호출되는 함수
    public Action onChangedSize;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (onChangedSize != null)
        {
            onChangedSize();
        }
    }
    public override void CalculateLayoutInputVertical()
    {
        base.CalculateLayoutInputVertical();

        if (onChangedSize != null)
        {
            onChangedSize();
        }
    }
}
