using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // WAJIB untuk membaca komponen TextMeshPro UI
using UnityEngine.SceneManagement; // WAJIB untuk fitur pindah Level / Scene

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 12f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private int score = 0;
    private Vector3 titikAwalRespawn;
    private List<GameObject> daftarSemuaTikus = new List<GameObject>();

    [Header("Sistem Skor UI")]
    [SerializeField] private TextMeshProUGUI kotakTeksSkor;

    [Header("Sistem Pop-Up Menang")]
    [SerializeField] private GameObject panelPopUpMenang;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        titikAwalRespawn = transform.position;

        GameObject[] objekTikus = GameObject.FindGameObjectsWithTag("Tikus");
        daftarSemuaTikus.AddRange(objekTikus);

        if (panelPopUpMenang != null)
        {
            panelPopUpMenang.SetActive(false);
        }

        score = 0;
        UpdateScoreUI();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    // UPDATE FIX LONCAT: Menggunakan Raycast agar akurat mendeteksi tanah tingkat atas
    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.3f, groundLayer);
        Debug.DrawRay(groundCheck.position, Vector2.down * 0.3f, Color.red); // Garis merah pemantau di tab Scene
        return hit.collider != null;
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tikus"))
        {
            score += 10;
            UpdateScoreUI();
            collision.gameObject.SetActive(false);
        }

        if (collision.CompareTag("Air") || collision.CompareTag("Jamur"))
        {
            RespawnPlayer();
        }

        if (collision.CompareTag("Batu"))
        {
            GameSelesaiMenang();
        }
    }

    private void RespawnPlayer()
    {
        transform.position = titikAwalRespawn;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        score = 0;
        UpdateScoreUI();

        foreach (GameObject tikusSatuan in daftarSemuaTikus)
        {
            if (tikusSatuan != null)
            {
                tikusSatuan.SetActive(true);
            }
        }
    }

    private void GameSelesaiMenang()
    {
        if (panelPopUpMenang != null)
        {
            panelPopUpMenang.SetActive(true);
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        this.enabled = false;
    }

    private void UpdateScoreUI()
    {
        if (kotakTeksSkor != null)
        {
            kotakTeksSkor.text = "SCORE: " + score.ToString();
        }
    }

    // =================================================================
    // KODE BARU: FUNGSI KHUSUS TOMBOL UI (HOME & LEVEL 2)
    // =================================================================

    // Fungsi yang akan dipanggil oleh Tombol HOME
    public void PindahKeHome()
    {
        // Membuka kembali scene utama (sesuaikan "SampleScene" dengan nama scene utamamu)
        SceneManager.LoadScene("SampleScene");
    }

    // Fungsi yang akan dipanggil oleh Tombol Angka 2
    public void PindahKeLevel2()
    {
        // Membuka scene level 2 (Pastikan kamu sudah membuat scene baru bernama "lvl 2")
        SceneManager.LoadScene("lvl 2");
    }
    // Fungsi baru untuk Tombol Angka 3 (Level 3)
    public void PindahKeLevel3()
    {
        // Membuka scene level 3 (Pastikan nama file scene lu sama persis, misal "lvl 3")
        SceneManager.LoadScene("lvl 3");
    }
}