using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int health;

    private bool stage2;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private BossAI bossAI;

    private void Awake()
    {
        healthSlider.maxValue = maxHealth;
        health = maxHealth;
    }

    private void Update()
    {
        if (health <= 0 && !stage2)
        {
            stage2 = true;
            health = maxHealth;
            bossAI.chargeActive = true;
        }
        else if (health <= 0)
        {
            Destroy(gameObject);
        }

        healthSlider.value = health;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
        }
    }
}
