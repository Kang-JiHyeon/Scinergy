using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_PlayerDestroy_Obj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SYA_PlayerCompo.Instance.PlayerDestroy += DestroyOnGo;
    }

    public void DestroyOnGo()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        SYA_PlayerCompo.Instance.PlayerDestroy -= DestroyOnGo;
    }
}
