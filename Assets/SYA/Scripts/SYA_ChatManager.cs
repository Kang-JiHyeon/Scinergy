using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using Photon.Realtime;
using Photon.Pun;
using AuthenticationValues = Photon.Chat.AuthenticationValues;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class SYA_ChatManager : MonoBehaviourPun, IChatClientListener
{

    private ChatClient chatClient;

    //ChatItem����
    public GameObject chatFactory;
    //��ũ�Ѻ��� ����Ʈ ���� ����
    public RectTransform trContent;
    public InputField inputField;
    public Text outputText;
    public string currentChannel;

    string Allchannel = "Allchannel";
    string Lobbychannel = "Lobbychannel";
    string Constchannel = "Constchannel";
    string Solarchannel = "Solarchannel";

    // Use this for initialization
    void Awake()
    {
        Application.runInBackground = true;

        //ä�� ���� ����
        chatClient = new ChatClient(this);
        chatClient.ChatRegion = "ASIA";
        chatClient.UseBackgroundWorkerForSending = true;
        chatClient.AuthValues = new AuthenticationValues(PhotonNetwork.NickName);
        string appId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat;
        string appVersion = PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion;
        //�۾��̵� , �۹��� , ä�� �⺻ ���� 
        chatClient.Connect(appId, appVersion, chatClient.AuthValues);

        currentChannel = Allchannel;
/*        chatClient.Subscribe(new string[] { Allchannel });*/

        //��ǲ�ʵ忡�� ���ͫ��� �� ȣ��Ǵ� �Լ� ���
        inputField.onSubmit.AddListener(OnSubmit);
    }

    //��ǲ�ʵ忡�� ���ͫ��� �� ȣ��Ǵ� �Լ�
    public void OnSubmit(string s)
    {
        //< color =#FFFFF>�г���</color>
        string chatText = $"[{currentChannel}] {PhotonNetwork.NickName} : {s}";
        //photonView.RPC("RpcAddChat", RpcTarget.All, chatText);
        PushMessage(currentChannel, chatText);
        //4 ��ǲê�� ���� �ʱ�ȭ
        inputField.text = "";
        //5 ��ǲê�� ��Ŀ�� ����
        inputField.ActivateInputField();
    }
    PhotonView pv;

    private void Update()
    {
/*        if(pv==null)
        {
            pv = SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName];
            if (PhotonNetwork.MasterClient.UserId == pv.Owner.UserId)//�����̶��
            {
                chatClient.Subscribe(new string[] { Allchannel, Lobbychannel, Constchannel, Solarchannel });
            }
            else
            {
                chatClient.Subscribe(new string[] { Allchannel, Lobbychannel });
            }
        }*/
        chatClient.Service();
        if (inputField.isFocused && Input.GetKeyDown(KeyCode.Tab))
        {
            OnClickTab();
        }
    }

    //���� ������
    public void OnClickTab()
    {
        string scene = SceneManager.GetActiveScene().name;
        //��ü �� ���� / ���� �� ��ü
        if (currentChannel == Allchannel)
        {
            if (scene == "SymposiumScene")
            {
                currentChannel = Lobbychannel;
            }
            else
            {
                currentChannel = scene == "KJH_RevolutionScene" ? Constchannel : Solarchannel;
            }
        }
    }

    //���۹�ư�� ������
    public void OnClickChat()
    {
        PushMessage(currentChannel, inputField.text);
    }

    public void PushMessage(string channelName, string message)
    {
        chatClient.PublishMessage(channelName, message);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        throw new System.NotImplementedException();
    }

    // ������ ������ ������
    public void OnConnected()
    {
        print("������ ����Ǿ����ϴ�.");

        // ������ ä�θ����� ����
        if (pv == null)
        {
            pv = SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName];
            if (PhotonNetwork.MasterClient.UserId == pv.Owner.UserId)//�����̶��
            {
                chatClient.Subscribe(new string[] { Allchannel, Lobbychannel, Constchannel, Solarchannel });
            }
            else
            {
                chatClient.Subscribe(new string[] { Allchannel, Lobbychannel });
            }
        }
    }
    // �������� ������ ������
    public void OnDisconnected()
    {
        print("������ ������ ���������ϴ�.");
    }


    //���� ����Ʈ�� ����
    float prevContentH;
    //��ũ�Ѻ� ����
    public RectTransform trScrollView;

    //ä�ο� �޼����� ���� �ö���� �Ҹ��� �Լ�
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        if (channelName.Equals(currentChannel))
        {
            //�ٲ�� �� h�� �ֱ�
            prevContentH = trContent.sizeDelta.y;

            //1 ê�������� ����� (�θ� ��ũ�Ѻ��� ������)
            GameObject item = Instantiate(chatFactory, trContent);
            //2 ���� ê�����ۿ��� ê������ ������Ʈ ��������
            SYA_ChatItem chat = item.GetComponent<SYA_ChatItem>();
            //3 ������ ������Ʈ�� s����
            chat.SetText(messages.ToString());
            StartCoroutine(AutoScrollBottom());
        }

    }

    IEnumerator AutoScrollBottom()
    {
        yield return null;
        if (trContent.sizeDelta.y > trScrollView.sizeDelta.y)
        {
            //����Ʈ�� �ٴڿ� ��� �־��ٸ�
            if (trContent.anchoredPosition.y >= prevContentH - trScrollView.sizeDelta.y)
            {
                //����Ʈ�� y���� �ٽ� ����������
                trContent.anchoredPosition = new Vector2(0, trContent.sizeDelta.y - trScrollView.sizeDelta.y);
            }
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnChatStateChange(ChatState state)
    {
        throw new System.NotImplementedException();
    }
}

