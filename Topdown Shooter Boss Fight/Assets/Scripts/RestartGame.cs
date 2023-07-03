using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public static RestartGame Instance { get; private set; }

    public bool gameOver;

    private TextMeshPro text;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        text = GetComponent<TextMeshPro>();
        text.text = "";
        gameOver = false;
    }

    private void Update()
    {
        if (gameOver)
        {
            text.text = "Press R to restart";
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
