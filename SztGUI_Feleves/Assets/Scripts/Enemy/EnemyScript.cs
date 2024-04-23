using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //public GameObject player;
    public Transform player;
    public float speed;
    [SerializeField] private GameObject onDestroyPrefab;
    private float distance;

    void Start()
    {
        
    }


    void Update()
    {
        if(!player)GetTarget();


        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

    }
    private void GetTarget()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnDestroy()
    {
            Instantiate(onDestroyPrefab, transform.position, Quaternion.identity);
    }
}
