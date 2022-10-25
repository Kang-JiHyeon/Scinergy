using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    private GraphicRaycaster GraphicRaycaster;
    public RectTransform HourHand;
    public RectTransform MinuteHand;
    public RectTransform ClockCenter;
    public float sphereRotateAngle;
    public float rotateAngle;
    [SerializeField]
    RectTransform SelectedHand;
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
        Control();
    }
    //int round = 0;
    //int originRound;
    //bool isRound = false;
    public GameObject CelestialSphere;
    float min;
    float hour;
    public void Control()
    {
        PointerEventData ped = new PointerEventData(null);
        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();

        if (Input.GetButtonDown("Fire1"))
        {
            GraphicRaycaster.Raycast(ped, results);
            if (results.Count <= 0) return;
            if(results[0].gameObject == HourHand.gameObject || results[0].gameObject == MinuteHand.gameObject)
            {
                SelectedHand = results[0].gameObject.GetComponent<RectTransform>();
            }
        }
        
        if (Input.GetButton("Fire1"))
        {
            rotateAngle = CalculateAngle(ClockCenter.transform.up, Input.mousePosition - ClockCenter.transform.position);

            if(SelectedHand == HourHand)
            {
                float offset = rotateAngle - hour;
                if (Mathf.Abs(offset) > 300)
                {
                    if (rotateAngle > hour)
                    {
                        hour += 360;
                    }
                    else
                    {
                        hour -= 360;
                    }
                    offset = rotateAngle - hour;
                }
                hour += offset;
                min += 12 * offset;
                
                sphereRotateAngle = offset/2;
            }
            if(SelectedHand == MinuteHand)
            {
                
                float offset = rotateAngle - min;
                if(Mathf.Abs(offset) > 300)
                {
                    if(rotateAngle > min)
                    {
                        min += 360;
                    }
                    else
                    {
                        min -= 360;
                    }
                    offset = rotateAngle - min;
                }
                min += offset;
                hour += (1.0f / 12.0f) * offset;
                sphereRotateAngle = offset / 24;
            }
            CelestialSphere.transform.Rotate(Vector3.up * sphereRotateAngle);
            #region ³»ÄÚµå
            //if(rotateAngle <= 10f)
            //{
            //    if(isRound == false)
            //    {
            //        originRound = round;
            //        round++;
            //        isRound = true;
            //    }
            //}
            //else
            //{
            //    isRound = false;
            //}
            ////print(rotateAngle);
            //print(round);
            //SelectedHand.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotateAngle));
            //if (SelectedHand == HourHand)
            //{
            //    MinuteHand.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 12 * rotateAngle));
            //}
            //if (SelectedHand == MinuteHand)
            //{
            //    print(rotateAngle);
            //    HourHand.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotateAngle * 30/360)) * Quaternion.Euler(new Vector3(0,0,-30 * round));
            //}
            #endregion
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            min -= 2;
            hour -= (1.0f / 12.0f) * 2;
        }
        if(Input.GetKey(KeyCode.Alpha2))
        {
            hour -= 2;
            min -= (12 * 2);
        }
        min %= 360;
        hour %= 360;
        MinuteHand.transform.eulerAngles = new Vector3(0, 0, min);
        HourHand.transform.eulerAngles = new Vector3(0, 0, hour);
    }
    public float CalculateAngle(Vector3 from, Vector3 to)
    {
        float f = Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
        return (f - 360) % 360;
    }
}
