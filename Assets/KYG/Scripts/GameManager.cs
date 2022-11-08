using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //천구
    public GameObject CelestialSpehere;
    //천구 반지름
    public float celestialSphereRadius;

    public Dictionary<string, GameObject> createdStarList = new ();
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
        Cursor.visible = false;
        //플레이어를 생성한다.
        PhotonNetwork.Instantiate("KYG_Player", Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
