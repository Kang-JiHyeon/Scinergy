using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickGetPost()
    {
        //������ �Խù� ��ȸ ��û(posts/1, GET)
        //HttpRequester�� ����
        HttpRequester requester = new HttpRequester();
        ///posts/1, GET, �Ϸ���� �� ȣ��Ǵ� �Լ�
        requester.url = "http://localhost:8090/api/objects/find?str=moon";
        requester.requestType = RequestType.GET;
        requester.onComplete = OnCompleteGetPost;
        HttpManager.instance.SendRequest(requester);
    }

    
    public void OnCompleteGetPost(DownloadHandler handler)
    {
        PostData postData = JsonUtility.FromJson<PostData>(handler.text);
        //Ÿ��Ʋ ���
        //���� ���
        print("��ȸ�Ϸ�");
    }

    public void OnClickGetPostAll()
    {
        //������ �Խù� ��ȸ ��û(posts/1, GET)
        //HttpRequester�� ����
        HttpRequester requester = new HttpRequester();
        ///posts/1, GET, �Ϸ���� �� ȣ��Ǵ� �Լ�
        requester.url = "https://jsonplaceholder.typicode.com/posts";
        requester.requestType = RequestType.GET;
        requester.onComplete = OnCompleteGetPostAll;
        HttpManager.instance.SendRequest(requester);
    }


    public void OnCompleteGetPostAll(DownloadHandler handler)
    {
        //�迭 �����͸� Ű���� �ִ´�.
        string s = "{\"data\":" + handler.text + "}";
        //data : List<PostData>
        PostDataArray array = JsonUtility.FromJson<PostDataArray>(s);
        for(int i = 0; i<array.data.Count; i++)
        {
            print(array.data[i].id);
            print(array.data[i].title);
            print(array.data[i].body);
        }
        //Ÿ��Ʋ ���
        //���� ���
        print("��ȸ�Ϸ�");
    }
}
