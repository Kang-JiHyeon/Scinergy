using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KJH_Orbit : MonoBehaviour
{
    float G = 100f;
    GameObject[] celestials;

    // Start is called before the first frame update
    void Start()
    {
        // 천체 배열
        celestials = GameObject.FindGameObjectsWithTag("Celestial");
    }

    // 중력 작용
    public void Gravity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized * (G * (m1 * m2) / (r * r)));
                }
            }
        }
    }

    // 속도
    public void InitialVelocity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);
                    a.transform.LookAt(b.transform.position);

                    a.GetComponent<Rigidbody>().velocity += a.transform.right * MathF.Sqrt((G * m2) / r);
                }
            }
        }
    }
}
