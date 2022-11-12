using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_TitleScene : MonoBehaviour
{
    //시간이 흐르고
    //2초가 지나면 문구가 등장한다
    //문구가 등장한 후 아무런 키를 누르면 다음 장면으로 넘어간다
    //현재시간 나타날 시간 문구

    float currentTime = 0;
    float appearTime = 2;
    public GameObject anyKeyStr;
    Text text;

    enum State
    {
        //등장 
        appear,
        //문구 fade
        fadeIn,
        fadeOut
    }

    State state = State.appear;

    // Start is called before the first frame update
    void Start()
    {
        text = anyKeyStr.GetComponent<Text>();
        anyKeyStr.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Color color = text.color;
        switch (state)
        {
            case State.appear:
                currentTime += Time.deltaTime;
                if (appearTime < currentTime)
                {
                    currentTime = 0;
                    anyKeyStr.SetActive(true);
                    state = State.fadeIn;
                }
                break;
            case State.fadeIn:
                color.a += Time.deltaTime * 0.5f;
                if (color.a >= 0.9f) state = State.fadeOut;
                break;
            case State.fadeOut:
                color.a -= Time.deltaTime * 0.5f;
                if (color.a <= 0) state = State.fadeIn;
                break;
        }
        text.color = color;

        if (anyKeyStr.activeSelf)
        {
            if (Input.anyKeyDown)
            {
                SYA_SceneChange.Instance.AvartaScene();

            }
        }
    }
}
