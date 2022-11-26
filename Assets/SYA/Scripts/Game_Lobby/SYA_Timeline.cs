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
    //���콺 Ŭ���� �帣�� �ð�
    float currentTime = 0;
    //���� Ŭ�� ���� �ð�
    float clickTime = 0.5f;
    //Ŭ�� Ƚ��
    int glassButtonOn = 0;
    //�ó׸ӽ� �÷���
    bool animationPlay;

    // Update is called once per frame
    void Update()
    {
        blackBgSource .volume= SYA_UI.SYA_UIManager.Instance.exEF;
        // TV ���� Ŭ���� ��� ����
        if (Input.GetMouseButtonDown(0))
        {
            // �����̴� �Ϳ��� �����ϴ�.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //�ش� ������Ʈ�� �θ� �̸��� TV���
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
            //Ŭ�� �� �ð��� �帥��
            currentTime += Time.deltaTime;
            //���� �ð��� �Ǹ� ��ư�� ���� Ƚ���� �ð��� 0�� �ȴ�
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
                //���� �÷��̾�꿡�� ��ȣ�� ������
                playableDirector.Play(timelineAsset);
                blackBgSource.Play();
            }
        }

        if (playableDirector.time >= playableDirector.duration)
        {
            playableDirector.Stop();
            blackBgSource.Stop();
            animationPlay = false;
            //�÷��̾�� ����Ǿ��ٴ� ��ȣ ������
        }
    }
}
