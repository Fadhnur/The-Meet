using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KorekApi : MonoBehaviour
{
    public GameObject korekApi;
    public GameObject api;

    public AudioSource lighterSound;

    public bool isOn;

    void Start()
    {
        isOn = false;
        api.SetActive(false);
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1") && korekApi.activeInHierarchy)     //mouse klik kiri
        {
            api.SetActive(true);
            lighterSound.Play();
            isOn = true;
        }

        else if (Input.GetButtonDown("Fire1") && isOn)
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
