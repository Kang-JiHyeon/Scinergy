using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KJH_UITest : MonoBehaviour
{
    public GameObject sprite;
    public GameObject textBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // up
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            iTween.MoveTo(sprite, iTween.Hash("y", 0, "time", 0.5f, "islocal", true, "movetopath", false, "easetype", iTween.EaseType.easeInCubic));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            iTween.MoveTo(sprite, iTween.Hash("y", -65, "time", 0.5f, "islocal", true, "movetopath", false, "easetype", iTween.EaseType.easeOutCubic));
        }

        


    }

    // 마우스가 버튼 위에 올라갔을 때 이벤트 발생
    public void OnEnter()
    {
        textBox.SetActive(true);
        iTween.MoveTo(sprite, iTween.Hash("y", 0, "time", 0.5f, "islocal", true, "movetopath", false, "easetype", iTween.EaseType.easeInCubic));
        //iTween.MoveTo(textBox, iTween.Hash("y", 50, "time", 0.5f, "islocal", true, "movetopath", false, "easetype", iTween.EaseType.easeInCubic, "delay", .1f));
        iTween.ScaleTo(textBox, iTween.Hash("scale", new Vector3(0.2f, 0.4f, 0), "delay", .1f));
    }

    public void OnExit()
    {
        //textBox.SetActive(false);
        iTween.MoveTo(sprite, iTween.Hash("y", -50, "time", 0.5f, "islocal", true, "movetopath", false, "easetype", iTween.EaseType.easeInCubic));
        iTween.ScaleTo(textBox, iTween.Hash("scale", Vector3.zero));
    }

    public void OnClick()
    {
        textBox.SetActive(false);
        sprite.SetActive(false);
        print("클릭됨");
    }
}
