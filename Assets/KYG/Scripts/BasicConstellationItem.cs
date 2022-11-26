using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BasicConstellationItem : MonoBehaviour
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
        if (GetComponentInParent<BasicConstellationList>().SelectedConstellation)
        {
            GetComponentInParent<BasicConstellationList>().SelectedConstellation.GetComponent<Constellation>().isSelected = false;
        }
        GetComponentInParent<BasicConstellationList>().SelectedConstellation = constellation;
        constellation.GetComponent<Constellation>().isSelected = true;
        GetComponentInParent<BasicConstellationList>().SelectedConstellationName.text = "   선택된 별자리 :  " + constellation.name;
        GetComponentInParent<BasicConstellationList>().SelectedConstellationItem = gameObject;
    }
}
