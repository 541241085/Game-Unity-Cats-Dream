using UnityEngine;
using UnityEngine.SceneManagement; // Buat restart level

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    void Start()
    {
        // Pas game mulai, nyawa diisi penuh
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Darah Player: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Mati!");
        // Restart level yang sekarang lagi dimainin
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Optional: Kalau mau bikin sistem jatuh ke jurang langsung mati
    private void Update()
    {
        if (transform.position.y < -10f) // Angka -10 sesuaikan sama kedalaman jurang lo
        {
            Die();
        }
    }
}