using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OSW_BtnClickEvent : MonoBehaviour
{
    public GameObject ChangeAudience;
    public GameObject ChangePrensenter;
    public Canvas m_canvas;
    GraphicRaycaster m_gr;
    PointerEventData m_ped;
    EventSystem es;

    List<RaycastResult> results;
    string userName;

    void Update()
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
                results = new List<RaycastResult>();
                m_gr.Raycast(m_ped, results);

                if (results[0].gameObject.name.Contains("UserListItem"))
                {
                    userName = GetComponentInChildren<Text>().text;
                    if (results[0].gameObject.GetComponentInChildren<Text>().text == userName)
                    {
                        if (SYA_SymposiumManager.Instance.playerAuthority[userName] == "Audience")
                        {
                            ChangePrensenter.SetActive(!ChangePrensenter.activeSelf);
                        }
                        else if (SYA_SymposiumManager.Instance.playerAuthority[userName] == "Presenter")
                        {
                            ChangeAudience.SetActive(!ChangeAudience.activeSelf);
                        }
                    }
                }
            }
        }
    }

    //청중 -> 발표자 / 발표자 -> 청중
    public void GiveAuthority(string name, string authority)
    {
        SYA_SymposiumManager.Instance.playerAuthority[name] = authority;
    }

    // 버튼 OnClicked 함수, 여기서는 GiveAuthority 함수를 호출해주는 용도로 사용!
    public void BtnAuthority(Button button)
    {
        if (button.name == "ChangePrensenter")
        {
            SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].RPC("RPCGiveAuthority", RpcTarget.All, userName, "Presenter");
        }
        else if (button.name == "ChangeAudience")
        {
            SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].RPC("RPCGiveAuthority", RpcTarget.All, userName, "Audience");
        }
    }
}
