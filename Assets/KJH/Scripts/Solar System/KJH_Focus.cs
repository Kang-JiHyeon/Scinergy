using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��Ŀ�� ��� = ���콺�� Ŭ���� �༺
// ��Ŀ�� �� �� focus �� ��Ÿ���� �ϰ� �ʹ�.
// ��Ŀ�� �� ���콺�� ��ġ�� �༺�� ���� �Ÿ� �־����� ũ�Ⱑ Ŀ���� �ϰ� �ʹ�.
// focus�� ī�޶� �׻� �ٶ󺸰� �ϰ� �ʹ�. (v)
public class KJH_Focus : MonoBehaviour
{
    Transform parent;
    Transform focus;
    public float speed = 4f;
    public bool isFocus = false;
    public float originTargetScale = 0f;
    public float targetScale = 0f;

    // Start is called before the first frame update
    void Start()
    {
        focus = transform.GetChild(0);
        focus.localScale = Vector3.zero;
        originTargetScale = focus.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        focus.forward = Camera.main.transform.forward;

        if (isFocus)
        {
            focus.localScale = Vector3.Lerp(focus.localScale, Vector3.one * targetScale, speed * Time.deltaTime);

            if(Vector3.Distance(focus.localScale, Vector3.one * targetScale) < 0.01f)
            {
                focus.localScale = Vector3.one * targetScale;
                isFocus = false;
            }
        }
    }
}