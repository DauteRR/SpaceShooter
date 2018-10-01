using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject[] hazards; // Three types of asteroids and enemies
    public Vector3 spawnValues;
    public int hazardCount = 10;
    public float spawnWait = 0.4f;
    public float startWait = 1;
    public float waveWait = 0.5f;

    private int playerPunctuation;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private bool restart;
    private bool gameOver;

    void Start()
    {
        gameOver = false;
        restart = false;
        gameOverText.text = "";
        restartText.text = "";
        playerPunctuation = 0;
        UpdateScoreText();
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (restart && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; ++i)
            {
                Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazards[UnityEngine.Random.Range(0, hazards.Length)], spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);

                if (gameOver)
                {
                    restartText.text = "Press 'R' for restart";
                    restart = true;
                    break;
                }
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        playerPunctuation += scoreToAdd;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + playerPunctuation;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
