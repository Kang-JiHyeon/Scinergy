using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerControl : MonoBehaviour
{
    bool visible = false;
    public GameObject playerUI;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<PhotonView>().IsMine && SceneManager.GetActiveScene().name == "KYG_Scene")
        {
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
