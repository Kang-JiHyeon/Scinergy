using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedStarList : MonoBehaviour
{
    public Transform CreatedStarListContent;
    public GameObject CreatedStarItemFactory;
    public GameObject SelectedStar;
    public GameObject SelectedStarItem;
    public GameObject Player;
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
        GameManager.instance.createdStarList.Remove(SelectedStar.GetComponent<Star>().starName);
        SelectedStar.GetComponent<Star>().StarState = Star.State.shootingStar;
        Destroy(SelectedStarItem);
        SelectedStarItem = null;
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
}
