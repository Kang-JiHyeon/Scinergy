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
                //인스턴스에 나를 넣고
                Instance = this;
                //나를 씬이 전환이 되어도 파괴되지 않게 하겠다

                DontDestroyOnLoad(gameObject);
            }
            //그렇지 않으면
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

            //오디오매니저에게 캔버스 설정
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

        //가이드 그림 X와 연결, TAB 눌러서 껏다켰다
        public GameObject guid;
        public void OnGuid()
        {
            guid.SetActive(!guid.activeSelf);
        }

        public Image btn_chat;
        public Sprite chatOn;
        public Sprite chatOff;
        //채팅창 온오프
        public GameObject chat;
        bool chating;
        //글쓰는 인풋필드
        //전송 버튼
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

        //마이크 온오프

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
            // 마이크 키기
            // 본인의 닉네임의 오디오 소스를 끄고 켠다
            //SYA_SymposiumManager.Instance.playerVoice[PhotonNetwork.NickName].enabled = micOn.activeSelf;
            //SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].RPC("RpcMicOnOff", RpcTarget.All, PhotonNetwork.NickName, micOn.activeSelf);
            //photonView.RPC("RpcMicOnOff", RpcTarget.All, PhotonNetwork.NickName, micOn.activeSelf);
        }

        /*        [PunRPC]
                public void RpcMicOnOff(string name, bool micOn)
                {
                    SYA_SymposiumManager.Instance.playerVoice[name].enabled = micOn;
                }*/


        //나가기 창 온오프
        public GameObject quit;
        //버튼
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

        //기록중인 시간
        public Text recodingTime;
        bool recoding_time;

        //영상찍기 창 온오프
        //영상아이콘을 누르면
        public void Record()
        {
            //s녹화중이었다면
            if (isRecording)
            {
                if (recodEnd.activeSelf)
                    recodEnd.SetActive(false);
                else
                    //종료하겠냐는 문구가 뜬다
                    recodEnd.SetActive(true);
            }
            //녹화중이 아니었다면
            else
            {
                if (recodStart.activeSelf)
                    recodStart.SetActive(false);
                else
                    //시작하겠냐는 문구가 뜬다
                    recodStart.SetActive(true);
            }

        }

        //예를 누르면 
        public void RecordOn()
        {
            if (!isRecording)
            {
                //녹확가 시작되고
                VideoCaptureCtrl.instance.StartCapture();
                //녹화중 상태가 되며
                isRecording = true;
                //시간표시 텍스트가 켜지고
                recodingTime.enabled = true;
                //시간이 흐른다
                recoding_time = true;
                //창이 닫힌다
                recodStart.SetActive(false);
                //영상아이콘이 주황색으로 변한다
                btn_rec.sprite = recordOn;
            }
            else
            {
                //녹확가 끝나게 되고
                VideoCaptureCtrl.instance.StopCapture();
                //녹화중 상태가 아니 되며
                isRecording = false;
                //시간이 멈추고
                recodingTimeNum = 0;
                //시간표시 텍스트가 꺼지고
                recodingTime.enabled = false;
                //시간이 멈춘다
                recoding_time = false;
                //창이 닫힌다
                recodEnd.SetActive(false);
                //영상아이콘이 회색으로 변한다
                btn_rec.sprite = recordOff;
            }
        }

        //아니요를 누르면 
        public void RecordOff()
        {
            if (isRecording)
            {
                //창이 닫힌다
                recodEnd.SetActive(false);
            }
            else
            {
                recodStart.SetActive(false);
            }
        }

        //설정창 온오프
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
                //버튼을 누르면 마이크로 바뀐다
                button.sprite = micopOn;
                //마이크 설정 이미지가 뜬다
                micOption.SetActive(true);
                audio = true;
            }
            else
            {
                //다시 누르면 오디오로 바뀐다
                button.sprite = audiioopOn;
                //오디오 설정 이미지가 뜬다
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
        //완료를 누르면 반영된다
        //취소를 누르면 창이 닫힌다(오디오 설정과 그전 밸류값으로)
        public void OnSaveOption()
        {
            print($"배경의 음량을 {exBG}저장합니다 ");
            print($"효과의 음량을 {exEF}저장합니다 ");
            SYA_SymposiumManager.Instance.playerVoice[PhotonNetwork.NickName].volume = exMic;
            print($"마이크의 음량을 {exMic}저장합니다 ");
        }

        //완료버튼 누른 결루
        public void OnNewSave(float n)
        {
            exBG = BG.value;
            exEF = ef.value;
            exMic = mic.value;
            //OnSaveOption();
        }

        //취소
        public void Cancle()
        {
            BG.value = eexBG;
            ef.value = eexEF;
            mic.value = eexMic;
            OnSaveOption();
        }


    }
}
