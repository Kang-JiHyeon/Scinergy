using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Voice;
using Photon.Voice.PUN;
using Photon.Voice.Unity;

public class SYA_Voice : MonoBehaviourPun
{
    public static SYA_Voice Instance;
    public Recorder recorder;
    int currMicIdx;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SYA_PlayerCompo.Instance.PlayerDestroy += OndestroyGo;
        
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            currMicIdx++;
            currMicIdx %= Microphone.devices.Length;
            recorder.PhotonMicrophoneDeviceId = currMicIdx;
            DeviceInfo info = new DeviceInfo(currMicIdx, Microphone.devices[currMicIdx]);
            recorder.MicrophoneDevice = info;
            ////Recorder - Microphone Type 이 Unity or Photon인지에 따라 밑에 둘중 하나..
            ////recorder.UnityMicrophoneDevice = Microphone.devices[currMicIdx];
            //print(recorder.PhotonMicrophoneDeviceId + ", " + currMicIdx);
            recorder.RestartRecording(true);
        }

    }*/

    public void OndestroyGo()
    {
        Destroy(gameObject);
    }
}
