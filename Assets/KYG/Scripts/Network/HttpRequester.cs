using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
//게시물 정보
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
    //요청타입(GET,POST,PUT,DELETE)
    public RequestType requestType;
    //응답이 왔을 때 호출해줄 함수(Action)
    //Action : 함수를 넣을 수 있는 자료형
    //반환자료형 void, 매개변수 없는 함수를 넣을 수 있다
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
