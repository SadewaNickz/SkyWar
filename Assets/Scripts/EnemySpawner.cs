using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f; // Musuh muncul setiap 2 detik
    
    // Batas atas dan bawah layar agar musuh muncul di posisi acak
    public float minY = -4f; 
    public float maxY = 4f;

    void Start()
    {
        // Memulai perulangan memunculkan musuh
        InvokeRepeating("SpawnEnemy", 1f, spawnRate);
    }

    void SpawnEnemy()
    {
        // Mengacak posisi Y
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0f);
        
        // Memunculkan musuh
        Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
    }
}