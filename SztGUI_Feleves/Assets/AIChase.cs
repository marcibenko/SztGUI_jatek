using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private float distance;

    void Start()
    {
        
    }


    void Update()
    {
        //the commented sections are making it possible for the enemy to always face the player.
        //it is outcommented, since the current skin of the enemy has shadows init, making the turning weird.( in some angles, the shadows are on top of the enemy)

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        //direction.Normalize();
        //float angle= Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
