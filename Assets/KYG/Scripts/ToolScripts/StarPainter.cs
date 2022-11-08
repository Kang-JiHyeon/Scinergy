using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StarPainter : MonoBehaviour
{
    public GameObject lineFactory;
    public GameObject ConstellationFactory;
    public TMP_InputField ConstellationName;
    public GameObject StarDrawStartBtn;
    public GameObject StarDrawEndBtn;
    public GameObject Constellation;
    public StarLine starLine;
    public GameObject player;
    bool isDrawing = false;
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
        if (ConstellationName.GetComponent<TMP_InputField>().isFocused)
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
                    print(starInfo.transform.name);
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
    public void OnDrawStartBtn()
    {
        isDrawing = true;
        StarDrawStartBtn.SetActive(false);
        StarDrawEndBtn.SetActive(true);
        Constellation = Instantiate(ConstellationFactory);
        Constellation.transform.parent = GameManager.instance.CelestialSphere.transform;
        Constellation.gameObject.name = ConstellationName.text;
    }

    public void OnDrawEndBtn()
    {
        isDrawing = false;
        StarDrawStartBtn.SetActive(true);
        StarDrawEndBtn.SetActive(false);
        ConstellationName.text = null;
    }
}
