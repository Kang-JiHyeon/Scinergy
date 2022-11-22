using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextDirecton : MonoBehaviour
{
    public TextMeshProUGUI starName;
    public TextMeshProUGUI ra;
    public TextMeshProUGUI dec;
    Star star; 
    private void Awake()
    {
        star = GetComponentInParent<Star>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        starName.text = star.starName;
        //float rah = float.Parse(star.ra.ToString().Split(".")[0]);
        //float ram = float.Parse(star.ra.ToString().Split(".")[1].Substring(0,2));
        //float ras = float.Parse(star.ra.ToString().Split(".")[1].Substring(2,2));
        //float rap = float.Parse(star.ra.ToString().Split(".")[1].Substring(4));

        //ra.text = "적경 : " + star.ra.ToString() + "h";
        //dec.text = "적위 : "+ star.dec.ToString() + "°";
    }

    // Update is called once per frame
    void Update()
    {      
        transform.forward = Camera.main.transform.forward;
        transform.position = star.transform.position + new Vector3(0, -50, 0);
    }
}
