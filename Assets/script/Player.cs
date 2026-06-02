using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jf = 10f; // Coba mulai dari angka kecil dulu
    Rigidbody2D rb;
    bool isJumpPressed; // Variabel bantuan buat nyimpen input

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Cek input tombol di Update supaya nggak ada yang terlewat
        if (Input.GetButtonDown("Jump"))
        {
            isJumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");

        // Pakai rb.linearVelocity (Unity 6) atau rb.velocity (versi lama)
        rb.linearVelocity = new Vector2(horiz * speed, rb.linearVelocity.y);

        // Eksekusi loncat di sini
        if (isJumpPressed)
        {
            // Pakai ForceMode2D.Impulse biar loncatnya instan dan enak
            rb.AddForce(Vector2.up * jf, ForceMode2D.Impulse);
            isJumpPressed = false; // Reset biar nggak loncat terus-terusan
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tikus"))
        {
            Destroy(collision.gameObject);
        }
    }
}