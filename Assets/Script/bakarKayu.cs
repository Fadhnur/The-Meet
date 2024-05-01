using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bakarKayu : MonoBehaviour
{
    public GameObject wood, fire, intText;
    public AudioSource burn;
    public bool isBurn;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(true);
            if (isBurn == false) 
            {
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // dan cek inventory, apakah sudah ada korek
                    //if (korek di hand > 0)
                    //burn.Play();
                    fire.SetActive(true);
                    isBurn = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(false);
        }
    }
}
