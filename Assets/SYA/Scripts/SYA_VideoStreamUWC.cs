using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace uWindowCapture
{
    public class SYA_VideoStreamUWC : MonoBehaviourPun
    {
        public static SYA_VideoStreamUWC Instance;
        //화면 공유 버튼을 누르면
        //다른 유저의 컴퓨터가 있는지 검사
        //-> photonView를 사용하여 isMine
        //-> 플레이어 리스트 검사
        //-> 플레이어 카운트 2개 이상이라면
        //call진행
        //ismine true는 Pc1
        //false는 Pc2
        //리스트의 아이콘을 누르면 
        //애드 윈도우창을 소스 이미지 리스트에 넣는다

        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public GameObject InstantiateWindowPrefab(GameObject gameObject, Transform transform)
        {
            GameObject go = PhotonNetwork.Instantiate(gameObject.name, Vector3.zero, Quaternion.identity);
            go.transform.parent = transform;
            return go;
        }
    }
}
