using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_Voice : MonoBehaviour
{
    public static SYA_Voice Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DestroyOnGo()
    {
        Destroy(gameObject);
    }
}
