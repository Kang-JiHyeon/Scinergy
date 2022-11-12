using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_DontDestroy : MonoBehaviour
{
    public bool setActive;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(setActive);
    }
    private void Start()
    {
        
    }
}
