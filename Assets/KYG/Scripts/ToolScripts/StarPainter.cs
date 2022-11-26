using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
public class StarPainter : MonoBehaviourPun
{
    public GameObject lineFactory;
    public GameObject temporaryStarFactory;
    public GameObject ConstellationFactory;
    public TMP_InputField ConstellationNameInput;
    public GameObject StarDrawStartBtn;
    public GameObject StarDrawEndBtn;
    public GameObject Constellation;
    public GameObject ConstellationList;
    public GameObject ConstellationListUI;
    public StarLine starLine;
    public GameObject player;
    bool isDrawing = false;
    public string constellationName;
    GameObject star1;
    GameObject star2;


    // Start is called before the first frame update
    void Start()
    {
        StarDrawEndBtn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(isDrawing && EventSystem.current.IsPointerOverGameObject() == false) StarRay();
        if (ConstellationNameInput.GetComponent<TMP_InputField>().isFocused)
        {
            player.GetComponent<PlayerMove>().enabled = false;
        }
        else
        {
            player.GetComponent<PlayerMove>().enabled = true;
        }
    }
    GameObject temporaryStar;
    GameObject line;
    void StarRay()
    {
        Ray starRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        //RaycastHit starInfo;
        if (Input.GetButtonDown("Fire1"))
        {
            photonView.RPC(nameof(RPCOnBtnDown), RpcTarget.All, starRay.origin, starRay.direction);

            //if (Physics.Raycast(starRay, out starInfo))
            //{
            //    if (starInfo.transform.gameObject.GetComponent<Star>())
            //    {
            //        star1 = starInfo.transform.gameObject;
            //        star1.transform.parent = Constellation.transform;
            //        temporaryStar = Instantiate(temporaryStarFactory, Constellation.transform);
            //        temporaryStar.transform.position = starInfo.transform.position;
            //        line = Instantiate(lineFactory);
            //        starLine = line.GetComponent<StarLine>();
            //        line.transform.parent = Constellation.transform;
            //        starLine.star1 = star1;
            //        starLine.star2 = temporaryStar;
            //    }
            //}
        }
        if (Input.GetButton("Fire1"))
        {
            Vector3 lookDir = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)) - Camera.main.transform.position;
            lookDir.Normalize();
            Vector3 rayOrigin = Camera.main.transform.position + lookDir * GameManager.instance.celestialSphereRadius * 1.1f;
            Ray starDrawRay = new Ray(rayOrigin, -lookDir);
            RaycastHit starDrawInfo;
            if (Physics.Raycast(starDrawRay, out starDrawInfo))
            {
                photonView.RPC(nameof(RPCOnBtn), RpcTarget.All, starDrawInfo.point);
            }
            //temporaryStar.transform.position =Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane));
        }
        if (Input.GetButtonUp("Fire1"))
        {
            photonView.RPC(nameof(RPCOnBtnUp), RpcTarget.All, starRay.origin, starRay.direction);
            //if (Physics.Raycast(starRay, out starInfo))
            //{
            //    if (starInfo.transform.gameObject.GetComponent<Star>())
            //    {
            //        star2 = starInfo.transform.gameObject;
            //        star2.transform.parent = Constellation.transform;
            //        starLine.star2 = star2;                    
            //    }
            //}
            //Destroy(temporaryStar);
        }
    }

    [PunRPC]
    void RPCOnBtnDown(Vector3 origin, Vector3 forward)
    {
        Ray starRay = new Ray(origin, forward);
        RaycastHit starInfo; 
        if (Physics.Raycast(starRay, out starInfo))
        {
            if (starInfo.transform.gameObject.GetComponent<Star>())
            {
                star1 = starInfo.transform.gameObject;
                star1.transform.parent = Constellation.transform;
                temporaryStar = Instantiate(temporaryStarFactory, Constellation.transform);
                temporaryStar.transform.position = starInfo.transform.position;
                line = Instantiate(lineFactory);
                starLine = line.GetComponent<StarLine>();
                line.transform.parent = Constellation.transform;
                starLine.star1 = star1;
                starLine.star2 = temporaryStar;
            }
        }
    }

    [PunRPC]
    void RPCOnBtn(Vector3 starDrawInfo)
    {
        temporaryStar.transform.position = starDrawInfo;
    }

    [PunRPC]
    void RPCOnBtnUp(Vector3 origin, Vector3 forward)
    {
        Ray starRay = new Ray(origin, forward);
        RaycastHit starInfo;
        if (Physics.Raycast(starRay, out starInfo))
        {
            if (starInfo.transform.gameObject.GetComponent<Star>())
            {
                star2 = starInfo.transform.gameObject;
                star2.transform.parent = Constellation.transform;
                starLine.star2 = star2;
            }
        }
        Destroy(temporaryStar);
    }

    public void OnDrawStartBtn()
    {
        isDrawing = true;
        StarDrawStartBtn.SetActive(false);
        StarDrawEndBtn.SetActive(true);
        photonView.RPC("RPCDrawStart", RpcTarget.All, ConstellationNameInput.text);
    }
    [PunRPC]
    void RPCDrawStart(string name)
    {
        Constellation = Instantiate(ConstellationFactory);
        Constellation.transform.parent = GameManager.instance.CelestialSphere.transform;
        constellationName = name;
        Constellation.name = name;
        GameManager.instance.createdConstellationList[name] = Constellation;
    }
    public void OnDrawEndBtn()
    {
        photonView.RPC("RPCDrawEnd",RpcTarget.All);
        StarDrawStartBtn.SetActive(true);
        StarDrawEndBtn.SetActive(false);
        ConstellationNameInput.text = null;
        isDrawing = false;
    }
    [PunRPC]
    void RPCDrawEnd()
    {
        CreatedConstellationList createdConstellationList = ConstellationList.GetComponent<CreatedConstellationList>();
        createdConstellationList.Init(constellationName, Constellation);  
    }
    public void OnListBtn()
    {
        if (ConstellationListUI.activeSelf)
        {
            ConstellationListUI.SetActive(false);
        }
        else
        {
            ConstellationListUI.SetActive(true);
        }
    }
    public void OnCloseBtn()
    {
        gameObject.SetActive(false);
    }
}
