using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// * �Ͻ� ����
// ī�޶� ī�޶� ���� ��ȯ
// - �Ͻ� : y�� -90
// - ���� : y�� 90

// ī�޶� ��ġ ǥ��
public class KJH_EclipseUI : MonoBehaviour
{
    public Light eclipseLight; 
    public Transform insideCamera;
    float camRotY = -90f;
    float targetY = 0f;
    bool isRot = false;

    // Start is called before the first frame update
    void Start()
    {
        insideCamera.localRotation = Quaternion.Euler(0, -90f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRot)
        {
            CameraRotate();
        }
    }
    // �Ͻ� ��ư
    public void OnClick_SunEclipse()
    {
        eclipseLight.intensity = 1f;
        isRot = true;
        targetY = 270f;
    }

    // ���� ��ư
    public void OnClick_MoonEclipse()
    {
        eclipseLight.intensity = 0f;
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
