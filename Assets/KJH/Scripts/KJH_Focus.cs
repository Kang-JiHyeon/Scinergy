using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 포커스 대상 = 마우스로 클릭한 행성
// 포커스 할 때 focus 를 나타나게 하고 싶다.
// 포커스 후 마우스의 위치가 행성과 일정 거리 멀어지면 크기가 커지게 하고 싶다.
// focus가 카메라를 항상 바라보게 하고 싶다. (v)
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

    // 매개변수로 온 값을 전역변수에 저장해두고
    // 전역변수값과 다시 들어온 매개변수의 값과 비교
    // 같으면 return
    // 다르면 stop and coroutine

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