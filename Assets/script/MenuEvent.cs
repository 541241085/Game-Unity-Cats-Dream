using UnityEngine;
using UnityEngine.SceneManagement; // WAJIB ADA INI

public class MenuEvent : MonoBehaviour
{
    // Fungsi ini yang lo panggil di tombol
    public void LoadLevel(int levelIndex)
    {
        // Load berdasarkan nomor urut di Build Settings
        SceneManager.LoadScene(levelIndex);
    }

    // Atau kalau mau pake nama file scene-nya
    public void LoadLevelByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}