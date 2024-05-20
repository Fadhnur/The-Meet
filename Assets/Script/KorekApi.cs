using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KorekApi : MonoBehaviour
{
    public GameObject korekApi;
    public GameObject api;

    public AudioSource lighterSound;

    public bool isOn;

    private Pickup pickupScript; // Referensi ke skrip Pickup

    void Start()
    {
        isOn = false;
        api.SetActive(false);

        // Mendapatkan referensi ke skrip Pickup
        pickupScript = FindObjectOfType<Pickup>();
    }


    void Update()
    {
        //Mendapatkan objek yang sedang dipegang oleh pemain
        GameObject heldObject = pickupScript.GetHeldObject();

        if (Input.GetButtonDown("Fire1") && korekApi.activeInHierarchy && heldObject != null && heldObject.CompareTag("Fire"))     //mouse klik kiri
        {
            api.SetActive(true);
            lighterSound.Play();
            isOn = true;
        }

        else if (Input.GetButtonDown("Fire1") && isOn && heldObject != null && heldObject.CompareTag("Fire"))
        {
            return;
        }

        if (Input.GetButtonDown("Fire2") && korekApi.activeInHierarchy && isOn)     //mouse klik kanan
        {
            api.SetActive(false);
            isOn = false;
        }
    }
}
