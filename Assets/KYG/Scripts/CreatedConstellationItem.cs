using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CreatedConstellationItem : MonoBehaviour
{
    public GameObject constellation;
    public TextMeshProUGUI constellationName;
    // Start is called before the first frame update
    void Start()
    {
        constellationName.text = constellation.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        GetComponentInParent<CreatedConstellationList>().SelectedConstellation = constellation;
        GetComponentInParent<CreatedConstellationList>().SelectedConstellationName.text = "   선택된 별자리 :  " + constellation.name;
        GetComponentInParent<CreatedConstellationList>().SelectedConstellationItem = gameObject;
    }
}
