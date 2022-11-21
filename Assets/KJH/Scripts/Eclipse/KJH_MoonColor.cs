using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KJH_MoonColor : MonoBehaviour
{
    public KJH_ShadowLine shadowLine;
    public Transform sun;
    public Transform earth;
    public Material mat_moon;
    public Material mat_shadow;
    public Image image_solarLight;

    Color color_targetMoon;
    Color color_targetShadow;
    Color color_solarLight;

    float changeSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        earth = transform.parent.parent;
        color_targetMoon = Color.white;
        color_targetShadow = Color.white;
        color_solarLight = new Color(1, 1, 1, 0f);
        image_solarLight.color = color_solarLight;
    }

    // Update is called once per frame
    void Update()
    {
        ////Vector3 dir = transform.position - earth.position;
        //Vector3 cross = Vector3.Cross(Vector3.right, transform.position - earth.position);

        // 월식
        if(KJH_EclipseState.instance.state == KJH_EclipseState.EclipseState.Lunar)
        {
            if (shadowLine.isRootShadow)
            {
                // 달이 지구 본그림자에 완전히 들어가는 현상
                if (Mathf.Abs(earth.position.z - transform.position.z) < 1.5f)
                {
                    color_targetMoon = new Color(0.6f, 0.3f, 0.3f, 1);
                }
                // 달이 지구 본그림자에 일부만 들어가는 현상
                else
                {
                    color_targetMoon = new Color(0.6f, 0.5f, 0.5f, 1);
                    color_targetShadow.a = 0.3f;
                }
            }
            // 지구 반그림자 -> 살짝 어두워지게
            else if (shadowLine.isPenumbralShadow)
            {
                color_targetMoon = new Color(0.6f, 0.6f, 0.6f, 1);
                color_targetShadow.a = 0.8f;
            }
            else
            {
                color_targetMoon = Color.white;
                color_targetShadow.a = 0.8f;
            }
        }
        // 일식
        // 태양과의 거리에 따라 이미지의 alpha값을 조절한다.
        else if(KJH_EclipseState.instance.state == KJH_EclipseState.EclipseState.Solar)
        {
            color_targetMoon = new Color(0.8f, 0.8f, 0.8f, 1);
            color_targetShadow.a = 0f;

            float z = Mathf.Abs(transform.position.z);
            if (z < 3f && transform.position.x < earth.position.x)
            {
                color_solarLight.a = (1 - z / 3) * 0.8f;
            }
            else
            {
                color_solarLight.a = 0f;
            }

            image_solarLight.color = color_solarLight;
        }

        mat_moon.color = Color.Lerp(mat_moon.color, color_targetMoon, Time.deltaTime * changeSpeed);
        mat_shadow.color = Color.Lerp(mat_shadow.color, color_targetShadow, Time.deltaTime * 3);

    }
}
