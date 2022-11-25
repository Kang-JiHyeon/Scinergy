using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KJH_DontDestroyObject : MonoBehaviour
{
    private void Awake()
    {
        var obj = FindObjectsOfType<KJH_DontDestroyObject>();

        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 로비씬이나 일월식 씬이면 파괴한다.
        if (SceneManager.GetActiveScene().name.Contains("SYA") || SceneManager.GetActiveScene().name.Contains("Eclipse"))
        {
            Destroy(gameObject);
        }
    }
}
