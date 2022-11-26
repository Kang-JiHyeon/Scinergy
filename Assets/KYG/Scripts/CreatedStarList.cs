using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class CreatedStarList : MonoBehaviourPun
{
    public Transform CreatedStarListContent;
    public GameObject CreatedStarItemFactory;
    public GameObject SelectedStar;
    public TextMeshProUGUI SelectedStarName;
    public GameObject SelectedStarItem;
    public GameObject Player;
    public GameObject CreatedStarListUI;
    public GameObject createdConstellationList;
    // Start is called before the first frame update
    void Start()
    {
        //Init();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(string starname, GameObject starInfo)
    {
        GameObject createdStarItem = Instantiate(CreatedStarItemFactory, CreatedStarListContent);
        CreatedStarItem item = createdStarItem.GetComponent<CreatedStarItem>();
        item.star = starInfo;
        item.starName.text = starname;
    }
    public void OnStarLookBtn()
    {
        Player.GetComponent<PlayerRot>().StarSet(SelectedStar.transform.position);
    }
    
    public void OnStarDeleteBtn()
    {
        Star deletingStar = GameManager.instance.createdStarList[SelectedStar.GetComponent<Star>().starName].GetComponent<Star>();
        deletingStar.randX = UnityEngine.Random.Range(-90f, 90f);
        deletingStar.randY = UnityEngine.Random.Range(-20f, -30f);
        photonView.RPC("RPCStarDelete", RpcTarget.All, SelectedStar.GetComponent<Star>().starName,deletingStar.randX, deletingStar.randY);           
        Destroy(SelectedStarItem);
        SelectedStarItem = null;
    }
    
    [PunRPC]
    void RPCStarDelete(string SelectedStarName, float randX, float randY)
    {
        Star RPCStar = GameManager.instance.createdStarList[SelectedStarName].GetComponent<Star>();
        RPCStar.randX = randX;
        RPCStar.randY = randY;
        RPCStar.StarState = Star.State.shootingStar;
        GameManager.instance.createdStarList.Remove(SelectedStarName);
    }
    public void OnDeleteAllStar()
    {
        foreach (KeyValuePair<string, GameObject> star in GameManager.instance.createdStarList)
        {
            star.Value.GetComponent<Star>().StarState = Star.State.shootingStar;
        }
        Transform[] createdStarItemList = CreatedStarListContent.GetComponentsInChildren<Transform>();
        if(createdStarItemList != null)
        {
            for(int i = 1; i < createdStarItemList.Length; i++)
            {
                if (createdStarItemList[i] != transform) Destroy(createdStarItemList[i].gameObject);
            }
        }
        GameManager.instance.createdStarList.Clear();
    }

    public void OnCloseBtn()
    {
        CreatedStarListUI.SetActive(false);
    }
}
