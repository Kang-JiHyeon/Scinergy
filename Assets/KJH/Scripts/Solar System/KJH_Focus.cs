using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 포커스 대상 = 마우스로 클릭한 행성
// 포커스 할 때 focus 를 나타나게 하고 싶다.
// 포커스 후 마우스의 위치가 행성과 일정 거리 멀어지면 크기가 커지게 하고 싶다.
// focus가 카메라를 항상 바라보게 하고 싶다. (v)
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