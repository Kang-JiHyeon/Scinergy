using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolBtn : MonoBehaviour
{
    public List<Sprite> btnImage = new();
    public GameObject targetObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject.activeSelf)
        {
            GetComponent<Image>().sprite = btnImage[0];
        }
        else
        {
            GetComponent<Image>().sprite = btnImage[1];
        }
    }
    public void OnActive()
    {
        
    }
}
