using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ∏∂øÏΩ∫ »Ÿ∑Œ ¡‹¿Œ ¡‹æ∆øÙ «œ∞Ì ΩÕ¥Ÿ.
public class KJH_EclipseCamera : MonoBehaviour
{
    Camera cam;

    public float zoomSpeed = 1f;
    public float minFov = 20f;
    public float maxFov = 60f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.fieldOfView = maxFov;
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;

        if(scroll != 0f)
        {
            cam.fieldOfView += scroll;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFov, maxFov);
        }
    }
}
