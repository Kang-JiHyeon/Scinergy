using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StarGenerator : MonoBehaviour
{
    public static StarGenerator instance;
    #region Input
    public TMP_InputField starNameInput;
    public TMP_InputField decInput;
    public TMP_InputField raInput;
    public TMP_InputField starAmount;
    public TMP_Dropdown typeDropdown;
    public TMP_Dropdown brightnessDropdown;
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
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        typeDropdown.onValueChanged.AddListener(OnTypeDropDownEvent);
        brightnessDropdown.onValueChanged.AddListener(OnBrightnessDropDownEvent);
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
        GameManager.instance.createdStarList[starName] = star;
        star.GetComponent<Star>().InfoSet(starName, ra, dec,starType, brightness);
        player.GetComponent<PlayerRot>().StarSet(star.transform.position);
        createdStarList.Init(starName, star);
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
            star.GetComponent<Star>().InfoSet(starName, ra, dec, starTypeList[0], brightness);
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
