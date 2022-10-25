using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using SYA_UserInfoManagerSaveLoad;

public class SYA_AvartaSceneManager : MonoBehaviourPun
{
    public Camera camera;

    public static SYA_AvartaSceneManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    //아바타 선택창 화살표 클릭시 포지션 및 스케일 변경
    //화살표,포지션 및 스케일들,아바타유형들
    public Button right_Arrow;
    public Button left_Arrow;
    public List<GameObject> pos = new List<GameObject>();
    public List<Vector3> sca = new List<Vector3>();
    public List<GameObject> avatar = new List<GameObject>();

    //pos 1에 위치한 오브젝트 생성해서 왼쪽에 2.5로
    public Transform avatarPos;
    Vector3 avatarSca = new Vector3(2.5f, 2.5f, 2.5f);

    //동그라미 UI 기능. 자기가 담당한  아바타가 가운데라면 빨간 동그라미로
    //아바타n번이 가운데로 오면 n번 UI에게 빨간동그라미 싱호보내고
    //아니라면 하얀동그라미 신호 보내기
    public Action<int> WhiteClircle;

    // Start is called before the first frame update
    void Start()
    {
        ScaSet();
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonOn)
        {
            AvatarPosSca(0);
            AvatarPosSca(1);
            AvatarPosSca(2);
            AvatarPosSca(3);
            AvatarPosSca(4);
        }
        //레이어 바꿔주고
        //AvatarLayerChange(avatar[num].gameObject.transform.position.z, avatar[num].gameObject.transform);
        //컬링마스크 사옹
        camera.cullingMask = ~(1<< 18);
    }
    //업데이트 문에서 돌리는 함수 관할
    bool buttonOn;
    //버튼을 물러서 회전중인지를 나타내는 불값
    bool rotateIng;
    //Lerp이용해서 아바타의 위치와 크기 변화
    public void AvatarPosSca(int num)
    {
        //print(111);
        Vector3 avartaPos = avatar[num].transform.position;
        Vector3 avartaSca = avatar[num].transform.localScale;
        avartaPos = Vector3.Lerp(avartaPos, pos[num].transform.position, Time.deltaTime * 0.95f);
        avartaSca = Vector3.Lerp(avartaSca, sca[num], Time.deltaTime * 0.95f);
        avatar[num].transform.position= avartaPos;
        avatar[num].transform.localScale = avartaSca;
        if (Vector3.Distance(avatar[num].transform.position, pos[num].transform.position) < 0.2f
            && Vector3.Distance(avatar[num].transform.localScale, sca[num]) < 0.2f)
        {
            avatar[num].transform.position = pos[num].transform.position;
            avatar[num].transform.localScale = sca[num];
            
            //가운데에 있는지 분별하는 코드
            if (1.5f - avatar[num].transform.localScale.x < 0.1f)
            {
                //print(2222222);
                CreatAvatar(avatar[num].gameObject);
                WhiteClircle(num);
                rotateIng = false;
                buttonOn = false;
            }
        }
    }

    List<GameObject> avatarList = new List<GameObject>();
    GameObject avatarP;

    //가운데 있는 아바타 생성해서 왼쪽에 생성
    public void CreatAvatar(GameObject avatar)
    {
        if(avatarList.Count>0)
        {
            Destroy(GameObject.Find(avatarList[0].name).gameObject);
            avatarList.Clear();
        }
        avatarP= Instantiate(avatar);
        avatarList.Add(avatarP);
        avatarP.transform.localScale = avatarSca;
        avatarP.transform.position = avatarPos.position;
    }

    //아바타 세팅 실행
    public void AvatarSet()
    {
        buttonOn = true;
    }

    //처음 크기 조절
    public void ScaSet()
    {
        for (int i = 0; i < avatar.Count; ++i)
        {
            if (i == 1)
            {
                sca.Add(new Vector3(1.5f, 1.5f, 1.5f));
            }
            else
            {
                sca.Add(new Vector3(1, 1, 1));
            }
        }
        AvatarSet();
    }

    //아바타 선택창 화살표 클릭시 포지션 및 스케일 변경
    public void ArrowClickR()
    {
        if (rotateIng) return;
        AvatarRotate(false);
        rotateIng = true;
    }
    public void ArrowClickL()
    {
        if (rotateIng) return;
        AvatarRotate(true);
        rotateIng = true;
    }

    //아바타 이동을 위한 포지션 및 스케일 변경
    public void AvatarRotate(bool left)
    {
        if (left)
        {
            pos.Add(pos[0]);
            pos.RemoveAt(0);
            sca.Add(sca[0]);
            sca.RemoveAt(0);
        }
        else
        {
            pos.Insert(0, pos[avatar.Count - 1]);
            pos.RemoveAt(avatar.Count);
            sca.Insert(0, sca[avatar.Count - 1]);
            sca.RemoveAt(avatar.Count);
        }
        AvatarSet();
    }

    //아바타 선택 버튼을 물렀을 때
    //왼쪽에 저장된 아바타 유형을 인포에 저장
    public void AvatarSellect()
    {
        SYA_UserInfoSave.Instance.AvatarSave(avatarP.name.Substring(0,8));
        SYA_SceneChange.Instance.LoginScene();
    }

    public void AvatarLayerChange(float z, Transform avatar)
    {
        if(z>=-2)
        {
            avatar.gameObject.layer = 18;
            foreach(Transform child in avatar.transform)
            {
                AvatarLayerChange(avatar.transform.position.z, child);
            }
        }
        else
        {
            avatar.gameObject.layer = 17;
            foreach (Transform child in avatar.transform)
            {
                AvatarLayerChange(avatar.transform.position.z, child);
            }
        }
    }
}

