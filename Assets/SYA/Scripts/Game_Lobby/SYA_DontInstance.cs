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
            //�ν��Ͻ��� ���� �ְ�
            Instance = this;
            //���� ���� ��ȯ�� �Ǿ �ı����� �ʰ� �ϰڴ�

            DontDestroyOnLoad(gameObject);
        }
        //�׷��� ������
        else
        {
            Destroy(gameObject);
        }
    }
}
