using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYG_AudioManager : MonoBehaviour
{
    public static KYG_AudioManager instance;
    public AudioSource backgroundMusic;
    public AudioSource clickSound;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (backgroundMusic != null)
            backgroundMusic.volume = SYA_UI.SYA_UIManager.Instance.exBG;
        if(clickSound !=null)
            clickSound.volume = SYA_UI.SYA_UIManager.Instance.exEF;
    }
}
