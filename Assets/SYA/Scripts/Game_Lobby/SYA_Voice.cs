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

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SYA_PlayerCompo.Instance.PlayerDestroy += OndestroyGo;
        
    }

    public void OndestroyGo()
    {
        Destroy(gameObject);
    }
}
