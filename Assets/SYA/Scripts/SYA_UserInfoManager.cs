using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SYA_UserInfoName;

namespace SYA_UserInfoManagerSaveLoad
{
    //실시간으로 정보저장 받아 저장해두는 곳
    //불러온 정보를 저장하여 뿌려주는 역할도 할 예정
    public class SYA_UserInfoManager : MonoBehaviour
    {
        public static SYA_UserInfoManager Instance;

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
        public string Id;
        public string Passward;
        public string NicName;
        public string Avatar;
    }


    //저장하는 것 관련 함수 모음과
    //네트워크로 보내기 전 JSON형식으로 수정
    //네트워크로 보내주는 함수 관리
    public class SYA_UserInfoSave : MonoBehaviour
    {
        private static SYA_UserInfoSave instance = null;

        public static SYA_UserInfoSave Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SYA_UserInfoSave();
                }
                return instance;
            }
        }

        SaveInfo saveInfo = new SaveInfo();
        UserInfoData userInfoData = new UserInfoData();

        //아이디 저장 패스워드 저장
        //회원가입 버튼을 누를 때
        public void IdPasswardSave(string id, string passward)
        {
            print(111);
            SYA_UserInfoManager.Instance.Id = id;
            SYA_UserInfoManager.Instance.Passward = passward;
        }

        //닉네임 저장
        public void NicNameSave(string name)
        {
            print(2222);
            SYA_UserInfoManager.Instance.NicName = name;
        }

        //아바타 유형 저장
        public void AvatarSave(string avatar)
        {
            SYA_UserInfoManager.Instance.Avatar = avatar;
        }

        public void Save()
        {
            print(4);
            saveInfo.UserInfo = new List<UserInfoData>();

            userInfoData.PlayerID = SYA_UserInfoManager.Instance.Id;
            userInfoData.Passward = SYA_UserInfoManager.Instance.Passward;
            userInfoData.NicName = SYA_UserInfoManager.Instance.NicName;
            userInfoData.Avatar = SYA_UserInfoManager.Instance.Avatar;
            userInfoData.Option = new List<UserOption>();

            saveInfo.UserInfo.Add(userInfoData);

            string jsonData = JsonUtility.ToJson(saveInfo, true);

            print(jsonData);
            print("저장완료");
        }
    }

    public class SYA_UserInfoLoad
    {
        //아이디 불러오기

        //패스워드 불러오기

        //닉네임 불러오기

        //아바타 유형 불러오기

    }
}