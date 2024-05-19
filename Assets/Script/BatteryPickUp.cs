using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickUp : MonoBehaviour
{
    private bool inReach;

    public GameObject pickUpText;
    private GameObject flashlight;

    // Menyimpan referensi ke objek baterai yang berada dalam jangkauan
    private GameObject batteryInReach; 

    // public AudioSource pickUpSound;

    void Start()
    {
        inReach = false;
        pickUpText.SetActive(false);
        flashlight = GameObject.Find("Flashlight");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Battery")
        {
            inReach = true;
            pickUpText.SetActive(true);
            batteryInReach = other.gameObject; // Simpan referensi ke objek baterai
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Battery")
        {
            inReach = false;
            pickUpText.SetActive(false);
            batteryInReach = null; // Hapus referensi saat baterai keluar dari jangkauan
        }
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && inReach && batteryInReach != null)
        {
            flashlight.GetComponent<FlashLight>().batteries += 1;
            // pickUpSound.Play();
            inReach = false;
            pickUpText.SetActive(false);
            Destroy(batteryInReach);
            batteryInReach = null;  // Hapus referensi setelah menghancurkan objek
        }
        
    }
}
