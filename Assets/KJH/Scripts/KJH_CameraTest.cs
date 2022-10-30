using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class KJH_CameraTest : MonoBehaviour
{
    KJH_SelectPlanet selectPlanet;
    public float rotateSpeed = 1f;
    public float scrollSpeed = 200f;

    public Transform target;

    public SphericalCoordinates sphericalCoordinates;
    //private Vector3 reverseDistance;
    //private Vector3 movePos = Vector3.zero;
    //Vector3 velocity = Vector3.zero;
    //public float zoomSmoothTime = 0.2f;

    [System.Serializable]
    public class SphericalCoordinates
    {
        public float _radius, _azimuth, _elevation;

        // target과의 거리
        public float radius
        {
            get { return _radius; }
            private set
            {
                _radius = Mathf.Clamp(value, _minRadius, _maxRadius);
            }
        }

        // 수평(방위각)
        public float azimuth
        {
            get { return _azimuth; }
            private set
            {
                _azimuth = Mathf.Repeat(value, _maxAzimuth - _minAzimuth);
            }
        }

        // 수직(양각)
        public float elevation
        {
            get { return _elevation; }
            private set
            {
                _elevation = Mathf.Clamp(value, _minElevation, _maxElevation);
            }
        }

        public float _minRadius = 3f;
        public float _maxRadius = 100f;

        public float minAzimuth = 0f;
        private float _minAzimuth;

        public float maxAzimuth = 360f;
        private float _maxAzimuth;

        public float minElevation = -89.5f;
        private float _minElevation;

        public float maxElevation = 89.5f;
        private float _maxElevation;

        public SphericalCoordinates() { }

        // 직교좌표 -> 구면좌표 초기값 설정
        public SphericalCoordinates(Vector3 cartesianCoordinate)
        {
            // 도수 -> 라디안으로 변환
            _minAzimuth = Mathf.Deg2Rad * minAzimuth;
            _maxAzimuth = Mathf.Deg2Rad * maxAzimuth;

            _minElevation = Mathf.Deg2Rad * minElevation;
            _maxElevation = Mathf.Deg2Rad * maxElevation;

            // 카메라와 원점과의 거리
            radius = cartesianCoordinate.magnitude;
            // 방위각
            azimuth = Mathf.Atan2(cartesianCoordinate.z, cartesianCoordinate.x);
            // 양각
            elevation = Mathf.Asin(cartesianCoordinate.y / radius);
        }

        // 구면좌표계 -> 직교좌표계
        public Vector3 toCartesian
        {
            get
            {
                float t = radius * Mathf.Cos(elevation);
                return new Vector3(t * Mathf.Cos(azimuth), radius * Mathf.Sin(elevation), t * Mathf.Sin(azimuth));
            }
        }

        public SphericalCoordinates Rotate(float newAzimuth, float newElevation)
        {
            azimuth += newAzimuth;
            elevation += newElevation;
            return this;
        }

        public SphericalCoordinates TranslateRadius(float x)
        {
            radius = x;
            return this;
        }
    }

    // Use this for initialization
    void Start()
    {
        selectPlanet = GetComponent<KJH_SelectPlanet>();
        sphericalCoordinates = new SphericalCoordinates(transform.position);
        transform.position = sphericalCoordinates.toCartesian + target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectPlanet.camaraTarget)
        {
            target = selectPlanet.camaraTarget;
        }

        Move();
        
        ZoomInOut();
   
    }

    void Move()
    {
        float h, v;

        bool anyMouseButton = Input.GetMouseButton(0) | Input.GetMouseButton(1) | Input.GetMouseButton(2);
        h = anyMouseButton ? Input.GetAxis("Mouse X") : 0f;
        v = anyMouseButton ? Input.GetAxis("Mouse Y") : 0f;


        // 회전
        if (h * h > Mathf.Epsilon || v * v > Mathf.Epsilon)
        {
            transform.position = sphericalCoordinates.Rotate(h * -rotateSpeed * Time.deltaTime, v * -rotateSpeed * Time.deltaTime).toCartesian + target.position + movePos;
        }

        // 줌인,줌아웃
        //float sw = -Input.GetAxis("Mouse ScrollWheel");
        //if (sw * sw > Mathf.Epsilon)
        //{
        //          transform.position = sphericalCoordinates.TranslateRadius(sw * Time.deltaTime * scrollSpeed).toCartesian ;
        //      }

        //if (KJH_UIManager.instance.isActiveInfo == false)
        //{

            transform.LookAt(target.position + movePos);

        //}
    }

    // 마우스 휠로 줌인/줌아웃
    // - 행성으로 이동 중이거나 마우스 클릭해서 회전 중일 경우는 실행되지 않는다.
    public float distance = 10f;
    public float zoomSmoothTime = 0.2f;
    Vector3 velocity = Vector3.zero;
    public float wheelspeed = 10.0f;
    public float minDistance = 3f;
    public float maxDistance = 200f;
    Vector3 reverseDistance;
    Vector3 movePos = Vector3.zero;
    void ZoomInOut()
    {
        // 마우스 휠 입력
        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelspeed;

        // 구면좌표계의 radius 설정
        sphericalCoordinates.TranslateRadius(distance);


        if (distance < minDistance) distance = minDistance;
        if (distance > maxDistance) distance = maxDistance;
        reverseDistance = new Vector3(0f, 0f, distance);

        //movePos = KJH_UIManager.instance.isActiveInfo ? transform.right * -1.5f : Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + movePos - transform.rotation * reverseDistance, ref velocity, zoomSmoothTime);



    }
}
