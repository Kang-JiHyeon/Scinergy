using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class StarGenerator : MonoBehaviour
{
    public static StarGenerator instance;
    #region Input
    public TMP_InputField starNameInput;
    public TMP_Dropdown generateTypeDropdown;
    public TMP_InputField decInput;
    public TMP_InputField raInput;
    public TMP_InputField starAmount;
    public TMP_Dropdown typeDropdown;
    public TMP_Dropdown brightnessDropdown;
    public Button generateBtn;
    public List<GameObject> starTypeList = new List<GameObject>();
    public List<GameObject> starBrightnessList = new List<GameObject>();
    #endregion

 
    //별 이름
    public string starName;
    //적경
    public float ra;
    //적위
    public float dec;
    //별 종류
    public GameObject starType;
    //별 밝기
    public GameObject brightness;

    public GameObject player;

    public GameObject starList;

    public GameObject testObject;

    int generateTypeNumber = 0;

    public bool drawStar;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        typeDropdown.onValueChanged.AddListener(OnTypeDropDownEvent);
        brightnessDropdown.onValueChanged.AddListener(OnBrightnessDropDownEvent);
        generateTypeDropdown.onValueChanged.AddListener(OnGenerateTypeDropDownEvent);
        typeDropdown.ClearOptions();
        brightnessDropdown.ClearOptions();
    }
    // Start is called before the first frame update
    void Start()
    {
        typeDropdownSet();
        brightnessDropDownSet();
    }

    // Update is called once per frame
    void Update()
    {
        if (starNameInput.GetComponent<TMP_InputField>().isFocused)
        {
            player.GetComponent<PlayerMove>().enabled = false;
        }
        else
        {
            player.GetComponent<PlayerMove>().enabled = true;
        }
        if(starNameInput.text !="") starName = starNameInput.text;
        if (decInput.text != "") dec = float.Parse(decInput.text);
        if (raInput.text != "") ra = float.Parse(raInput.text);
        if (drawStar && EventSystem.current.IsPointerOverGameObject() == false) DrawStar();
        if (starNameInput.text == ""|| generateTypeDropdown.value == 0 || raInput == null || decInput == null || typeDropdown.value == 0 || brightnessDropdown.value ==0)
        {
            generateBtn.interactable = false;
        }
        else
        {
            generateBtn.interactable = true;
        }
    }
    int starNumber = 1;
    public void DrawStar()
    {
        Vector3 lookDir = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)) - Camera.main.transform.position;
        lookDir.Normalize();
        Vector3 rayOrigin = Camera.main.transform.position + lookDir * GameManager.instance.celestialSphereRadius * 1.1f;
        //Debug.DrawRay(rayOrigin, -lookDir * 1000, Color.red);

        Ray starDrawRay = new Ray(rayOrigin, -lookDir);
        RaycastHit starDrawInfo;

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(starDrawRay, out starDrawInfo))
            {
                if (starDrawInfo.collider.name == "CelestialSphere")
                {
                    Vector3 shoot = starDrawRay.direction;
                    shoot.y = 0;
                    CreatedStarList createdStarList = starList.GetComponent<CreatedStarList>();
                    GameObject star = Instantiate(starType);
                    if (GameManager.instance.createdStarList.ContainsKey(starName))
                    {
                        starName += ( " ("+starNumber+")");
                        starNumber++;
                    }
                    else
                    {
                        starNumber = 1;
                    }
                    GameManager.instance.createdStarList[starName] = star;
                    dec = Mathf.Asin(starDrawInfo.point.y / GameManager.instance.celestialSphereRadius);
                    ra = Mathf.Acos(starDrawInfo.point.z / (GameManager.instance.celestialSphereRadius * Mathf.Cos(dec)));
                    if ((Vector3.Cross(Vector3.forward, shoot).normalized - Vector3.up).magnitude > 0.5f)
                    {
                        ra *= -1;
                    }

                    dec *= 180 / Mathf.PI;
                    ra *= 180 / Mathf.PI;
                    ra /= 15f;
                    star.GetComponent<Star>().InfoSet(starName, ra, dec, starType, brightness, generateTypeNumber);
                    //player.GetComponent<PlayerRot>().StarSet(star.transform.position);
                    createdStarList.Init(starName, star);
                }
            }
        }
    }
    public void typeDropdownSet()
    {
        List<TMP_Dropdown.OptionData> typeOptionList = new List<TMP_Dropdown.OptionData>();
        typeOptionList.Add(new TMP_Dropdown.OptionData("생성하실 별의 종류를 선택하세요"));
        foreach (GameObject type in starTypeList)
        {
            typeOptionList.Add(new TMP_Dropdown.OptionData(type.gameObject.name));
        }
        typeDropdown.AddOptions(typeOptionList);
        typeDropdown.value = 0;
    }

    public void brightnessDropDownSet()
    {
        List<TMP_Dropdown.OptionData> brightnessOptionList = new List<TMP_Dropdown.OptionData>();
        brightnessOptionList.Add(new TMP_Dropdown.OptionData("생성하실 별의 밝기를 선택하세요"));
        foreach (GameObject brightness in starBrightnessList)
        {
            brightnessOptionList.Add(new TMP_Dropdown.OptionData(brightness.gameObject.name));
        }
        brightnessDropdown.AddOptions(brightnessOptionList);
        brightnessDropdown.value = 0;
    }

    public void OnGenerateTypeDropDownEvent(int index)
    {
        generateTypeNumber = index;
        if (generateTypeNumber == 1)
        {
            decInput.transform.gameObject.SetActive(true);
            raInput.transform.gameObject.SetActive(true);
            generateBtn.interactable = true;
            drawStar = false;
        }
        if (generateTypeNumber == 2)
        {
            decInput.transform.gameObject.SetActive(false);
            raInput.transform.gameObject.SetActive(false);
            generateBtn.interactable = false;
            drawStar = true;
        }
    }
    public void OnTypeDropDownEvent(int index)
    {
        starType = starTypeList[index - 1];
    }
    public void OnBrightnessDropDownEvent(int index)
    {
        brightness = starBrightnessList[index - 1];
    }
    public void OnGenerateStarBtn()
    {
        CreatedStarList createdStarList = starList.GetComponent<CreatedStarList>();
        GameObject star = Instantiate(starType);
        if (GameManager.instance.createdStarList.ContainsKey(starName))
        {
            starName += (" (" + starNumber + ")");
            starNumber++;
        }
        else
        {
            starNumber = 1;
        }
        GameManager.instance.createdStarList[starName] = star;
        star.GetComponent<Star>().InfoSet(starName, ra, dec,starType, brightness, generateTypeNumber);
        player.GetComponent<PlayerRot>().StarSet(star.transform.position);
        createdStarList.Init(starName, star);
        starNameInput.text = null;
        starName = null;
        raInput.text = null;
        decInput.text = null;
    }

    public void OnRandomGenerateBtn()
    {
        for(int i = 0; i<int.Parse(starAmount.text); i++)
        {
            CreatedStarList createdStarList = starList.GetComponent<CreatedStarList>();
            GameObject star = Instantiate(starTypeList[0]);
            starName = "Star" + i;
            GameManager.instance.createdStarList[starName] = star;
            ra = Random.Range(0f, 25f);
            dec = Random.Range(-90f, 91f);
            brightness = starBrightnessList[Random.Range(1, starBrightnessList.Count)];
            generateTypeNumber = 1;
            star.GetComponent<Star>().InfoSet(starName, ra, dec, starTypeList[0], brightness,generateTypeNumber);
            createdStarList.Init(starName, star);
        }
    }
    public void OnStarListBtn()
    {
        if (starList.activeSelf)
        {
            starList.SetActive(false);
        }
        else
        {
            starList.SetActive(true);
        }
    }
}
