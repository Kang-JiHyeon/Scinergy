using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StarPainter : MonoBehaviour
{
    public GameObject lineFactory;
    public GameObject ConstellationFactory;
    public TMP_InputField ConstellationNameInput;
    public GameObject StarDrawStartBtn;
    public GameObject StarDrawEndBtn;
    public GameObject Constellation;
    public GameObject ConstellationList;
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

        if(isDrawing) StarRay();
        if (ConstellationNameInput.GetComponent<TMP_InputField>().isFocused)
        {
            player.GetComponent<PlayerMove>().enabled = false;
        }
        else
        {
            player.GetComponent<PlayerMove>().enabled = true;
        }
    }
    void StarRay()
    {
        Ray starRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit starInfo;
        
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(starRay, out starInfo))
            {
                if (starInfo.transform.GetComponent<Star>())
                {
                    star1 = starInfo.transform.gameObject;
                    star1.transform.parent = Constellation.transform;
                }
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (Physics.Raycast(starRay, out starInfo))
            {
                if (starInfo.transform.GetComponent<Star>())
                {
                    GameObject line = Instantiate(lineFactory);
                    star2 = starInfo.transform.gameObject;
                    star2.transform.parent = Constellation.transform;
                    starLine = line.GetComponent<StarLine>();
                    line.transform.parent = Constellation.transform;
                    starLine.star1 = star1;
                    starLine.star2 = star2;
                }
            }
        }

    }
    public List<string> starName = new();
    public void OnDrawStartBtn()
    {
        isDrawing = true;
        StarDrawStartBtn.SetActive(false);
        StarDrawEndBtn.SetActive(true);
        Constellation = Instantiate(ConstellationFactory);
        Constellation.transform.parent = GameManager.instance.CelestialSphere.transform;
        constellationName = ConstellationNameInput.text;
        Constellation.name = constellationName;
        GameManager.instance.createdConstellationList[constellationName] = Constellation;
        starName.Add(constellationName);
    }
    
    public void OnDrawEndBtn()
    {
        CreatedConstellationList createdConstellationList = ConstellationList.GetComponent<CreatedConstellationList>();
        createdConstellationList.Init(constellationName, Constellation);
        isDrawing = false;
        StarDrawStartBtn.SetActive(true);
        StarDrawEndBtn.SetActive(false);
        ConstellationNameInput.text = null;
    }
}
