using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationZone : MonoBehaviour
{
    public GameObject constellation;
    public Terrain terrain;
    public GameObject gradient;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {        
        constellation.SetActive(true);
        terrain.enabled = false;
        gradient.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        constellation.SetActive(false);
        terrain.enabled = true;
        gradient.SetActive(true);
    }
}
