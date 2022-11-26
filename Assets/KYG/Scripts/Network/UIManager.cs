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
        //서버에 게시물 조회 요청(posts/1, GET)
        //HttpRequester를 생성
        HttpRequester requester = new HttpRequester();
        ///posts/1, GET, 완료됐을 때 호출되는 함수
        requester.url = "http://localhost:8090/api/objects/find?str=moon";
        requester.requestType = RequestType.GET;
        requester.onComplete = OnCompleteGetPost;
        HttpManager.instance.SendRequest(requester);
    }

    
    public void OnCompleteGetPost(DownloadHandler handler)
    {
        PostData postData = JsonUtility.FromJson<PostData>(handler.text);
        //타이틀 출력
        //내용 출력
        print("조회완료");
    }

    public void OnClickGetPostAll()
    {
        //서버에 게시물 조회 요청(posts/1, GET)
        //HttpRequester를 생성
        HttpRequester requester = new HttpRequester();
        ///posts/1, GET, 완료됐을 때 호출되는 함수
        requester.url = "https://jsonplaceholder.typicode.com/posts";
        requester.requestType = RequestType.GET;
        requester.onComplete = OnCompleteGetPostAll;
        HttpManager.instance.SendRequest(requester);
    }


    public void OnCompleteGetPostAll(DownloadHandler handler)
    {
        //배열 데이터를 키값에 넣는다.
        string s = "{\"data\":" + handler.text + "}";
        //data : List<PostData>
        PostDataArray array = JsonUtility.FromJson<PostDataArray>(s);
        for(int i = 0; i<array.data.Count; i++)
        {
            print(array.data[i].id);
            print(array.data[i].title);
            print(array.data[i].body);
        }
        //타이틀 출력
        //내용 출력
        print("조회완료");
    }
}
