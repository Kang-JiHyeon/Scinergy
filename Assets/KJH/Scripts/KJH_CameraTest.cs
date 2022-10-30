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

        // target���� �Ÿ�
        public float radius
        {
            get { return _radius; }
            private set
            {
                _radius = Mathf.Clamp(value, _minRadius, _maxRadius);
            }
        }

        // ����(������)
        public float azimuth
        {
            get { return _azimuth; }
            private set
            {
                _azimuth = Mathf.Repeat(value, _maxAzimuth - _minAzimuth);
            }
        }

        // ����(�簢)
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

        // ������ǥ -> ������ǥ �ʱⰪ ����
        public SphericalCoordinates(Vector3 cartesianCoordinate)
        {
            // ���� -> �������� ��ȯ
            _minAzimuth = Mathf.Deg2Rad * minAzimuth;
            _maxAzimuth = Mathf.Deg2Rad * maxAzimuth;

            _minElevation = Mathf.Deg2Rad * minElevation;
            _maxElevation = Mathf.Deg2Rad * maxElevation;

            // ī�޶�� �������� �Ÿ�
            radius = cartesianCoordinate.magnitude;
            // ������
            azimuth = Mathf.Atan2(cartesianCoordinate.z, cartesianCoordinate.x);
            // �簢
            elevation = Mathf.Asin(cartesianCoordinate.y / radius);
        }

        // ������ǥ�� -> ������ǥ��
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


        // ȸ��
        if (h * h > Mathf.Epsilon || v * v > Mathf.Epsilon)
        {
            transform.position = sphericalCoordinates.Rotate(h * -rotateSpeed * Time.deltaTime, v * -rotateSpeed * Time.deltaTime).toCartesian + target.position + movePos;
        }

        // ����,�ܾƿ�
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

    // ���콺 �ٷ� ����/�ܾƿ�
    // - �༺���� �̵� ���̰ų� ���콺 Ŭ���ؼ� ȸ�� ���� ���� ������� �ʴ´�.
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
        // ���콺 �� �Է�
        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelspeed;

        // ������ǥ���� radius ����
        sphericalCoordinates.TranslateRadius(distance);


        if (distance < minDistance) distance = minDistance;
        if (distance > maxDistance) distance = maxDistance;
        reverseDistance = new Vector3(0f, 0f, distance);

        //movePos = KJH_UIManager.instance.isActiveInfo ? transform.right * -1.5f : Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + movePos - transform.rotation * reverseDistance, ref velocity, zoomSmoothTime);



    }
}
