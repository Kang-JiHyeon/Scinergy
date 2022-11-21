using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KJH_MoonColor : MonoBehaviour
{
    public KJH_ShadowLine shadowLine;
    public Transform earth;
    public Material mat_moon;
    public Material mat_shadow;

    Color targetMoonColor;
    Color targetShadowColor;
    float changeSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        earth = transform.parent.parent;
        targetMoonColor = Color.white;
        targetShadowColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        // ����
        if(KJH_EclipseState.instance.state == KJH_EclipseState.EclipseState.Lunar)
        {
            if (shadowLine.isRootShadow)
            {
                // ���� ���� ���׸��ڿ� ������ ���� ����
                if (Mathf.Abs(earth.position.z - transform.position.z) < 1.5f)
                {
                    targetMoonColor = new Color(0.6f, 0.3f, 0.3f, 1);
                    targetShadowColor.a = 0f;

                }
                // ���� ���� ���׸��ڿ� �Ϻθ� ���� ����
                else
                {
                    targetMoonColor = new Color(0.6f, 0.5f, 0.5f, 1);
                    targetShadowColor.a = 1f;
                }
            }
            // ���� �ݱ׸��� -> ��¦ ��ο�����
            else if (shadowLine.isPenumbralShadow)
            {
                targetMoonColor = new Color(0.6f, 0.6f, 0.6f, 1);
                targetShadowColor.a = 0f;
            }
            else
            {
                targetMoonColor = Color.white;
                targetShadowColor.a = 0f;
            }
        }
        // �Ͻ�
        else
        {
            targetMoonColor = Color.black;
            targetShadowColor.a = 0f;
        }

        mat_moon.color = Color.Lerp(mat_moon.color, targetMoonColor, Time.deltaTime * changeSpeed);
        mat_shadow.color = Color.Lerp(mat_shadow.color, targetShadowColor, Time.deltaTime * 3);
    }
}
