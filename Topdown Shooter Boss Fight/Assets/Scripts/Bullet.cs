using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, 5);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (bossHealth != null)
        {
            bossHealth.TakeDamage(1);//1 should work
        }
        else if (playerHealth != null)
        {
            Debug.Log("ouch");
            //playerHealth take damage
        }

        Destroy(gameObject);
    }
}
