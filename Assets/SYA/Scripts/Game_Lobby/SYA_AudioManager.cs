using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SYA_AudioManager : MonoBehaviour
{
    public static SYA_AudioManager instance;

    public AudioSource bgSource;
    public AudioSource clickSource;
    public AudioSource nextSource;

    public Canvas m_canvas;
    GraphicRaycaster m_gr;
    PointerEventData m_ped;

    string sceneName;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (clickSource != null)
        {
            //m_canvas = 자신이 사용하는 캔버스 넣기.
            m_gr = m_canvas.GetComponent<GraphicRaycaster>();
            m_ped = new PointerEventData(null);
        }
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (bgSource != null)
            bgSource.volume = SYA_UI.SYA_UIManager.Instance.exBG;
        if (clickSource != null)
        {
            if (!(sceneName.Contains("Avarta")|| sceneName.Contains("Room")))
                clickSource.volume = SYA_UI.SYA_UIManager.Instance.exEF;

            if (Input.GetMouseButtonDown(0))
            {
                if (sceneName.Contains("Sympo")&&!sceneName.Contains("Room")) return;
                m_ped.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                m_gr.Raycast(m_ped, results);

                foreach (RaycastResult ray in results)
                {
                    if (ray.gameObject.transform.GetComponent<Button>())
                    {
                        if (ray.gameObject.name.Contains("Right") || ray.gameObject.name.Contains("Left"))
                        {
                            print("On");
                            if (nextSource != null)
                                nextSource.Play();
                        }
                        else
                        {
                            clickSource.Play();
                        }
                    }
                }
            }
        }
    }
}
