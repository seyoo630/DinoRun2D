using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("References")]
    public Transform[] spawnPoints;
    public GameObject[] obstacles;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScore;
    public GameObject gameOverPanel;
    public Button RestartButton;


    public float spawnInterval = 2f;
    public float spawnDefault = 0f;
    public bool isSpawning = true;
    public int mainScore;

    public static GameManager instance;

    private AudioSource gameOverAudio;

    private void Awake()
    {
        if (instance == null)
        {
                instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        gameOverAudio = GetComponent<AudioSource>();

        SpawnObstacle();
        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        RestartButton.onClick.AddListener(RestartGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOverPanel.activeSelf)
        {
            spawnDefault += Time.deltaTime;

            if (spawnDefault >= spawnInterval && isSpawning)
            {
                SpawnObstacle();
                spawnDefault = 0;
            }
        }
    }

    public void updateScore()
    {
        scoreText.SetText("Á¡¼ö: " + mainScore);
    }

    void SpawnObstacle()
    {
        int RandomIndex = Random.Range(0, obstacles.Length);

        if(RandomIndex == 0)
        {
            Instantiate(obstacles[RandomIndex], spawnPoints[0].position, Quaternion.identity);
        }
        else if(1 <= RandomIndex && RandomIndex <=3)
        {
            Instantiate(obstacles[RandomIndex], spawnPoints[1].position, Quaternion.identity);
        }
        else
        {
            int randomSpawnIndex = Random.Range(2, spawnPoints.Length);
            Instantiate(obstacles[RandomIndex], spawnPoints[randomSpawnIndex].position, Quaternion.identity);
        }
    }

    public void GameOver()
    {
        gameOverAudio.Play();

        Time.timeScale = 0f; //Unity ½Ã°£Èå¸§ ¸ØÃã

        if(mainScore > PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", mainScore);
        }

        
        bestScore.SetText("Best Score: " + PlayerPrefs.GetInt("BestScore").ToString());
        gameOverPanel.SetActive(true);
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
        
    }
}
