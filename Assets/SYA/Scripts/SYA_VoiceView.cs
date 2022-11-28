using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Voice;
using Photon.Voice.PUN;

public class SYA_VoiceView : MonoBehaviour
{
    public AudioSource audioSource;
    public PhotonVoiceView voiceView;
    bool start;
    private void Start()
    {
        /*voiceView = GetComponent<PhotonVoiceView>();
        voiceView.enabled = false;*/
        voiceView.enabled = true;
        voiceView.RecorderInUse = SYA_Voice.Instance.recorder;
    }

    private void Update()
    {
        if (voiceView.RecorderInUse==null)
        {
            voiceView.RecorderInUse = SYA_Voice.Instance.recorder;
        }
    }

    [PunRPC]
    public void RPCOnMic(bool micing, string name)
    {
        audioSource.enabled = micing;
    }

}
