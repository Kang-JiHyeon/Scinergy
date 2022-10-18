using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Particle : MonoBehaviour
{

    public ParticleSystem particle;
    public Color cloudColor;
    [Range(-90f, 90f)]
    public float rotX,rotY,rotZ;
    public float angleB = 0;
    public float size;
    public float speed;
    public float fadeTime;

    // Start is called before the first frame update
    [System.Obsolete]
    void Awake()
    {
        rotX = particle.shape.rotation.x;
        rotY = particle.shape.rotation.y;
        rotZ = particle.shape.rotation.z;
        cloudColor = particle.startColor;
        angleB = particle.shape.angle;
        size = particle.startSize;
        speed = particle.startSpeed;
        fadeTime = particle.startLifetime;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        ParticleSystem.ShapeModule shapeModule = particle.shape;
        shapeModule.angle = angleB;
        shapeModule.rotation = new Vector3(rotX, rotY, rotZ);
        particle.startColor = cloudColor;
        particle.startSize = size;
        particle.startSpeed = speed;
        particle.startLifetime = fadeTime;
        
    }
}
