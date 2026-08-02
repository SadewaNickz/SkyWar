using UnityEngine;
using UnityEngine.UI;
using TMPro; // Gunakan using TMPro; jika kamu memakai TextMeshPro
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    // Singleton script
    public static GameManager instance;

    [Header("Status Game")]
    public int score = 0;
    public int baseHealth = 3;

    [Header("Referensi UI")]
    public TextMeshProUGUI scoreText;   
    public TextMeshProUGUI healthText;  

    public GameObject gameOverPanel;

    [Header("Menu Utama & Pause")]
    public GameObject mainMenuPanel;
    public GameObject pausePanel;
    public GameObject playerPlane;

    [Header("Elemen Gameplay (Disembunyikan di Menu)")]
    public GameObject baseObject;
    public GameObject pauseButton;
    public GameObject enemySpawner;

    private void Start()
    {
        // Kondisi awal saat game dibuka: Munculkan Main Menu
        ReturnToMenu(); 
    }
    public void StartGame()
    {
        // Reset skor dan nyawa jika diperlukan
        score = 0;
        baseHealth = 3;
        if (scoreText != null) scoreText.text = "Skor: 0";
        if (healthText != null) healthText.text = "Nyawa: 3";

        mainMenuPanel.SetActive(false);
        pausePanel.SetActive(false);

        playerPlane.SetActive(true); 
        if (baseObject != null) baseObject.SetActive(true);
        if (pauseButton != null) pauseButton.SetActive(true);
        if (scoreText != null) scoreText.gameObject.SetActive(true);
        if (healthText != null) healthText.gameObject.SetActive(true);
        if (enemySpawner != null) enemySpawner.SetActive(true);
        Time.timeScale = 1f; // Jalankan waktu
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Hentikan waktu
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Lanjutkan waktu
    }

    public void ReturnToMenu()
    {
        mainMenuPanel.SetActive(true);
        pausePanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        
        playerPlane.SetActive(false); 
        if (baseObject != null) baseObject.SetActive(false);
        if (pauseButton != null) pauseButton.SetActive(false);
        if (scoreText != null) scoreText.gameObject.SetActive(false);
        if (healthText != null) healthText.gameObject.SetActive(false);
        if (enemySpawner != null) enemySpawner.SetActive(false);
        Time.timeScale = 0f; // Hentikan permainan latar belakang
    }

    public void QuitGame()
    {
        Debug.Log("Keluar dari Game!");
        Application.Quit();
    }
    
    private void Awake()
    {
        // Setup Singleton
        if (instance == null) instance = this;
        Time.timeScale = 1f;
    }

    public void AddScore(int points)
    {
        score += points;
        if (scoreText != null) scoreText.text = "Skor: " + score;
    }

    public void TakeDamage(int damage)
    {
        baseHealth -= damage;
        if (healthText != null) healthText.text = "Nyawa: " + baseHealth;

        if (baseHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        
        // Menghentikan waktu (pesawat, peluru, dan musuh akan berhenti bergerak)
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // Mengulang scene saat ini
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}