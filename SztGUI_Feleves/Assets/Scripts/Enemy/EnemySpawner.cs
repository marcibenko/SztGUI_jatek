using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate;

    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private int whenComesRed;
    [SerializeField] private int whenComesPurple;

    [SerializeField] private bool canSpawn = true;
    private void Start()
    {
        StartCoroutine(AdjustSpawnRate());
        StartCoroutine(Spawner());
    }
    private IEnumerator AdjustSpawnRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            spawnRate -= 0.1f; 
            spawnRate = Mathf.Max(spawnRate, 0.1f); 
        }
    }
    private IEnumerator Spawner()
    {
        //WaitForSeconds wait = new WaitForSeconds(spawnRate);
        int count = 0;
        while (true) {
            yield return new WaitForSeconds(spawnRate);

            GameObject enemyToSpawn;
            count++;
            if (count == whenComesRed)
            {
                enemyToSpawn = enemyPrefabs[1];
            }
            else if (count == whenComesPurple)
            { 
                enemyToSpawn = enemyPrefabs[2];
                count = 0;
            }
            else
            {
                enemyToSpawn = enemyPrefabs[0];
            }
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity); 
        }
    }
}
