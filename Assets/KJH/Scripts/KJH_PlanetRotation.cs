using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 행성을 자전축(나) 기준 반시계 방향으로 회전하고 싶다.
// z 기울기를 지정하고 싶다.
public class KJH_PlanetRotation : MonoBehaviour
{
    public Transform sunLight;      // 태양 빛
    public Transform axis;          //자전축
    public Transform planet;        // 행성
    public float axialTilt = 0f;    // 자전축 기울기
    public float rotAngle = 1f;     // 자전 정도

    // Start is called before the first frame update
    void Start()
    {
        axis.localRotation = Quaternion.Euler(0, 0, axialTilt);
    }

    // Update is called once per frame
    void Update()
    {
        // 자전축 기울기
        axialTilt = Mathf.Clamp(axialTilt, 22.5f, 24.5f);
        axis.localRotation = Quaternion.Euler(-axialTilt, 0, 0);

        // 행성 자전
        planet.Rotate(0, -rotAngle, 0);

        // 태양 빛이 항상 지구를 보도록
        if (sunLight)
        {
            sunLight.LookAt(transform.position);
        }
    }
}
