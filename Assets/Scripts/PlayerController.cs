using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Pengaturan Jari (MediaPipe)")]
    [Tooltip("Semakin kecil angka ini, semakin rapat jari harus mencubit")]
    public float pinchThreshold = 0.05f; 
    public float moveSpeed = 15f; // Kecepatan pesawat mengikuti tangan (agar mulus)

    [Header("Pengaturan Tembakan")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.3f; // Jeda antar tembakan (detik)
    private float nextFireTime = 0f;

    [Header("Pengaturan Suara")]
    public AudioSource audioSource;
    public AudioClip shootSound;

    // Fungsi ini yang nanti akan dipanggil secara terus-menerus oleh script MediaPipe
    public void UpdateHandTracking(Vector2 indexTip, Vector2 thumbTip)
    {
        // 1. Logika Pergerakan (Aiming)
        MoveTo(indexTip);

        // 2. Logika Menembak (Pinching)
        float pinchDistance = Vector2.Distance(indexTip, thumbTip);
        if (pinchDistance < pinchThreshold)
        {
            Shoot();
        }
    }

    private void MoveTo(Vector2 normalizedPos)
    {
        // MediaPipe membaca titik Y=0 di atas, sedangkan Unity Screen membaca Y=0 di bawah. 
        // Jadi sumbu Y harus kita balik (1 - Y).
        Vector3 screenPos = new Vector3(
            normalizedPos.x * Screen.width, 
            (1f - normalizedPos.y) * Screen.height, 
            10f // Jarak kamera
        );
        
        // Konversi koordinat layar menjadi koordinat Game (World Space)
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f; // Kunci sumbu Z agar pesawat tidak maju/mundur
        
        // Memindahkan pesawat dengan Lerp agar pergerakannya tidak patah-patah
        transform.position = Vector3.Lerp(transform.position, worldPos, Time.deltaTime * moveSpeed);
    }

    private void Shoot()
    {
        // Cek apakah cooldown tembakan sudah selesai dan peluru sudah disiapkan
        if (Time.time >= nextFireTime && bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            nextFireTime = Time.time + fireRate;

            if (audioSource != null && shootSound != null)
            {
            // Menggunakan PlayOneShot agar suara tembakan bisa bertumpuk jika kamu menembak dengan cepat
            audioSource.PlayOneShot(shootSound); 
            }
        }
    }
}