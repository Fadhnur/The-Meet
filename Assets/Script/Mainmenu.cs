using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Mainmenu : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject loadingVP;

    public void NewGame()
    {
        loadingVP.SetActive(true);

        // Menghubungkan event saat video selesai diputar
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.Play();
    }  

    // Method yang dipanggil saat video selesai diputar
    private void OnVideoEnd(VideoPlayer vp)
    {
        // Memuat scene berikutnya setelah video selesai diputar
        SceneManager.LoadScene(1);
    }
    
    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Start() {
        Cursor.visible = true;
        loadingVP.SetActive(false);
    }

}
