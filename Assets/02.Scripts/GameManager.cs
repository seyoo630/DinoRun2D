using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public Transform[] spawnPoints;
    public GameObject[] obstacles;

    public float spawnInterval = 2f;
    public float spawnDefault = 0f;
    public bool isSpawning = true;
    // Start is called before the first frame update
    void Start()
    {
        SpawnObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        spawnDefault += Time.deltaTime;

        if(spawnDefault >= spawnInterval && isSpawning)
        {
            SpawnObstacle();
            spawnDefault = 0;
        }
    }

    void SpawnObstacle()
    {
        int RandomIndex = Random.Range(0, obstacles.Length);

        if(RandomIndex == 0)
        {
            Instantiate(obstacles[RandomIndex], spawnPoints[0].position, Quaternion.identity);
        }
        else if(1 <= RandomIndex && RandomIndex <=3)
        {
            Instantiate(obstacles[RandomIndex], spawnPoints[1].position, Quaternion.identity);
        }
        else
        {
            int randomSpawnIndex = Random.Range(2, spawnPoints.Length);
            Instantiate(obstacles[RandomIndex], spawnPoints[randomSpawnIndex].position, Quaternion.identity);
        }
    }
}
