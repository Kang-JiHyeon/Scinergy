using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 태양을 중심으로 행성들을 회전시키고 싶다.

public class KJH_SolarSystem : MonoBehaviour
{
    public List<Transform> planets;
    public List<float> periods;
    public List<float> AUs;
    public List<float> radius;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<planets.Count; i++)
        {
            Vector3 pos = transform.position + new Vector3(AUs[i] * 15, 0, 0);
            planets[i].position = pos;

            planets[i].localScale = planets[i].localScale * radius[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i<planets.Count; i++)
        {
            planets[i].RotateAround(transform.position, -transform.up, periods[i] * 0.001f);
            planets[i].forward = -transform.right;
            //planets[i].Rotate(-transform.up, periods[i] * 0.01f);
        }
    }
}
