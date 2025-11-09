using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;

public sealed class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public static bool gameIsPaused = false;
    public static bool playerIsDead = false;
    public static bool gameIsOver = false;
    public static bool isLoading = false;
    public static bool showFPS = true;
    public static bool godMode = false;
    public static int score = 0;
    public static int highScore = 0;
    public static int lives = 3;
    public static int level = 1;
    public static float masterVolume = 1f;
    public static float mouseSensitivity = 2f;
    public static string currentWeapon = "Pistol";

    public static Dictionary<string, int> AmmoDict = new() {
        { "Pistol", 99 },
        { "Rifle", 0 },
        { "RocketLauncher", 0 }
    };

    public CameraControl cameraControl;
    public Player player;
    public GameObject enemyPrefab;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public AudioClip jumpSound;
    public AudioClip shootSound;
    public AudioClip dieSound;
    public AudioSource musicSource;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI highScoreText;
    public Slider volumeSlider;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    private float nextSpawn = 0;
    public Transform[] spawnPoints; // assigned in Inspector

    public float GetPlayerHealth => player.healthBar.fillAmount;
    public float SetPlayerHealth => player.healthBar.fillAmount;

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Start() {
        RefreshUI();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(gameIsPaused) {
                UnPause();
            }
            else {
                Pause();
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            godMode = !godMode;
            Debug.Log("GodMode " + godMode);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            score += 1000;
            RefreshUI();
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            PlayerJump();
        }

        if(Input.GetKeyDown(KeyCode.E)) {
            PlayerShoot();
        }

        if(showFPS && Time.frameCount % 10 == 0) {
            //Debug.Log("FPS: " + (1f / Time.deltaTime).ToString("F1"));
        }

        if(!gameIsOver && !gameIsPaused && Time.time > nextSpawn) {
            nextSpawn = Time.time + Random.Range(1f, 3f);
            int r = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab, spawnPoints[r].position, Quaternion.identity);
        }

        if(!gameIsPaused && !playerIsDead) {
            float mx = Input.GetAxis("Mouse X") * mouseSensitivity;
            float my = Input.GetAxis("Mouse Y") * mouseSensitivity;
        }

        if(score >= 5000 && !gameIsOver) {
            Victory();
        }
    }

    private void RefreshUI() {
        scoreText.text = score.ToString();
        livesText.text = lives.ToString();
        weaponText.text = currentWeapon;
        ammoText.text = AmmoDict[currentWeapon].ToString();
        highScoreText.text = highScore.ToString();
    }

    public void Pause() {
        gameIsPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        musicSource.Pause();
        Cursor.lockState = CursorLockMode.None;
    }

    public void UnPause() {
        gameIsPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        musicSource.UnPause();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PlayerJump() {
        if(jumpSound != null) {
            musicSource.PlayOneShot(jumpSound); // re-using MusicSource because lazy
        }
    }

    public void PlayerShoot() {
        if(AmmoDict[currentWeapon] <= 0) {
            return;
        }
        AmmoDict[currentWeapon]--;
        RefreshUI();
        if(shootSound != null) {
            musicSource.PlayOneShot(shootSound);
        }
        // spawn bullet
        Instantiate(bulletPrefab, cameraControl.CurrentCamera.transform.position + cameraControl.CurrentCamera.transform.forward, cameraControl.CurrentCamera.transform.rotation);
    }

    public void DamagePlayer(int dmg) {
        if(godMode) {
            return;
        }
        player.healthBar.fillAmount -= GetDamagePercent(dmg);
        if(player.healthBar.fillAmount <= 0) {
            KillPlayer();
        }
    }

    public void DamagePlayer(float dmg) {
        if(godMode) {
            return;
        }
        player.healthBar.fillAmount -= GetDamagePercent(dmg);
        if(player.healthBar.fillAmount <= 0) {
            KillPlayer();
        }
    }

    private float GetDamagePercent(int dmg) {
        return dmg / 1f;
    }

    private float GetDamagePercent(float dmg) {
        return dmg / 1f;
    }

    private void KillPlayer() {
        playerIsDead = true;
        lives--;
        if(dieSound) {
            musicSource.PlayOneShot(dieSound);
        }
        if(lives <= 0) {
            GameOver();
        }
        else {
            Invoke("RespawnPlayer", 2f);
        }
    }

    private void RespawnPlayer() {
        player.healthBar.fillAmount = 1f;
        playerIsDead = false;
        RefreshUI();
    }

    public void AddScore(int s) {
        score += s;
        if(score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        RefreshUI();
    }

    private void GameOver() {
        gameIsOver = true;
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    private void Victory() {
        gameIsOver = true;
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadNextLevel() {
        level++;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level2" + level);
        // forgot to reset half the state…
    }

    public void OnVolumeChanged(float v) {
        masterVolume = v;
        musicSource.volume = v;
    }

    public void QuitGame() {
        Application.Quit();
    }
}
