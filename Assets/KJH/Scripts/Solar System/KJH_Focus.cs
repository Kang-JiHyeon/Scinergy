using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��Ŀ�� ��� = ���콺�� Ŭ���� �༺
// ��Ŀ�� �� �� focus �� ��Ÿ���� �ϰ� �ʹ�.
// ��Ŀ�� �� ���콺�� ��ġ�� �༺�� ���� �Ÿ� �־����� ũ�Ⱑ Ŀ���� �ϰ� �ʹ�.
// focus�� ī�޶� �׻� �ٶ󺸰� �ϰ� �ʹ�. (v)
public class KJH_Focus : MonoBehaviour
{
    Transform focus;
    public float speed = 2f;
    public bool isFocus = false;
    // Start is called before the first frame update
    void Start()
    {
        focus = transform.GetChild(0);
        focus.localScale = Vector3.zero;

        //StopAllCoroutines();
        //StartCoroutine(IeChangeFocusScale(0.3f));

    }

    // Update is called once per frame
    void Update()
    {
        focus.forward = Camera.main.transform.forward;
    }

    // �Ű������� �� ���� ���������� �����صΰ�
    // ������������ �ٽ� ���� �Ű������� ���� ��
    // ������ return
    // �ٸ��� stop and coroutine

    float originTargetScale = -1f;
    public void ChangeFocusScale(float targetScale)
    {
        if(originTargetScale != targetScale)
        {
            StopCoroutine(IeChangeFocusScale(targetScale));
            StartCoroutine(IeChangeFocusScale(targetScale));

            originTargetScale = targetScale;
        }
    }
    IEnumerator IeChangeFocusScale(float targetScale)
    {
        //isCoroutine = true;
        Vector3 scale = Vector3.one * targetScale;
        while (Mathf.Abs(focus.localScale.x - targetScale) > 0.005f)
        {
            focus.localScale = Vector3.Lerp(focus.localScale, scale, speed * Time.deltaTime);
            yield return null;
        }
        focus.localScale = scale;
    }
}