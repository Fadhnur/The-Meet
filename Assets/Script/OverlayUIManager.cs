using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OverlayUIManager : MonoBehaviour
{
    public GameObject controllUI; // Referensi ke panel overlay UI
    public GameObject iconHintUI;
    public KeyCode hideKey = KeyCode.Space; // Tombol untuk menyembunyikan overlay UI

    //public float displayTime = 3f; // Waktu tampil overlay UI (dalam detik)
    private void Start()
    {
        // Menampilkan overlay UI saat permainan dimulai
        controllUI.SetActive(true);
        iconHintUI.SetActive(true);
        Time.timeScale = 0f; // Menghentikan waktu permainan

        // Memulai coroutine untuk menampilkan overlay UI
        // StartCoroutine(DisplayOverlayUI());
    }

    private void Update()
    {
        // Menyembunyikan overlay UI saat tombol tertentu ditekan
        if (Input.GetKeyDown(hideKey))
        {
            controllUI.SetActive(false);
            iconHintUI.SetActive(false);
            Time.timeScale = 1f; // Mengaktifkan kembali waktu permainan
        }

        /*
        private IEnumerator DisplayOverlayUI(){
            controllUI.SetActive(true); //Menampilkan overlay UI
            Time.timeScale = 0f; // Menghentikan waktu permainan


            yield return new WaitForSecondsRealtime(displayTime); // Menunggu selama displayTime

            controllUI.SetActive(false); //Menyembunyikan overlay UI
            Time.timeScale = 1f; // Mengaktifkan kembali waktu permainan
        }
        */
    }
}
