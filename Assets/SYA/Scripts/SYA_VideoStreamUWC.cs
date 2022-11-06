using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace uWindowCapture
{
    public class SYA_VideoStreamUWC : MonoBehaviourPun
    {
        public static SYA_VideoStreamUWC Instance;
        //ȭ�� ���� ��ư�� ������
        //�ٸ� ������ ��ǻ�Ͱ� �ִ��� �˻�
        //-> photonView�� ����Ͽ� isMine
        //-> �÷��̾� ����Ʈ �˻�
        //-> �÷��̾� ī��Ʈ 2�� �̻��̶��
        //call����
        //ismine true�� Pc1
        //false�� Pc2
        //����Ʈ�� �������� ������ 
        //�ֵ� ������â�� �ҽ� �̹��� ����Ʈ�� �ִ´�

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
