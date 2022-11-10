using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CreatedStarItem : MonoBehaviour
{
    public GameObject star;
    public TextMeshProUGUI starName;

    // Start is called before the first frame update
    void Start()
    {
        starName.text = star.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        GetComponentInParent<CreatedStarList>().SelectedStar = star;
        GetComponentInParent<CreatedStarList>().SelectedStarName.text = "   선택된 별 :  " + star.name;
        GetComponentInParent<CreatedStarList>().SelectedStarItem = gameObject;
    }
    
}
