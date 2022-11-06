using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �༺�� ���� ��ġ�� �����ϰ� �ʹ�.
// - ó�� �������� ��
// - ���� �ð��� ���� �ð��� �ٸ� ��=
public class KJH_CurPlanetsPostion : MonoBehaviour
{
    // ���� �ð� ��ġ�� �����ϴ� ����Ʈ
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
