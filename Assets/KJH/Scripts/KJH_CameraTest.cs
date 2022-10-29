using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCompleteEventArgs
{
    public GameObject targetObject;
    public Vector3 position;
    public Quaternion quaternion;
}

public class KJH_CameraTest : MonoBehaviour
{
    public static event EventHandler<MoveCompleteEventArgs> EventHandeler_CameraMoveTarget;

    public GameObject camera;
    Transform targetObject;
    public float smoothTime = 0.3f;

    Vector3 velocity = Vector3.zero;

    public static bool isActive = false;
    public float zoomIn = -5f;

    Bounds boundsData;
    bool isBounds = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            Vector3 targetPosition;

            if(isBounds == false)
            {
                targetPosition = targetObject.TransformPoint(new Vector3(0, 10, zoomIn));
            }
            else
            {
                targetPosition = new Vector3(boundsData.center.z, boundsData.center.y + boundsData.size.y, boundsData.center.z - boundsData.size.z + zoomIn);
            }

            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, targetPosition, ref velocity, smoothTime);
            camera.transform.LookAt(targetObject);

            if (Vector3.Distance(targetPosition, camera.transform.position) < 0.1f)
            {
                MoveCompleteEventArgs args = new MoveCompleteEventArgs();
                args.targetObject = targetObject.gameObject;
                args.position = camera.transform.position;
                args.quaternion = camera.transform.rotation;
                //EventHandeler_CameraMoveTarget(this, args);

                Clear();
            }

        }
    }

    public void SetTarget(GameObject target, bool bounds = true)
    {
        if (target == null) return;
        isActive = true;
        targetObject = target.transform;

        if (bounds)
        {
            Bounds combinedBounds = new Bounds();
            var renderers = target.GetComponentsInChildren<Renderer>();

            foreach(var render in renderers)
            {
                combinedBounds.Encapsulate(render.bounds);
            }

            boundsData = combinedBounds;
            isBounds = true;
        }
        else
        {
            boundsData = new Bounds();
            isBounds = false;
        }
    }

    private void Clear()
    {
        smoothTime = 0.3f;
        isActive = false;
        //targetObject = null;
    }
}
