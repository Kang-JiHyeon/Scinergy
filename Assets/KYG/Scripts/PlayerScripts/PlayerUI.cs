using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class PlayerUI : MonoBehaviour
{
    public GameObject starGenerator;
    public GameObject starList;
    public GameObject telescope;
    public GameObject constellation;
    public GameObject compas;
    public GameObject Clock;
    public GameObject Map;
    public GameObject manual;
    public List<Button> masterBtn = new();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (!SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].IsMine) gameObject.SetActive(false);

            if (SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName] != "Owner")
            {
                for (int i = 0; i < masterBtn.Count; i++)
                {
                    masterBtn[i].interactable = false;
                }

            }
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
    public void OnStarGeneratorBtn()
    {
        if (starGenerator.activeSelf)
        {
            starGenerator.SetActive(false);
        }
        else
        {
            starGenerator.SetActive(true);
        }
    }
    public void OnStarListBtn()
    {
        if (starList.activeSelf)
        {
            starList.SetActive(false);
        }
        else
        {
            starList.SetActive(true);
        }
    }

    public void OnTelescopeBtn()
    {
        if (telescope.activeSelf)
        {
            telescope.GetComponent<Telescope>().TelescopeOff();
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
            telescope.SetActive(true);
        }
    }

    public void OnConstellationBtn()
    {
        if (constellation.activeSelf)
        {
            constellation.SetActive(false);
        }
        else
        {
            constellation.SetActive(true);
        }
    }
    public void OnClockBtn()
    {
        if (Clock.activeSelf)
        {
            Clock.SetActive(false);
        }
        else
        {
            Clock.SetActive(true);
        }
    }
    public void OnMapBtn()
    {
        if (Map.activeSelf)
        {
            Map.SetActive(false);
        }
        else
        {
            Map.SetActive(true);
        }
    }
    public void OnManualBtn()
    {
        if (manual.activeSelf)
        {
            manual.SetActive(false);
        }
        else
        {
            manual.SetActive(true);
        }
    }
    public void OnManualClose()
    {
        manual.SetActive(false);
    }
}
