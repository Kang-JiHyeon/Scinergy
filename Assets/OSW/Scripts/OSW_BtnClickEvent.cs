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
        //"Owner" �� ��쿡�� ��ư ��Ŭ�� Ȱ��ȭ!
        if (SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName] == "Owner")
        {
            //if (button == PointerEventData.InputButton.Right)
            if (Input.GetMouseButtonDown(1))
            {
                // ��ǥ�ڿ� û������ ���� ��ư�� �ΰ� �� �����ִ� ����
                // û�� �� ����Ʈ�信 ���콺 ��Ŭ�� �ϸ� "��ǥ�ڷ� ����" ��ư�� Ȱ��ȭ�ǰ�
                // ��ǥ�� �� ����Ʈ�信 ���콺 ��Ŭ�� �ϸ� "û������ ����" ��ư�� Ȱ��ȭ �ǰ�
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

                // ��ȯ ��ư�� ���� ����
                if (results[0].gameObject.transform.parent.parent.parent.parent.name == " PresenterBG")
                {
                    
                    Debug.Log("û������ ��ȯ");
                    ChangeAudience.SetActive(!ChangeAudience.activeSelf);
                    //ChangePrensenter = null;
                    //ChangePrensenter.SetActive(false);
                }
                
                if(results[0].gameObject.transform.parent.parent.parent.parent.name == "AudienceBG")
                {
                    Debug.Log("��ǥ�ڷ� ��ȯ");
                    ChangePrensenter.SetActive(!ChangePrensenter.activeSelf);
                    //ChangeAudience = null;
                    //ChangeAudience.SetActive(false);
                }
            }
        }
    }

    // ��ȯ ��ư�� ������ master�� �ٲ��!
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
        print($"Ŭ�� ���: {isRight}");
    }
}
