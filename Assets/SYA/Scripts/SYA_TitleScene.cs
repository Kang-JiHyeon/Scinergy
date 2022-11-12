using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_TitleScene : MonoBehaviour
{
    //�ð��� �帣��
    //2�ʰ� ������ ������ �����Ѵ�
    //������ ������ �� �ƹ��� Ű�� ������ ���� ������� �Ѿ��
    //����ð� ��Ÿ�� �ð� ����

    float currentTime = 0;
    float appearTime = 2;
    public GameObject anyKeyStr;
    Text text;

    enum State
    {
        //���� 
        appear,
        //���� fade
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
