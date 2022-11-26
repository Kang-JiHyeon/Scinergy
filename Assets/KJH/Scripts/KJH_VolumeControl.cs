using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KJH_VolumeControl : MonoBehaviour
{
    AudioSource audio_bg;
    AudioSource audio_click;

    // Start is called before the first frame update
    void Start()
    {
        audio_bg = GetComponent<AudioSource>();
        audio_click = GameObject.Find("Canvas").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audio_bg)
            audio_bg.volume = SYA_UI.SYA_UIManager.Instance.exBG;

        if(audio_click)
            audio_click.volume = SYA_UI.SYA_UIManager.Instance.exEF;
        else
            audio_click = GameObject.Find("Canvas").GetComponent<AudioSource>();
    }
}
