using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KJH_MoonColor : MonoBehaviour
{
    public KJH_ShadowLine shadow;
    public Material moonMat;
    public Transform earth;
    public Transform moon;
    // Start is called before the first frame update
    void Start()
    {
        //moon = transform.parent;
        earth = transform.parent.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if(KJH_EclipseState.instance.state == KJH_EclipseState.EclipseState.Moon)
        {
            if (shadow.isRootShadow)
            {
                // 달이 지구 본그림자에 완전히 들어가는 현상
                if (Mathf.Abs(earth.position.z - transform.position.z) < 1f)
                {
                    moonMat.color = Color.red;
                }
                // 달이 지구 본그림자에 일부만 들어가는 현상
                else
                {
                    moonMat.color = Color.gray;
                }
            }
            // 지구 반그림자 -> 살짝 어두워지게
            else if (shadow.isPenumbralShadow)
            {
                moonMat.color = new Color(0.6f, 0.6f, 0.4f, 1);
            }
            else
            {
                moonMat.color = Color.yellow;
            }
        }
        else
        {
            moonMat.color = Color.black;
        }
    }
}
