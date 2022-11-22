using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using RockVR.Video;

namespace SYA_UI
{
    public class SYA_UIManager : MonoBehaviourPun
    {
        public static SYA_UIManager Instance;


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
            exBG = BG.value;
            exEF = ef.value;
            exMic = mic.value;
        }

        float exBG;
        float exEF;
        float exMic;
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
        public void OnNewSave()
        {
            exBG = BG.value;
            exEF = ef.value;
            exMic = mic.value;
            OnSaveOption();
        }

        //���
        public void Cancle()
        {
            BG.value = exBG;
            ef.value = exEF;
            mic.value = exMic;
            OnSaveOption();
        }
    }
}
