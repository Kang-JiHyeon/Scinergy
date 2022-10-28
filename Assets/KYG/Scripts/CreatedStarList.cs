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
    }
    public void OnStarLookBtn()
    {
        GameObject.Find("Player").GetComponent<PlayerRot>().StarSet(SelectedStar.transform.position);
    }
    public void OnStarDeleteBtn()
    {
        GameManager.instance.createdStarList.Remove(SelectedStar.GetComponent<Star>().starName);
        Destroy(SelectedStar);
        Destroy(SelectedStarItem);
    }
}
