using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //õ��
    public GameObject CelestialSpehere;
    //õ�� ������
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
        //�÷��̾ �����Ѵ�.
        PhotonNetwork.Instantiate("KYG_Player", Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
