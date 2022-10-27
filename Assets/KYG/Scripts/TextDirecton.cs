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
    // Start is called before the first frame update
    void Start()
    {
        starName.text = GetComponentInParent<Star>().starName;
        ra.text ="적경 : "+ GetComponentInParent<Star>().ra.ToString();
        dec.text = "적위 : "+ GetComponentInParent<Star>().dec.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
        transform.position = GetComponentInParent<Star>().transform.position + new Vector3(100, 0, 0);
    }
}
