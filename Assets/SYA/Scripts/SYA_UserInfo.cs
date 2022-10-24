using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace SYA_UserInfoName
{
    #region ������������ ����(Json)
    [Serializable]
    public class SaveInfo
    {
        public List<UserInfoData> UserInfo;

    }

    //���� ����
    [Serializable]
    public class UserInfoData
    {
        public string PlayerID;
        public string Passward;
        public string NicName;
        public string Avatar;
        public List<UserOption> Option;
    }

    [Serializable]
    public class UserOption
    {
        public string Not;
        public string NNot;
    }
    #endregion

    public class SYA_UserInfo : MonoBehaviour
    {
        /*// Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }*/
    }
}