using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;

    [SerializeField] private ChefObstacle obstacle;
    [NonSerialized] public float obstacleMoveSpeed = 7.5f;
    [NonSerialized] public float spawnRate = 5f;

    [SerializeField] private GameObject player;
    private Clickable reticleSpawner;

    private void Start()
    {
        reticleSpawner = player.GetComponent<Clickable>();
        StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnObstacle() 
    {
        while (true)
        {
            Transform spawnPoint = UnityEngine.Random.Range(0, 2) == 0 ? point1 : point2;
            Transform destroyPoint = spawnPoint == point1 ? point2 : point1;

            ChefObstacle newObstacle = Instantiate(obstacle, spawnPoint.position, Quaternion.identity);
            newObstacle.destroyPoint = destroyPoint;
            newObstacle.moveSpeed = obstacleMoveSpeed;
            newObstacle.reticleSpawner = reticleSpawner;
            spawnRate = UnityEngine.Random.Range(2f, 8f);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
