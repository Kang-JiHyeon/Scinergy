using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// * �Ͻ� ����
// ī�޶� ī�޶� ���� ��ȯ
// - �Ͻ� : y�� -90
// - ���� : y�� 90

// ī�޶� ��ġ ǥ��
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

    // �Ͻ� ��ư ������ ��
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

        // �Ͻ� ��ư click ����
        image_eclipseBtns[0].sprite = sprite_clicks[0];

        // ���� ��ư nomal ����
        image_eclipseBtns[1].sprite = sprite_normals[1];

    }

    // ���� ��ư
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

        // �Ͻ� ��ư normal ����
        image_eclipseBtns[0].sprite = sprite_normals[0];

        // ���� ��ư click  ����
        image_eclipseBtns[1].sprite = sprite_clicks[1];
    }

    public void OnClick_EclipseType()
    {
        // main ��ư�� sprite ����
        KJH_SpaceSceneManager.instance.loadSceneIndex = 2;
        KJH_SpaceSceneManager.instance.ChangeButtonSprite();

        // �ϽĿ��� ��ư Ȱ��ȭ ���� 
        image_eclipseBtns[(int)state].sprite = eclipseType.activeSelf ? sprite_clicks[(int)state] : sprite_normals[(int)state];

    }
}
