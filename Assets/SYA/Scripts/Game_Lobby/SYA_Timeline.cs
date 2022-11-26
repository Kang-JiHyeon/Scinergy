using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SYA_Timeline : MonoBehaviour
{
    public AudioSource blackBgSource;

    public TimelineAsset timelineAsset;
    public PlayableDirector playableDirector;

    // Start is called before the first frame update
    void Start()
    {

    }
    //마우스 클릭시 흐르는 시간
    float currentTime = 0;
    //더블 클릭 제한 시간
    float clickTime = 0.5f;
    //클릭 횟수
    int glassButtonOn = 0;
    //시네머신 플레이
    bool animationPlay;

    // Update is called once per frame
    void Update()
    {
        blackBgSource .volume= SYA_UI.SYA_UIManager.Instance.exEF;
        // TV 더블 클릭시 모드 실행
        if (Input.GetMouseButtonDown(0))
        {
            // 지현이는 귀엽고 깜찍하다.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //해당 오브젝트의 부모 이름이 TV라면
            RaycastHit raycastHit = new RaycastHit();
            Physics.Raycast(ray, out raycastHit);
            if (raycastHit.collider.transform.parent != null)
                if (raycastHit.collider.transform.parent.name == "Glass_01")
                {
                    if (!animationPlay)
                    {
                        glassButtonOn++;
                    }
                    print("clickGlass");
                }
        }
        if (glassButtonOn >= 1)
        {
            //클릭 후 시간이 흐른다
            currentTime += Time.deltaTime;
            //제한 시간이 되면 버튼을 누른 횟수와 시간이 0이 된다
            if (currentTime >= clickTime)
            {
                currentTime = 0;
                glassButtonOn = 0;
            }
            if (glassButtonOn >= 2)
            {
                currentTime = 0;
                glassButtonOn = 0;
                animationPlay = true;
                //만약 플레이어무브에서 신호를 보내면
                playableDirector.Play(timelineAsset);
                blackBgSource.Play();
            }
        }

        if (playableDirector.time >= playableDirector.duration)
        {
            playableDirector.Stop();
            blackBgSource.Stop();
            animationPlay = false;
            //플레이어에게 종료되었다는 신호 보내기
        }
    }
}
