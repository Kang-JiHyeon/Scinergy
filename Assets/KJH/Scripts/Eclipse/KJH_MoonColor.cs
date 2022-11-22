using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KJH_MoonColor : MonoBehaviour
{
    public KJH_ShadowLine shadowLine;
    public GameObject particle_sun;
    public Transform sun;
    public Transform earth;
    public Material mat_sun;
    public Material mat_moon;
    public Material mat_shadow;
    public Image image_solarLight;

    Color color_targetMoon;
    Color color_targetShadow;
    Color color_solarLight;
    Color color_sun;

    float changeSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        earth = transform.parent.parent;
        color_targetMoon = Color.white;
        color_targetShadow = Color.white;
        color_solarLight = new Color(1, 1, 1, 0f);
        image_solarLight.color = color_solarLight;
        particle_sun.SetActive(false);
        mat_sun = sun.GetComponent<SpriteRenderer>().material;

        //color_sun = mat_sun.color;

        mat_sun.SetColor("_EmissionColor", mat_sun.color * 10f);
    }

    // Update is called once per frame
    void Update()
    {
        ////Vector3 dir = transform.position - earth.position;
        //Vector3 cross = Vector3.Cross(Vector3.right, transform.position - earth.position);

        // ����
        if(KJH_EclipseState.instance.state == KJH_EclipseState.EclipseState.Lunar)
        {
            if (shadowLine.isRootShadow)
            {
                // ���� ���� ���׸��ڿ� ������ ���� ����
                if (Mathf.Abs(earth.position.z - transform.position.z) < 1.5f)
                {
                    color_targetMoon = new Color(0.6f, 0.3f, 0.3f, 1);
                }
                // ���� ���� ���׸��ڿ� �Ϻθ� ���� ����
                else
                {
                    color_targetMoon = new Color(0.6f, 0.5f, 0.5f, 1);
                    color_targetShadow.a = 0.3f;
                }
            }
            // ���� �ݱ׸��� -> ��¦ ��ο�����
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
        // �Ͻ�
        // �¾���� �Ÿ��� ���� �̹����� alpha���� �����Ѵ�.
        else if(KJH_EclipseState.instance.state == KJH_EclipseState.EclipseState.Solar)
        {
            color_targetMoon = new Color(0.8f, 0.8f, 0.8f, 1);
            color_targetShadow.a = 0f;

            float z = Mathf.Abs(transform.position.z);

            // �¾�-���� ���̿� ���� ��
            if(transform.position.x < earth.position.x)
            {
                // �����Ͻ��� �� sun ũ������, ��ƼŬ ������Ʈ Ȱ��ȭ
                if(z < 0.2f)
                {
                    sun.localScale = Vector3.one * 0.13f;
                    particle_sun.SetActive(true);
                }
                else
                {
                    sun.localScale = Vector3.one * 0.1f;
                    particle_sun.SetActive(false);
                }
            

                // �Ͻ��� ����ɼ��� �ؽ����� alpha���� �������Ѽ� ��ο����̰� ��
                if (z < 3f)
                {
                    color_solarLight.a = (1 - z / 3) * 0.8f;
                }
                else
                {
                    color_solarLight.a = 0f;
                }

                image_solarLight.color = color_solarLight;
            }

        }

        mat_moon.color = Color.Lerp(mat_moon.color, color_targetMoon, Time.deltaTime * changeSpeed);
        mat_shadow.color = Color.Lerp(mat_shadow.color, color_targetShadow, Time.deltaTime * 3);

    }
}
