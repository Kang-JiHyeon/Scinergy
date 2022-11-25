using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// * 일식 월식
// 카메라 카메라 시점 변환
// - 일식 : y축 -90
// - 월식 : y축 90

// 카메라 위치 표시
public class KJH_EclipseState : MonoBehaviour
{
    public List<Material> skyBoxs = new List<Material>();
    public Transform insideCamera;
    public KJH_MoonColor moonColor;
    public float earthScale = 3f;
    public bool isChangeEarthScale = false;

    float camRotY = 270f;
    float targetY = 270f;
    bool isRot = false;

    [Header("[ Button ]")]
    public GameObject eclipseType;
    public List<Image> image_eclipseBtns;
    public List<Sprite> sprite_normals;
    public List<Sprite> sprite_clicks;


    public static KJH_EclipseState instance; 
    public EclipseState state = EclipseState.Solar;

    public enum EclipseState
    {
        Solar,
        Lunar
    }

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

    // 일식 버튼 눌렀을 때
    public void OnClick_SolarEclipse()
    {
        state = EclipseState.Solar;
        RenderSettings.skybox = skyBoxs[0];

        if (!targetY.Equals(270))
            isRot = true;

        targetY = 270f;

        earthScale = 5f;
        isChangeEarthScale = true;
        moonColor.earthShadow.SetActive(false);
        moonColor.colorChangeSpeed = 0.3f;

        // 일식 버튼 click 상태
        image_eclipseBtns[0].sprite = sprite_clicks[0];

        // 월식 버튼 nomal 상태
        image_eclipseBtns[1].sprite = sprite_normals[1];

    }

    // 월식 버튼
    public void OnClick_LunarEclipse()
    {
        state = EclipseState.Lunar;
        RenderSettings.skybox = skyBoxs[1];

        if (!targetY.Equals(90f))
            isRot = true;

        targetY = 90f;

        earthScale = 3f;
        isChangeEarthScale = true;
        moonColor.earthShadow.SetActive(true);
        moonColor.colorChangeSpeed = 1f;

        // 일식 버튼 normal 상태
        image_eclipseBtns[0].sprite = sprite_normals[0];

        // 월식 버튼 click  상태
        image_eclipseBtns[1].sprite = sprite_clicks[1];
    }

    public void OnClick_EclipseType()
    {
        // main 버튼의 sprite 변경
        KJH_SpaceSceneManager.instance.loadSceneIndex = 2;
        KJH_SpaceSceneManager.instance.ChangeButtonSprite();

        // 일식월식 버튼 활성화 상태 
        image_eclipseBtns[(int)state].sprite = eclipseType.activeSelf ? sprite_clicks[(int)state] : sprite_normals[(int)state];

    }
}
