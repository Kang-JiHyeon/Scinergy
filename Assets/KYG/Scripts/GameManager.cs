using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //천구
    public GameObject CelestialSphere;
    //천구 반지름
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
        //플레이어를 생성한다.
        CelestialSphere.GetComponent<SphereCollider>().radius = celestialSphereRadius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
