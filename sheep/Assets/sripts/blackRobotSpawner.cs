using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackRobotSpawner : MonoBehaviour
{
    public float spawnRate;
    public GameObject mob;
    public GameObject particles;
    public GameObject laser;
    public Transform spawnPoint;
    public Transform particlesPoint;
    void Start()
    {
        InvokeRepeating("Spawn", 0f, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Spawn()
    {
        GameObject instance = Instantiate(particles, particlesPoint.position, transform.rotation);
        Destroy(instance, 0.5f);
        GameObject laseR = Instantiate(laser, particlesPoint.position, transform.rotation);
        Destroy(laseR, 0.5f);
        Instantiate(mob, spawnPoint.position, Quaternion.Euler(0, -180, 0));
    }
}
