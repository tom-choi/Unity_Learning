using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;

    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public GameObject pauseScreen;
    private bool pasued;
    private int lives;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;

    void ChangePaused()
    {
        if (!pasued)
        {
            pasued = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pasued = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {}
    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
    }
    public void StartGame(int difficulty)
    {
        StartCoroutine(SpawnTarget());
        score = 0;
        spawnRate /= difficulty;
        scoreText.text = "Score: " + score;
        isGameActive = true;
        UpdateScore(0);
        UpdateLives(3);
        titleScreen.gameObject.SetActive(false);
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0,targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }
}
