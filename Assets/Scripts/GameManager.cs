using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public float spawnRateMax = 2.5f;
    float spawnRate;
    int score, lives;
    public TextMeshProUGUI scoreText, livesText;
    public GameObject titleScreen, gameOverScreen, restartButton;
    public static GameManager instance;
    public bool isGameActive;
    public Slider volumeSlider;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Найдено больше одного примера Game manager");
            return;
        }
        instance = this;
    }
    
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
            Load();
        }
        else
        {
            Load();
        }
    }

    // Update is called once per frame
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = $"Score: {score}";
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            spawnRate = Random.Range(0, spawnRateMax);
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.gameObject.SetActive(true);
        restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScreen.SetActive(false);
        lives = 3;
        livesText.text = $"Lives: {lives}";
        volumeSlider.transform.parent.gameObject.SetActive(false);
    }

    public void HitThePlayer()
    {
        if (isGameActive)
        {
            lives--;
            livesText.text = $"Lives: {lives}";
            if (lives < 1)
            {
                GameOver();
            }
        }
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
}
