using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBomb : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private GameObject bullet;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Invoke("Divide", 2);
    }
    private void Divide()
    {
        for (int i = 0; i < 4; i++)
        {
            transform.Rotate(Vector3.forward * 90);
            Instantiate(bullet, transform.position, transform.localRotation);
        }
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Divide();
    }
}
