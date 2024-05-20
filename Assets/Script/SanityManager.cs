using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class SanityManager : MonoBehaviour
{
    Slider sanitySlider;
    public int fullSanity;
    public int difficulty;
    float percent;
    //float currentSanity;
    private GameObject flashlight;

    public PostProcessProfile profile;
    Vignette vignette;
    DepthOfField depthOfField;
    public GameObject kunti; // Objek yang memiliki animasi jumpscare
    public Transform playerCamera; // Transform dari kamera pemain
    public float jumpscareDuration = 1.0f; // Durasi jumpscare dalam detik

    private bool isJumpscaring = false;
    public AudioSource jumpscareSFX;
    public string deathScene;

    // Start is called before the first frame update
    void Start()
    {
        flashlight = GameObject.Find("Flashlight");
        //Menampilkan ui slider untuk sanity
        profile.TryGetSettings(out vignette);
        profile.TryGetSettings(out depthOfField);

        sanitySlider = GetComponent<Slider>();
        sanitySlider.maxValue = fullSanity;
        sanitySlider.value = fullSanity;

        //currentSanity = fullSanity;

        vignette.intensity.value = 0;
        depthOfField.active = false;    // Mulai dengan Depth of Field tidak aktif

        //StartCoroutine(LoseSanity());
    }

    void Update()
    {
        if (flashlight.GetComponent<FlashLight>().off == true)
        {
            StartCoroutine(LoseSanity());
        }

        else if(flashlight.GetComponent<FlashLight>().on == true)
        {
            Affectsanity(1000);

            if (sanitySlider.value <= 800000)
            {
                Affectsanity(2000);
            }
            else if(sanitySlider.value == 100000)
            {
                Affectsanity(3000);
            }
            else if (sanitySlider.value >= 90000)
            {
                Affectsanity(4000);
            }
        }
    }
   

    //animasi mengurangi slider pada ui sanity
    IEnumerator LoseSanity()
    {
        while(sanitySlider.value > 0)
        {
            sanitySlider.value -= 1f * difficulty;
            float newValue = (sanitySlider.value - sanitySlider.maxValue) * -1;
            percent = newValue / sanitySlider.maxValue;
            vignette.intensity.value = percent;

            // Aktifkan efek blur berdasarkan sanity
            if (depthOfField != null)
            {
                depthOfField.active = true;
                depthOfField.aperture.value = Mathf.Lerp(32f, 1.4f, percent);
                depthOfField.focalLength.value = Mathf.Lerp(300f, 50f, percent);
            }

            // Cek sanity dan tampilkan jumpscare jika kurang dari 1000
            if (sanitySlider.value < 10000 && !isJumpscaring)
            {
                StartCoroutine(TriggerJumpscare());
            }

            yield return null;
        }
        // Hentikan semua coroutine yang mungkin berjalan
        StopAllCoroutines();
        // Pindah ke death scene segera setelah sanity habis
        SceneManager.LoadScene(deathScene);

        /*while (currentSanity > 0)
        {
            currentSanity -= 2f * difficulty;

            percent = currentSanity / fullSanity;
            vignette.intensity.value = percent;

            yield return null;
        }
        SceneManager.LoadScene(deathScene);*/
    }

    public void Affectsanity(float value)
    {
        sanitySlider.value += value;
        // Debug.Log("Sanity incrased by : " + value);

         // Matikan efek blur jika sanity sudah penuh
        if (sanitySlider.value == sanitySlider.maxValue)
        {
            depthOfField.active = false;
        }
    }

    private IEnumerator TriggerJumpscare()
    {
        isJumpscaring = true;

        if (kunti != null)
        {
            kunti.SetActive(true); // Aktifkan objek kunti

            // Tempatkan kunti di depan kamera pemain
            kunti.transform.position = playerCamera.position + playerCamera.forward * 2.0f; // Sesuaikan jarak sesuai keinginan
            kunti.transform.LookAt(playerCamera); // Pastikan kunti menghadap ke kamera pemain

            Animator kuntiAnimator = kunti.GetComponent<Animator>();
            if (kuntiAnimator != null)
            {
                kuntiAnimator.SetTrigger("jumpscare"); // Pastikan Anda memiliki trigger "PlayJumpscare" di Animator
                jumpscareSFX.Play();
            }
        }

        yield return new WaitForSeconds(jumpscareDuration);

        if (kunti != null)
        {
            kunti.SetActive(false); // Sembunyikan kunti setelah jumpscare
        }

        isJumpscaring = false;
    }
}
