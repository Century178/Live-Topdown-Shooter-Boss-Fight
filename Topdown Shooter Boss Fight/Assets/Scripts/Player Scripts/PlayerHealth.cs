using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;

    [SerializeField] private TextMeshProUGUI healthText;

    private void Awake()
    {
        Time.timeScale = 1f;
        healthText.text = "HP: " + health.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            health--;
            healthText.text = "HP: " + health.ToString();
            if (health > 0)
            {
                return;
            }
            Cursor.visible = true;
            Destroy(gameObject);
            Time.timeScale = 0f;
        }
    }
}
