using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class WorldMap  : MonoBehaviour, IPointerClickHandler
{
    private GraphicRaycaster GraphicRaycaster;
    private void Awake()
    {
        GraphicRaycaster = GetComponentInChildren<GraphicRaycaster>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnGraphicRayCast();
    }
    public void OnGraphicRayCast()
    {
        PointerEventData ped = new PointerEventData(null);
        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        if (Input.GetButtonDown("Fire1"))
        {
            GraphicRaycaster.Raycast(ped, results);
            if (results.Count <= 0) return;
            //results[0].gameObject.transform.position = ped.position;
            print(results);
            print(results[0].gameObject.name);
            print(Input.mousePosition);
            print(results[0].gameObject.transform.position);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log("Mouse Position : " + eventData.position);
        }
    }
}
