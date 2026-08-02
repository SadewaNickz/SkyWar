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