using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OSW_BtnClickEvent : MonoBehaviour//, IPointerClickHandler
{
    public GameObject ChangeAudience;
    public GameObject ChangePrensenter;

    public Canvas m_canvas;
    GraphicRaycaster m_gr;
    PointerEventData m_ped;
    EventSystem es;

    SYA_SympoPresentstion sYA_SympoPresentstion;

    void Start()
    {
        sYA_SympoPresentstion = FindObjectOfType<SYA_SympoPresentstion>();
    }

    private void Update()
    {
        //"Owner" 일 경우에만 버튼 우클릭 활성화!
        if (SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName] == "Owner")
        {
            //if (button == PointerEventData.InputButton.Right)
            if (Input.GetMouseButtonDown(1))
            {
                // 발표자와 청중으로 변경 버튼은 두개 다 꺼져있는 상태
                // 청중 쪽 리스트뷰에 마우스 우클릭 하면 "발표자로 변경" 버튼이 활성화되고
                // 발표자 쪽 리스트뷰에 마우스 우클릭 하면 "청중으로 변경" 버튼이 활성화 되게
                m_canvas = GameObject.Find("Canvas_DontDestroy").GetComponent<Canvas>();
                m_gr = m_canvas.GetComponent<GraphicRaycaster>();
                es = FindObjectOfType<EventSystem>();
                
                m_ped = new PointerEventData(es);

                m_ped.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                m_gr.Raycast(m_ped, results);

                //foreach (RaycastResult res in results)
                //{
                //    print(res.gameObject.name);
                //}
                //print("res Count: " + results.Count);
                //print("canvas: " + m_canvas.name);
                //print("gr: " + m_gr != null);
                //print("ped: " + m_ped != null);

                // 전환 버튼이 같이 생김
                if (results[0].gameObject.transform.parent.parent.parent.parent.name == " PresenterBG")
                {
                    
                    Debug.Log("청중으로 전환");
                    ChangeAudience.SetActive(!ChangeAudience.activeSelf);
                    //ChangePrensenter = null;
                    //ChangePrensenter.SetActive(false);
                }
                
                if(results[0].gameObject.transform.parent.parent.parent.parent.name == "AudienceBG")
                {
                    Debug.Log("발표자로 전환");
                    ChangePrensenter.SetActive(!ChangePrensenter.activeSelf);
                    //ChangeAudience = null;
                    //ChangeAudience.SetActive(false);
                }
            }
        }
    }

    // 전환 버튼을 누르면 master가 바뀐다!
    public void Presenter()
    {
        SYA_SymposiumManager.Instance.playerAuthority[name] = "Owner";
        sYA_SympoPresentstion.pre.SetActive(true);
    }

    public void Audience()
    {
        SYA_SymposiumManager.Instance.playerAuthority[name] = "Audience";
        sYA_SympoPresentstion.pre.SetActive(false);
    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    Debug.Log($"Clicked {eventData}");

    //    // get the click button from eventData
    //    var button = eventData.button;


    //}

    public void TestFunc(bool isRight)
    {
        print($"클릭 결과: {isRight}");
    }
}
