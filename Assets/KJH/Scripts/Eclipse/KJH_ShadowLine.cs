using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 태양의 경계 좌표
// 달의 경계 좌표


// 태양->달 방향, 그 방향으로 ray를 쏴서 지구가 hit되면 line을 그림
// 태양 위 -> 달 위
// 태양 위 -> 달 아래
// 태양 아래 -> 달 위
// 태양 아래 -> 달 아래


public class KJH_ShadowLine : MonoBehaviour
{
    public Transform earth;
    public Transform moon;

    public List<Vector3> positions = new List<Vector3>();
    public LineRenderer[] lrs;
    Vector3[] sunPoss = new Vector3[2];
    Vector3[] earthPoss = new Vector3[2];
    Vector3[] moonPoss = new Vector3[2];

    public bool isRootShadow = false;
    public bool isPenumbralShadow = false;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        earth = GameObject.Find("Earth_Space").transform;
        moon = GameObject.Find("Moon").transform;
        lrs = transform.GetComponentsInChildren<LineRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        sunPoss[0] = transform.position + new Vector3(0, 0, transform.localScale.x);
        sunPoss[1] = transform.position + new Vector3(0, 0, -transform.localScale.x);
        moonPoss[0] = moon.position + new Vector3(0, 0, moon.localScale.x);
        moonPoss[1] = moon.position + new Vector3(0, 0, -moon.localScale.x);
        earthPoss[0] = earth.position + new Vector3(0, 0, earth.localScale.x);
        earthPoss[1] = earth.position + new Vector3(0, 0, -earth.localScale.x);

        switch (KJH_EclipseState.instance.state)
        {
            case KJH_EclipseState.EclipseState.Solar:
                DrawSolarEclipseLine();
                break;
            case KJH_EclipseState.EclipseState.Lunar:
                DrawLunarEclipseLine();
                Cross();
                break;
        }
    }


    // 일식
    void DrawSolarEclipseLine()
    {
        for (int i = 0; i < sunPoss.Length; i++)
        {
            for (int j = 0; j < moonPoss.Length; j++)
            {
                positions.Clear();
                Vector3 dir = moonPoss[j] - sunPoss[i];
                RaycastHit hitInfo;

                if (Physics.Raycast(moonPoss[j], dir, out hitInfo))
                {
                    if (hitInfo.transform.name == "Earth_Space")
                    {
                        positions.Add(sunPoss[i]);
                        positions.Add(moonPoss[j]);
                        positions.Add(hitInfo.point);
                    }
                }
                // 그림자 라인 그리기
                lrs[index].positionCount = positions.Count;
                lrs[index].SetPositions(positions.ToArray());
                index++;
                index %= lrs.Length;
            }
        }
    }
    void DrawLunarEclipseLine()
    {
        for (int i = 0; i < sunPoss.Length; i++)
        {
            for (int j = 0; j < earthPoss.Length; j++)
            {
                positions.Clear();

                Vector3 dir = earthPoss[j] - sunPoss[i];

                // 그림자 라인 그리기
                positions.Add(sunPoss[i]);
                positions.Add(sunPoss[i] + dir.normalized * 40f);

                lrs[index].positionCount = positions.Count;
                lrs[index].SetPositions(positions.ToArray());
                index++;
                index %= lrs.Length;
            }
        }
    }
    Vector3[] dirs = new Vector3[4];
    Vector3[] crosss = new Vector3[4];
    void Cross()
    {
        Vector3 dir1 = earthPoss[0] - sunPoss[0];
        Vector3 cross1 = Vector3.Cross(dir1, moon.position - sunPoss[0]);

        Vector3 dir2 = earthPoss[1] - sunPoss[1];
        Vector3 cross2 = Vector3.Cross(dir2, moon.position - sunPoss[1]);

        Vector3 dir3 = earthPoss[1] - sunPoss[0];
        Vector3 cross3 = Vector3.Cross(dir3, moon.position - sunPoss[0]);

        Vector3 dir4 = earthPoss[0] - sunPoss[1];
        Vector3 cross4 = Vector3.Cross(dir4, moon.position - sunPoss[1]);

        if(earth.position.x < moon.position.x)
        {
            if (cross1.y > 0 && cross2.y < 0 && earth.position.x < moon.position.x)
            {
                Debug.Log("본그림자 안에 들어옴");
                isRootShadow = true;
                isPenumbralShadow = false;
            }
            else if(cross3.y < 0 && cross4.y > 0)
            {
                Debug.Log("반그림자 안에 들어옴");
                isRootShadow = false;
                isPenumbralShadow = true;
            }
            else
            {
                isRootShadow = false;
                isPenumbralShadow = false;
            }
        }
    }

}
