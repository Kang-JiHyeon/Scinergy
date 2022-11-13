using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace SYA_UI
{
    public class SYA_UIManager : MonoBehaviourPun
    {
        public static SYA_UIManager Instance;

        public Transform btn_chat;
        public Transform btn_mic;

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
        }

        //채팅창 온오프
        public GameObject chat;
        //글쓰는 인풋필드
        //전송 버튼
        public void ChatOnOff()
        {
            chat.SetActive(!chat.activeSelf);
            btn_chat.GetChild(0).gameObject.SetActive(!btn_chat.GetChild(0).gameObject.activeSelf);
            btn_chat.GetChild(1).gameObject.SetActive(!btn_chat.GetChild(1).gameObject.activeSelf);
        }

        //마이크 온오프
        public GameObject micOn;
        public GameObject micOff;
        public void MicOnOff()
        {
            micOn.SetActive(!micOn.activeSelf);
            micOff.SetActive(!micOff.activeSelf);

            SYA_SymposiumManager.Instance.playerVoice[PhotonNetwork.NickName].enabled = micOn.activeSelf;
            // 마이크 키기
            // 본인의 닉네임의 오디오 소스를 끄고 켠다
            //SYA_SymposiumManager.Instance.playerVoice[PhotonNetwork.NickName].enabled = micOn.activeSelf;
            //SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].RPC("RpcMicOnOff", RpcTarget.All, PhotonNetwork.NickName, micOn.activeSelf);
            //photonView.RPC("RpcMicOnOff", RpcTarget.All, PhotonNetwork.NickName, micOn.activeSelf);
        }

        [PunRPC]
        public void RpcMicOnOff(string name, bool micOn)
        {
            SYA_SymposiumManager.Instance.playerVoice[name].enabled = micOn;
        }

        //설정창 온오프
        public GameObject option;
        public Transform btn_option;
        public void OptionOnOff()
        {
            option.SetActive(!option.activeSelf);
            btn_option.GetChild(0).gameObject.SetActive(!btn_option.GetChild(0).gameObject.activeSelf);
            btn_option.GetChild(1).gameObject.SetActive(!btn_option.GetChild(1).gameObject.activeSelf);
        }

        //나가기 창 온오프
        public GameObject quit;
        public void QuitOnOff()
        {
            if(SceneManager.GetActiveScene().name.Contains("Sympo"))
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
    }
}
