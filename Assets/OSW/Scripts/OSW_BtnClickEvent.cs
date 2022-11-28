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

    //û�� -> ��ǥ�� / ��ǥ�� -> û��
    public void GiveAuthority(string name, string authority)
    {
        SYA_SymposiumManager.Instance.playerAuthority[name] = authority;
    }

    // ��ư OnClicked �Լ�, ���⼭�� GiveAuthority �Լ��� ȣ�����ִ� �뵵�� ���!
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
