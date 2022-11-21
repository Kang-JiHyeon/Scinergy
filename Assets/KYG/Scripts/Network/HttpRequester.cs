using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
//�Խù� ����
public class PostData
{
    public int userId;
    public int id;
    public string title;
    public string body;
}
[Serializable]
public class PostDataArray
{
    public List<PostData> data;
}
public enum RequestType
{
    POST,
    GET,
    PUT,
    DELETE
}
public class HttpRequester : MonoBehaviour
{
    //url
    public string url;
    //��ûŸ��(GET,POST,PUT,DELETE)
    public RequestType requestType;
    //������ ���� �� ȣ������ �Լ�(Action)
    //Action : �Լ��� ���� �� �ִ� �ڷ���
    //��ȯ�ڷ��� void, �Ű����� ���� �Լ��� ���� �� �ִ�
    public Action<DownloadHandler> onComplete;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


}
