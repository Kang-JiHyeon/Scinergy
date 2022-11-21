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
using SYA_UserInfoManagerSaveLoad;
using SYA_UI;

public class SYA_ChatManager : MonoBehaviourPun, IChatClientListener
{
    public static SYA_ChatManager Instance;
    public ChatClient chatClient;

    //ChatItem����
    public GameObject chatFactory;
    //��ũ�Ѻ��� ����Ʈ ���� ����
    public RectTransform trContent;
    public InputField inputField;
    public Text outputText;
    public string currentChannel;

    Dictionary<string, string> cannel = new Dictionary<string, string>();
    public string Allchannel = "Allchannel";
    public string Lobbychannel = "Lobbychannel";
    public string Constchannel = "Constchannel";
    public string Solarchannel = "Solarchannel";

    Color allColor;
    Color lobbyColor;
    Color constColor;
    Color solarColor;

    string allColor_code = "#58FA58";
    string lobbyColor_code = "#2E64FE";
    string constColor_code = "#9A2EFE";
    string solarColor_code = "#FE642E";

    public bool inputFocused;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        cannel["Allchannel"] = "��ü";
        cannel["Lobbychannel"] = "�κ�";
        cannel["Constchannel"] = "���ڸ�";
        cannel["Solarchannel"] = "�༺";
        Application.runInBackground = true;

        //ä�� ���� ����
        chatClient = new ChatClient(this);
        chatClient.ChatRegion = "ASIA";
        chatClient.UseBackgroundWorkerForSending = true;
        //chatClient.AuthValues = new AuthenticationValues(PhotonNetwork.NickName);
        string appId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat;
        string appVersion = PhotonNetwork.AppVersion;
        //�۾��̵� , �۹��� , ä�� �⺻ ���� 
        chatClient.Connect(appId, appVersion, new AuthenticationValues(SYA_UserInfoManager.Instance.NicName));

        currentChannel = Allchannel;
        /*        chatClient.Subscribe(new string[] { Allchannel });*/

        //��ǲ�ʵ忡�� ���ͫ��� �� ȣ��Ǵ� �Լ� ���
        inputField.onSubmit.AddListener(OnSubmit);
    }

    //��ǲ�ʵ忡�� ���ͫ��� �� ȣ��Ǵ� �Լ�
    public void OnSubmit(string s)
    {
        //�ƹ��͵� �ʵ忡 �� �����ִٸ� ����
        if (s == "") return;
        //< color =#FFFFF>�г���</color>
        string chatText = $"[{cannel[currentChannel]}] {PhotonNetwork.NickName} : {s}";
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

        inputFocused = inputField.isFocused;

        channelName.text = $"<color=#{ColorUtility.ToHtmlStringRGB(ColorChat(currentChannel))}>[{cannel[currentChannel]}]</color>";
        chatClient.Service();
        if (SYA_UIManager.Instance.chat.activeSelf)
        {
            if (messagess.Count != 0)
                foreach (string mess in messagess)
                {
                    MakeChat(mess);
                    messagess.Remove(mess);
                }
        }
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
        if (PhotonNetwork.MasterClient.UserId != pv.Owner.UserId)
        {
            if (currentChannel == Allchannel)
            {

                if (scene == "SymposiumScene")
                {
                    currentChannel = Lobbychannel;
                }
                else
                {
                    currentChannel = scene == "KJH_SolarSystemScene" ? Solarchannel : Constchannel;
                }
            }
            else
            {
                currentChannel = Allchannel;
            }
        }
        else
        {
            if (currentChannel == Lobbychannel)
            {
                currentChannel = Solarchannel;
            }
            else if (currentChannel == Solarchannel)
            {
                currentChannel = Constchannel;
            }
            else if (currentChannel == Constchannel)
            {
                currentChannel = Allchannel;
            }
            else if (currentChannel == Allchannel)
            {
                currentChannel = Lobbychannel;
            }
        }
    }

    public Text channelName;
    //���۹�ư�� ������
    public void OnClickChat()
    {
        OnSubmit(inputField.text);
    }

    public void PushMessage(string channelName, string message)
    {
        chatClient.PublishMessage(channelName, message);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
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
        chatClient.SetOnlineStatus(ChatUserStatus.Online);
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




    //Allchannel, Lobbychannel, Constchannel, Solarchannel

    public List<string> messagess = new List<string>();

    //ä�ο� �޼����� ���� �ö���� �Ҹ��� �Լ�
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        //ä��â�� ���� �ִٸ� 

        /*if (channelName.Equals(currentChannel))
        {*/


        /*Vector2 y = trContent.;
        y.y += item.transform.localScale.y;
        trContent.localScale = y;*/
        //3 ������ ������Ʈ�� s����

        //���� �޼��� ��� �����صα�

        messagess.Add($"<color=#{ColorUtility.ToHtmlStringRGB(ColorChat(channelName))}> {messages[0].ToString()}</color>");


        //}
    }

    Color ColorChat(string cannel)
    {
        Color chanColor = new Color();
        if (cannel == Allchannel)
        {
            if (ColorUtility.TryParseHtmlString(allColor_code, out allColor))
                chanColor = allColor;
        }
        else if (cannel == Lobbychannel)
        {
            if (ColorUtility.TryParseHtmlString(lobbyColor_code, out lobbyColor))
                chanColor = lobbyColor;
        }
        else if (cannel == Constchannel)
        {
            if (ColorUtility.TryParseHtmlString(constColor_code, out constColor))
                chanColor = constColor;
        }
        else
        {
            if (ColorUtility.TryParseHtmlString(solarColor_code, out solarColor))
                chanColor = solarColor;
        }
        return chanColor;
    }

    void MakeChat(string mess)
    {
        //�ٲ�� �� h�� �ֱ�
        prevContentH = trContent.sizeDelta.y;

        //1 ê�������� ����� (�θ� ��ũ�Ѻ��� ������)
        GameObject item = Instantiate(chatFactory, trContent);
        //2 ���� ê�����ۿ��� ê������ ������Ʈ ��������
        SYA_ChatItem chat = item.GetComponent<SYA_ChatItem>();
        chat.SetText(mess);
        StartCoroutine(AutoScrollBottom());
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

    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
    }

    public void OnUnsubscribed(string[] channels)
    {
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
    }

    public void OnUserSubscribed(string channel, string user)
    {

    }

    public void OnUserUnsubscribed(string channel, string user)
    {

    }

    public void OnChatStateChange(ChatState state)
    {

    }
}

