using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;

    private float invulnTime;
    [SerializeField] private float invulnLimit;

    private bool isDead;

    [SerializeField] private TextMeshProUGUI healthText;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Time.timeScale = 1f;
        healthText.text = "HP: " + health.ToString();
        invulnTime = invulnLimit;
    }

    private void Update()
    {
        if (invulnTime > 0)
        {
            invulnTime -= Time.deltaTime;
            Physics2D.IgnoreLayerCollision(3, 6);
            Physics2D.IgnoreLayerCollision(3, 7);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(3, 6, false);
            Physics2D.IgnoreLayerCollision(3, 7, false);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health--;
            healthText.text = "HP: " + health.ToString();
            invulnTime = invulnLimit;

            if (health <= 0) Death();
        }
    }

    private void Death()
    {
        Cursor.visible = true;
        Destroy(gameObject);
        Time.timeScale = 0f;
        RestartGame.Instance.gameOver = true;
    }
}
