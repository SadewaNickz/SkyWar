using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Kecepatan laju peluru

    void Update()
    {
        // Peluru terus bergerak lurus ke atas (Sumbu Y positif)
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Hancurkan peluru jika sudah keluar layar (misal Y > 10) 
        // Ini sangat penting agar memori RAM tidak penuh oleh peluru yang terbang tanpa batas
        if (transform.position.x > 15f)
        {
            Destroy(gameObject);
        }
    }

    // Fungsi ini dipanggil otomatis saat peluru menyentuh objek lain (yang Is Trigger-nya aktif)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Nanti kita beri tag "Enemy" pada pesawat musuh
        if (collision.CompareTag("Enemy")) 
        {
            Destroy(collision.gameObject); // Hancurkan musuhnya
            Destroy(gameObject);           // Hancurkan peluru ini
        }
    }
}
