using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; 

    void Update()
    {
        // Arah Peluru
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Destroy bullet object
        if (transform.position.x > 15f)
        {
            Destroy(gameObject);
        }
    }

    // Peluru ketika terkena objek
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) 
        {
            GameManager.instance.AddScore(10);
            
            Destroy(collision.gameObject); 
            Destroy(gameObject);
        }
    }
}
