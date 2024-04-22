using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed = 10f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 3f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime); //not sure about this
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("RedEnemy")&& !collision.gameObject.CompareTag("BlueEnemy"))
        {
            CapsuleCollider2D capsuleCollider = collision.attachedRigidbody ? collision.attachedRigidbody.GetComponent<CapsuleCollider2D>() : collision.GetComponent<CapsuleCollider2D>();
            if (capsuleCollider != null)
            {
                rb.velocity = Vector2.zero;
                Destroy(gameObject);
            }
        }
    }
}
