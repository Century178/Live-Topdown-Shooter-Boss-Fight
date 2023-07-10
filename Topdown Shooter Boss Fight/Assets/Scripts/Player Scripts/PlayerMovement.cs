using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float x, y;
    private Rigidbody2D rb;

    public static PlayerMovement Player { get; private set; }

    private void Awake()
    {
        if (Player != null) Destroy(gameObject);
        else Player = this;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(x, y).normalized * speed;
    }
}
