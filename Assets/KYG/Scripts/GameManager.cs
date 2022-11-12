using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //õ��
    public GameObject CelestialSphere;
    //õ�� ������
    public float celestialSphereRadius;

    public Dictionary<string, GameObject> createdStarList = new ();

    public Dictionary<string, GameObject> createdConstellationList = new();
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
        //Cursor.visible = false;
        //�÷��̾ �����Ѵ�.
        CelestialSphere.GetComponent<SphereCollider>().radius = celestialSphereRadius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
