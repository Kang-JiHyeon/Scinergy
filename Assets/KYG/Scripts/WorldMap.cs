using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class WorldMap  : MonoBehaviour
{
    private GraphicRaycaster GraphicRaycaster;
    public RectTransform mapImage;
    public GameObject CelestialControl;
    float width;
    float height;
    public float longitude;
    public float latitude;
    public float longitudeAdjust;
    public float latitudeAdjust;

    private void Awake()
    {
        GraphicRaycaster = GetComponentInChildren<GraphicRaycaster>();
    }
    // Start is called before the first frame update
    void Start()
    {
        width = mapImage.rect.width;
        height = mapImage.rect.height;
        print(width);
        print(height);
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
        if (Input.GetButton("Fire1"))
        {
            GraphicRaycaster.Raycast(ped, results);
            if (results.Count <= 0) return;
            //results[0].gameObject.transform.position = ped.position;

            longitude = (Input.mousePosition - results[0].gameObject.transform.position).x/width*2 * 180;
            latitude = (Input.mousePosition - results[0].gameObject.transform.position).y/height*2 * 90;
            longitudeAdjust = longitude + 90;
            latitudeAdjust = latitude - 90;
            CelestialControl.GetComponent<Rotate>().mapControl = true;
            //print(Input.mousePosition - results[0].gameObject.transform.position);
        }
        else
        {
            CelestialControl.GetComponent<Rotate>().mapControl = false;
        }
    }
}
