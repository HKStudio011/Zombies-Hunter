using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Spawn Enemy")]
public class SpawnEnemy : MonoBehaviour
{
    [Header("Enemy")]

    public GameObject Zombie;

    [Header("Time of Spawn")]
    public float MinTime = 0.5f;
    public float MaxTime = 1.0f;

    private GameObject[] spawnPoint;
    private float lastTime;
    private float spawnTIme;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectsWithTag("Spawn Point");
        UpdateSpawnTime();
    }

    void UpdateSpawnTime()
    {
        lastTime = Time.time;
        spawnTIme = Random.Range(MinTime, MaxTime);
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        if(lastTime + spawnTIme <= Time.time) 
        {
            if(spawnPoint == null || spawnPoint.Length == 0)
            {
                return;
            }
            var indexSpawnPoint = Random.Range(0,spawnPoint.Length);
            Instantiate(Zombie, spawnPoint[indexSpawnPoint].transform.position,Quaternion.identity);
            UpdateSpawnTime();
        }
    }
}
