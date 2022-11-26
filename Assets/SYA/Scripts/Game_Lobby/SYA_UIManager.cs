using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using RockVR.Video;
using UnityEngine.EventSystems;

namespace SYA_UI
{
    public class SYA_UIManager : MonoBehaviourPun
    {
        public static SYA_UIManager Instance;

        GraphicRaycaster m_gr;
        PointerEventData m_ped;

        private void Awake()
        {
            //if (!photonView.IsMine) return;
            if (Instance == null)
            {
                //�ν��Ͻ��� ���� �ְ�
                Instance = this;
                //���� ���� ��ȯ�� �Ǿ �ı����� �ʰ� �ϰڴ�

                DontDestroyOnLoad(gameObject);
            }
            //�׷��� ������
            else
            {
                Destroy(gameObject);
            }
            m_gr = GetComponent<GraphicRaycaster>();
            m_ped = new PointerEventData(null);
            BG.onValueChanged.AddListener(OnNewSave);
            ef.onValueChanged.AddListener(OnNewSave);
            mic.onValueChanged.AddListener(OnNewSave);
        }

        float recodingTimeNum;
        int min;
        public GameObject TV;

        private void Update()
        {
            if (TV != null)
                TV.SetActive(SceneManager.GetActiveScene().name.Contains("Sympo"));
            if (recoding_time)
            {
                recodingTimeNum += Time.deltaTime;
                recodingTime.text = string.Format("{0:D2}:{1:D2}", min, (int)recodingTimeNum);
                if ((int)recodingTimeNum > 59)
                {
                    recodingTimeNum = 0;
                    min++;
                }
            }

            //������Ŵ������� ĵ���� ����
            if (SceneManager.GetActiveScene().name.Contains("Sympo"))
                if (Input.GetMouseButtonDown(0))
                {
                    m_ped.position = Input.mousePosition;
                    List<RaycastResult> results = new List<RaycastResult>();
                    m_gr.Raycast(m_ped, results);
                    foreach (RaycastResult ray in results)
                    {
                        if (ray.gameObject.transform.GetComponent<Button>())
                        {
                            SYA_AudioManager.instance.clickSource.Play();
                        }
                    }
                }

        }

        //���̵� �׸� X�� ����, TAB ������ �����״�
        public GameObject guid;
        public void OnGuid()
        {
            guid.SetActive(!guid.activeSelf);
        }

        public Image btn_chat;
        public Sprite chatOn;
        public Sprite chatOff;
        //ä��â �¿���
        public GameObject chat;
        bool chating;
        //�۾��� ��ǲ�ʵ�
        //���� ��ư
        public void ChatOnOff()
        {
            if (!chating)
            {
                btn_chat.sprite = chatOn;
                chating = true;
            }
            else
            {
                btn_chat.sprite = chatOff;
                chating = false;
            }
            chat.SetActive(chating);
        }

        //����ũ �¿���

        public GameObject micOff;
        public GameObject micOn;
        public Image btn_mic;
        public Sprite micOnS;
        public Sprite micOffS;
        bool micing;
        public void MicOnOff()
        {
            if (!micing)
            {
                btn_mic.sprite = micOnS;
                micing = true;
            }
            else
            {
                btn_mic.sprite = micOffS;
                micing = false;
            }
            SYA_SymposiumManager.Instance.playerVoice[PhotonNetwork.NickName].enabled = micing;
            // ����ũ Ű��
            // ������ �г����� ����� �ҽ��� ���� �Ҵ�
            //SYA_SymposiumManager.Instance.playerVoice[PhotonNetwork.NickName].enabled = micOn.activeSelf;
            //SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].RPC("RpcMicOnOff", RpcTarget.All, PhotonNetwork.NickName, micOn.activeSelf);
            //photonView.RPC("RpcMicOnOff", RpcTarget.All, PhotonNetwork.NickName, micOn.activeSelf);
        }

        /*        [PunRPC]
                public void RpcMicOnOff(string name, bool micOn)
                {
                    SYA_SymposiumManager.Instance.playerVoice[name].enabled = micOn;
                }*/


        //������ â �¿���
        public GameObject quit;
        //��ư
        public Image btn_quit;
        public Sprite quitOn;
        public Sprite quitOff;
        public void QuitOnOff()
        {
            if (btn_quit.sprite != quitOn)
            {
                btn_quit.sprite = quitOn;
            }
            else
            {
                btn_quit.sprite = quitOff;
            }
            if (SceneManager.GetActiveScene().name.Contains("Sympo"))
            {
                quit.SetActive(!quit.activeSelf);
            }
            else
            {
                lobbyOn();
            }
        }

        public GameObject lobbyBack;
        public void lobbyOn()
        {
            lobbyBack.SetActive(!lobbyBack.activeSelf);
        }

        public void lobbyBackScene()
        {
            GetComponentInChildren<SYA_SympoUI>().SymposiumChsnge();
        }

        bool isRecording;
        public Image btn_rec;
        public Sprite recordOn;
        public Sprite recordOff;

        public GameObject recodStart;
        public GameObject recodEnd;

        //������� �ð�
        public Text recodingTime;
        bool recoding_time;

        //������� â �¿���
        //����������� ������
        public void Record()
        {
            //s��ȭ���̾��ٸ�
            if (isRecording)
            {
                if (recodEnd.activeSelf)
                    recodEnd.SetActive(false);
                else
                    //�����ϰڳĴ� ������ ���
                    recodEnd.SetActive(true);
            }
            //��ȭ���� �ƴϾ��ٸ�
            else
            {
                if (recodStart.activeSelf)
                    recodStart.SetActive(false);
                else
                    //�����ϰڳĴ� ������ ���
                    recodStart.SetActive(true);
            }

        }

        //���� ������ 
        public void RecordOn()
        {
            if (!isRecording)
            {
                //��Ȯ�� ���۵ǰ�
                VideoCaptureCtrl.instance.StartCapture();
                //��ȭ�� ���°� �Ǹ�
                isRecording = true;
                //�ð�ǥ�� �ؽ�Ʈ�� ������
                recodingTime.enabled = true;
                //�ð��� �帥��
                recoding_time = true;
                //â�� ������
                recodStart.SetActive(false);
                //����������� ��Ȳ������ ���Ѵ�
                btn_rec.sprite = recordOn;
            }
            else
            {
                //��Ȯ�� ������ �ǰ�
                VideoCaptureCtrl.instance.StopCapture();
                //��ȭ�� ���°� �ƴ� �Ǹ�
                isRecording = false;
                //�ð��� ���߰�
                recodingTimeNum = 0;
                //�ð�ǥ�� �ؽ�Ʈ�� ������
                recodingTime.enabled = false;
                //�ð��� �����
                recoding_time = false;
                //â�� ������
                recodEnd.SetActive(false);
                //����������� ȸ������ ���Ѵ�
                btn_rec.sprite = recordOff;
            }
        }

        //�ƴϿ並 ������ 
        public void RecordOff()
        {
            if (isRecording)
            {
                //â�� ������
                recodEnd.SetActive(false);
            }
            else
            {
                recodStart.SetActive(false);
            }
        }

        //����â �¿���
        public GameObject option;
        public Image btn_option;
        public Sprite optionOn;
        public Sprite optionOff;
        public void OptionOnOff()
        {
            if (!option.activeSelf)
            {
                btn_option.sprite = optionOn;
                option.SetActive(true);
            }
            else
            {
                btn_option.sprite = optionOff;
                option.SetActive(false);
            }
            eexBG= BG.value;
            eexEF= ef.value;
            eexMic = mic.value;
        }

        public GameObject micOption;
        public Image button;
        public Sprite micopOn;
        public Sprite audiioopOn;
        public Scrollbar BG;
        public Scrollbar ef;
        public Scrollbar mic;
        new bool audio;
        public void OnOpttionAudio()
        {
            if (!audio)
            {
                //��ư�� ������ ����ũ�� �ٲ��
                button.sprite = micopOn;
                //����ũ ���� �̹����� ���
                micOption.SetActive(true);
                audio = true;
            }
            else
            {
                //�ٽ� ������ ������� �ٲ��
                button.sprite = audiioopOn;
                //����� ���� �̹����� ���
                micOption.SetActive(false);
                audio = false;
            }
        }

        public float exBG;
        public float exEF;
        public float exMic;
        public float eexBG;
        public float eexEF;
        public float eexMic;
        //�ϷḦ ������ �ݿ��ȴ�
        //��Ҹ� ������ â�� ������(����� ������ ���� ���������)
        public void OnSaveOption()
        {
            print($"����� ������ {exBG}�����մϴ� ");
            print($"ȿ���� ������ {exEF}�����մϴ� ");
            SYA_SymposiumManager.Instance.playerVoice[PhotonNetwork.NickName].volume = exMic;
            print($"����ũ�� ������ {exMic}�����մϴ� ");
        }

        //�Ϸ��ư ���� ���
        public void OnNewSave(float n)
        {
            exBG = BG.value;
            exEF = ef.value;
            exMic = mic.value;
            //OnSaveOption();
        }

        //���
        public void Cancle()
        {
            BG.value = eexBG;
            ef.value = eexEF;
            mic.value = eexMic;
            OnSaveOption();
        }


    }
}
