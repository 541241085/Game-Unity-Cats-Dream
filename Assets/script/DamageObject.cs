using UnityEngine;

public class DamageObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cek apakah yang nabrak itu Player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Ambil komponen PlayerHealth dari si player terus kurangin darahnya
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }
}