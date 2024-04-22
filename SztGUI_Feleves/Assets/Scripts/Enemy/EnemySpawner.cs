using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate=1f;

    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private int whenComesRed;
    [SerializeField] private int whenComesPurple;

    [SerializeField] private bool canSpawn = true;
    private void Start()
    {
        StartCoroutine(Spawner());
    }
    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        int count = 0;
        while (true) {
            yield return wait;

            GameObject enemyToSpawn;
            count++;
            //if (count==whenComesRed)
            //{
            //    enemyToSpawn = enemyPrefabs[1];
            //    count = 1;
            //}
            //else
            //{
            //    enemyToSpawn= enemyPrefabs[0];
            //    count++;
            //}
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
