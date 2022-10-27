using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace uWindowCapture
{

    [RequireComponent(typeof(Image))]
    public class UwcWindowListItem : MonoBehaviour
    {
        //영아
        public GameObject slider;
        public Slider sliderScale;
        public GraphicRaycaster gr;
        //

        Image image_;
        [SerializeField] Color selected;
        [SerializeField] Color notSelected;

        public UwcWindow window { get; set; }
        public UwcWindowList list { get; set; }
        public UwcWindowTexture windowTexture { get; set; }

        [SerializeField] RawImage icon;
        [SerializeField] Text title;
        [SerializeField] Text x;
        [SerializeField] Text y;
        [SerializeField] Text z;
        [SerializeField] Text width;
        [SerializeField] Text height;
        [SerializeField] Text status;

        void Awake()
        {
            gr = GetComponentInParent<GraphicRaycaster>();
            image_ = GetComponent<Image>();
            image_.color = notSelected;
        }

        void Update()
        {
            if (window == null) return;

            if (!window.hasIconTexture && !window.isIconic)
            {
                icon.texture = window.texture;
            }
            else
            {
                icon.texture = window.iconTexture;
            }

            var windowTitle = window.title;
            title.text = string.IsNullOrEmpty(windowTitle) ? "-No Name-" : windowTitle;

            x.text = window.isMinimized ? "-" : window.x.ToString();
            y.text = window.isMinimized ? "-" : window.y.ToString();
            z.text = window.zOrder.ToString();

            width.text = window.width.ToString();
            height.text = window.height.ToString();

            status.text =
                window.isIconic ? "Iconic" :
                window.isZoomed ? "Zoomed" :
                "-";

        }

        public void OnClick()
        {
            //영아
            var ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(ped, results);
            print(results[0].gameObject.name);
            if (results[0].gameObject.name.Contains("Handle")) return;

            slider.SetActive(!slider.activeSelf);
            //

            if (windowTexture == null)
            {
                AddWindow();
            }
            else
            {
                RemoveWindow();
            }
            SliderValue();
        }

        void AddWindow()
        {
            var manager = list.windowTextureManager;
            windowTexture = manager.AddWindowTexture(window);
            image_.color = selected;
        }

        public void RemoveWindow()
        {
            var manager = list.windowTextureManager;
            manager.RemoveWindowTexture(window);
            windowTexture = null;
            image_.color = notSelected;
        }

        //영아
        void ScaleWindow(UwcWindowTexture windowTexture, bool useFilter, float scale)
        {
            windowTexture.scaleControlType = WindowTextureScaleControlType.BaseScale;
            windowTexture.scalePer1000Pixel = scale;
        }

        public void SliderValue()
        {
            if(windowTexture !=null)
            ScaleWindow(windowTexture, false, sliderScale.value);
        }
        //
    }

}