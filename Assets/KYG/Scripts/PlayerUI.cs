using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    bool compasActive = true;
    public GameObject compas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCompasBtn()
    {
        //compasActive = !compasActive;
        if (compas.activeSelf)
        {
            compas.SetActive(false);
        }
        else
        {
            compas.SetActive(true);
        }
    }
}
