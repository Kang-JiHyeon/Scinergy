using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_DontDestroy : MonoBehaviour
{
    public bool setActive;
    private void Awake()
    {
        gameObject.SetActive(setActive);
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
    }
}
