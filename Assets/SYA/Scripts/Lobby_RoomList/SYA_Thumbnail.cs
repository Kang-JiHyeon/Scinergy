using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_Thumbnail : MonoBehaviour
{
    // 이미지 적용할 곳
    public Image thumbnail;
    //이미지 스프라이트 소스, 목록
    public List<Sprite> thumbnails = new List<Sprite>();
    //생성되는 곳 부모
    public Transform content;
    //생성할 아이템
    public GameObject item;
    //완료 버튼 누르면 창닫기

    public void OnClickTh()
    {
        gameObject.SetActive(false);
    }

// Start is called before the first frame update
    void Start()
    {
        SetTh();
    }

    //소스리스트대로 변경
    void SetTh()
    {
        for(int i=0; i<thumbnails.Count; ++i)
        {
            GameObject go = Instantiate(item, content);
            go.GetComponent<Image>().sprite = thumbnails[i];
        }
    }

/*    // Update is called once per frame
    void Update()
    {
        
    }*/
}
