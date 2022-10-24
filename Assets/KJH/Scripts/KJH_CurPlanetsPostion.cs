using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 행성의 현재 위치를 저장하고 싶다.
// - 처음 실행했을 때
// - 관측 시간이 현재 시간과 다를 때=
public class KJH_CurPlanetsPostion : MonoBehaviour
{
    // 현재 시간 위치를 저장하는 리스트
    public List<Transform> curPlanetsPositions;
    public Transform go_planets;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < go_planets.childCount; i++)
        {
            curPlanetsPositions.Add(go_planets.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurPosition()
    {
        for(int i=0; i<curPlanetsPositions.Count; i++)
        {
            KJH_SolarSystem.instance.planets[i].position = curPlanetsPositions[i].position;
        }
    }
}
