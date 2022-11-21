using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// * 일식 월식
// 카메라 카메라 시점 변환
// - 일식 : y축 -90
// - 월식 : y축 90

// 카메라 위치 표시
public class KJH_EclipseState : MonoBehaviour
{
    public Light eclipseLight; 
    public Transform insideCamera;
    float camRotY = 270f;
    float targetY = 270f;
    bool isRot = false;

    public List<Material> skyBoxs = new List<Material>();
    public enum EclipseState
    {
        Solar,
        Lunar
    }
    public static KJH_EclipseState instance; 

    public EclipseState state = EclipseState.Solar;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        insideCamera.localRotation = Quaternion.Euler(0, 270f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRot)
        {
            CameraRotate();
        }
    }
    // 일식 버튼
    public void OnClick_SolarEclipse()
    {
        state = EclipseState.Solar;
        RenderSettings.skybox = skyBoxs[0];
        //eclipseLight.intensity = 1f;

        if (!targetY.Equals(270))
            isRot = true;

        targetY = 270f;
    }

    // 월식 버튼
    public void OnClick_LunarEclipse()
    {
        state = EclipseState.Lunar;
        RenderSettings.skybox = skyBoxs[1];

        //eclipseLight.intensity = 0f;

        if (!targetY.Equals(90f))
            isRot = true;

        targetY = 90f;
    }

    void CameraRotate()
    {
        camRotY = Mathf.Lerp(camRotY, targetY, Time.deltaTime);

        if (Mathf.Abs(targetY - camRotY) < 0.1f)
        {
            insideCamera.localRotation = Quaternion.Euler(0, targetY, 0);
            isRot = false;
            return;
        }
        insideCamera.localRotation = Quaternion.Euler(0, camRotY, 0);
    }
}
