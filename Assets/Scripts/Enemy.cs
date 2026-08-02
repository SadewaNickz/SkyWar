using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        // Bergerak lurus ke arah kiri layar
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

        // Hancurkan musuh jika terlewat sampai ujung kiri luar layar (memori aman)
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Jika menabrak Base
        if (collision.CompareTag("Base"))
        {
            Debug.Log("Base Tertabrak! Nyawa berkurang.");
            Destroy(gameObject);
        }
    }
}