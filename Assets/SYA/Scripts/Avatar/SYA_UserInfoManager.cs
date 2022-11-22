using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SYA_UserInfoName;

namespace SYA_UserInfoManagerSaveLoad
{
    //�ǽð����� �������� �޾� �����صδ� ��
    //�ҷ��� ������ �����Ͽ� �ѷ��ִ� ���ҵ� �� ����
    public class SYA_UserInfoManager : MonoBehaviour
    {
        public static SYA_UserInfoManager Instance;

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
        public string Id;
        public string Passward;
        public string NicName;
        public string Avatar;
    }


    //�����ϴ� �� ���� �Լ� ������
    //��Ʈ��ũ�� ������ �� JSON�������� ����
    //��Ʈ��ũ�� �����ִ� �Լ� ����
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

        //���̵� ���� �н����� ����
        //ȸ������ ��ư�� ���� ��
        public void IdPasswardSave(string id, string passward)
        {
            print(111);
            SYA_UserInfoManager.Instance.Id = id;
            SYA_UserInfoManager.Instance.Passward = passward;
        }

        //�г��� ����
        public void NicNameSave(string name)
        {
            print(2222);
            SYA_UserInfoManager.Instance.NicName = name;
        }

        //�ƹ�Ÿ ���� ����
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
            print("����Ϸ�");
        }
    }

    public class SYA_UserInfoLoad
    {
        //���̵� �ҷ�����

        //�н����� �ҷ�����

        //�г��� �ҷ�����

        //�ƹ�Ÿ ���� �ҷ�����

    }
}