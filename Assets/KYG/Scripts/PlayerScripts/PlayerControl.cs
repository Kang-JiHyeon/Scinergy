using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerControl : MonoBehaviour
{
    bool visible = false;
    public GameObject playerUI;
    //스타트일 때 한 번만 실행
    bool start;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<PhotonView>().IsMine && SceneManager.GetActiveScene().name == "KYG_Scene")
        {
            if(start)
            {
                playerUI.SetActive(true);
                playerUI.GetComponent<PlayerUI>().manual.SetActive(true);
                start = false;
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                visible = !visible;
                Cursor.visible = visible;
                playerUI.SetActive(visible);
            }
        }
        else
        {

        }
    }
}
