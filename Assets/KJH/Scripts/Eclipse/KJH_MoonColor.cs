using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KJH_MoonColor : MonoBehaviour
{
    public Transform camera_inside;

    public KJH_ShadowLine shadowLine;
    public GameObject earthShadow;
    public GameObject particle_sun;
    public Transform sun;
    public Transform earth;
    public Material mat_moon;
    public Material mat_shadow;

    Color color_targetMoon;
    Color color_targetShadow;

    public float colorChangeSpeed = 1f;
    float target_solarScale = 0.1f;
    float target_sunParticleScale = 0f;
    float target_skyboxExposure = 1f;
    float current_skyboxExposure = 1f;

    // Start is called before the first frame update
    void Start()
    {
        earth = transform.parent.parent;
        color_targetMoon = new Color(0.5f, 0.5f, 0.5f, 1);
        color_targetShadow = Color.white;
        color_targetShadow.a = 0f;
        colorChangeSpeed = 0.3f;
        RenderSettings.skybox.SetFloat("_Exposure", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        // ����
        if (KJH_EclipseState.instance.state == KJH_EclipseState.EclipseState.Lunar)
        {
            RenderSettings.skybox.SetFloat("_Exposure", 1f);
            if (shadowLine.isRootShadow)
            {
                // ���� ���� ���׸��ڿ� ������ ���� ����
                if (Mathf.Abs(earth.position.z - transform.position.z) < 2f)
                {
                    color_targetMoon = new Color(0.75f, 0.45f, 0.4f, 1);
                }
                // ���� ���� ���׸��ڿ� �Ϻθ� ���� ����
                else
                {
                    color_targetMoon = new Color(0.4f, 0.6f, 0.6f, 1);
                }
            }
            // ���� �ݱ׸��� -> ��¦ ��ο�����
            else if (shadowLine.isPenumbralShadow)
            {
                color_targetMoon = new Color(0.8f, 0.8f, 0.8f, 1);
            }
            else
            {
                color_targetMoon = Color.white;
            }

        }
        // �Ͻ�
        // �¾���� �Ÿ��� ���� �̹����� alpha���� �����Ѵ�.
        else if(KJH_EclipseState.instance.state == KJH_EclipseState.EclipseState.Solar)
        {
            color_targetMoon = new Color(0.5f, 0.5f, 0.5f, 1);
            float z = Mathf.Abs(transform.position.z);

            // �¾�-���� ���̿� ���� ��ġ�� ��
            if(transform.position.x < earth.position.x)
            {
                // �����Ͻ��� �� sun ũ������, ��ƼŬ ������Ʈ Ȱ��ȭ
                if(earth.position.x > 7)
                {
                    if(z < 0.4f) target_solarScale = 0.13f;
                    else if (z < 0.8f) target_sunParticleScale = 1f;
                }
                else
                {
                    target_sunParticleScale = 0f;   
                }

                // �¾� ũ�� ����
                sun.localScale = Vector3.Lerp(sun.localScale, Vector3.one * target_solarScale, Time.deltaTime);
                if (Mathf.Abs(sun.localScale.x - target_solarScale) < 0.001f)
                {
                    sun.localScale = Vector3.one * target_solarScale;
                }

                // �Ͻ� ��ƼŬ ũ�� ����
                particle_sun.transform.localScale = Vector3.Lerp(particle_sun.transform.localScale, Vector3.one * target_sunParticleScale, Time.deltaTime * 2);
                if (Mathf.Abs(particle_sun.transform.localScale.x - target_sunParticleScale) < 0.1f)
                {
                    particle_sun.transform.localScale = Vector3.one * target_sunParticleScale;
                }

                // �� ��ī�̹ڽ��� exposure�� ����
                if(z < 0.5f)
                {
                    target_skyboxExposure = earth.position.x > 7 ? 0.2f : 0.4f;
                }
                else if(z < 1)
                {
                    target_skyboxExposure = earth.position.x > 7 ? 0.6f : 0.8f;
                }
                else
                {
                    target_skyboxExposure = 1f;
                }
            }
            // ��ī�̹ڽ� exposure ����
            current_skyboxExposure = Mathf.Lerp(current_skyboxExposure, target_skyboxExposure, Time.deltaTime);

            if (Mathf.Abs(target_skyboxExposure - current_skyboxExposure) < 0.01f)
            {
                current_skyboxExposure = target_skyboxExposure;
            }
            RenderSettings.skybox.SetFloat("_Exposure", current_skyboxExposure);
        }
        mat_moon.color = Color.Lerp(mat_moon.color, color_targetMoon, Time.deltaTime * colorChangeSpeed);
    }

}
