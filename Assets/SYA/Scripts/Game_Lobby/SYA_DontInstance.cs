using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_DontInstance : MonoBehaviour
{
    public static SYA_DontInstance Instance;


    private void Awake()
    {
        //if (!photonView.IsMine) return;
        if (Instance == null)
        {
            //인스턴스에 나를 넣고
            Instance = this;
            //나를 씬이 전환이 되어도 파괴되지 않게 하겠다

            DontDestroyOnLoad(gameObject);
        }
        //그렇지 않으면
        else
        {
            Destroy(gameObject);
        }
    }
}
