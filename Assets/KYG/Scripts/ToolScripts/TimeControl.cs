using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
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
    bool timeFlow = false;
    public GameObject TimeFlowBtn;
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
        if (timeFlow)
        {
            TimeFlow();
            TimeFlowBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Stop";
        }
        else
        {
            TimeFlowBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Flow";
        }
    }
    //int round = 0;
    //int originRound;
    //bool isRound = false;
    public GameObject CelestialSphere;
    float min;
    float hour;
    public Slider timeScaleSlider;
    public float timeScale;
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
            if(SelectedHand) rotateAngle = CalculateAngle(ClockCenter.transform.up, Input.mousePosition - ClockCenter.transform.position);

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
                
                sphereRotateAngle = -offset/2;
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
                sphereRotateAngle = -offset / 24;
            }
            if(SelectedHand) GameManager.instance.CelestialSphere.transform.Rotate(Vector3.up * sphereRotateAngle);
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

        if (Input.GetButtonUp("Fire1"))
        {
            SelectedHand = null;
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

    public void OnTimeFlowBtn()
    {
        timeFlow = !timeFlow;
    }

    public void TimeFlow()
    {
        //if (Input.GetKey(KeyCode.Alpha1))
        //{
        //    min -= timeScale;
        //    hour -= (1.0f / 12.0f) * timeScale;
        //    sphereRotateAngle = timeScale / 24;
        //    CelestialSphere.transform.Rotate(Vector3.up * sphereRotateAngle);
        //}
        //if (Input.GetKey(KeyCode.Alpha2))
        //{
        //    hour -= timeScale;
        //    min -= (12 * timeScale);
        //    sphereRotateAngle = timeScale / 2;
        //    CelestialSphere.transform.Rotate(Vector3.up * sphereRotateAngle);
        //}
        timeScale = timeScaleSlider.value;
        min -= timeScale;
        hour -= (1.0f / 12.0f) * timeScale;
        sphereRotateAngle = timeScale / 24;
        GameManager.instance.CelestialSphere.transform.Rotate(Vector3.up * sphereRotateAngle);
    }
    public void OnCloseBtn()
    {
        gameObject.SetActive(false);
    }
}
