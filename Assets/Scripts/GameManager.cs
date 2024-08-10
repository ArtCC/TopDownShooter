using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private int lives;

    public static GameManager Instance { get; private set;}

    public bool IsPlayerDead { get; private set;}
    
    private bool allWavesSpawmed;
    private int enemiesLeft;
    private float sceneRestartTime = 0.5f;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        UpdateLivesText();
    }

    public void SetAllWavesSpawmed() {
        allWavesSpawmed = true;
    }

    public void Die() {
        lives--;

        if (lives == 0) {
            ResetGame();
        } else {
            IsPlayerDead = true;
            
            StopEnemies();
            StopEnemiesSpawn();
            StartCoroutine(WaitAndRestart(sceneRestartTime));
        }
    }

    public void IncreaseEnemiesLeft() {
        enemiesLeft++;
    }

    public void DecreaseEnemiesLeft() {
        enemiesLeft--;

        if (enemiesLeft == 0 && allWavesSpawmed) {
            LoadNextScene();
        }
    }

    public void LoadNextScene() {
        Reset();

        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(activeSceneIndex);
    }

    private void Reset() {
        allWavesSpawmed = false;
        IsPlayerDead = false;
        enemiesLeft = 0;
    }

    private void ResetGame() {
        SceneManager.LoadScene(0);
        
        Destroy(gameObject);
        Destroy(AudioManager.Instance.gameObject);
    }

    private void UpdateLivesText() {
        livesText.text = "Lives: " + lives.ToString();
    }

    private void StopEnemies() {
        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();

        foreach (EnemyMovement enemy in enemies) {
            enemy.StopMovement();
        }
    }

    private void StopEnemiesSpawn() {
        Spanwner spanwner = FindAnyObjectByType<Spanwner>();
        spanwner.StopSpawn();
    }

    private IEnumerator WaitAndRestart(float restartTime) {
        yield return new WaitForSeconds(restartTime);

        UpdateLivesText();

        IsPlayerDead = false;

        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }
}