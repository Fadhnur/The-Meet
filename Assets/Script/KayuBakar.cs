using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayuBakar : MonoBehaviour
{
    public GameObject korekApi;
    public GameObject korekApiOBJ;
    public GameObject api;
    public GameObject lightText;
    public GameObject lightFire;
    public AudioSource lightSFX;

    public bool unlit;
    public bool inReach;
    public bool wet;

    // private GameObject currentReachObject;

    private Pickup pickupScript; // Referensi ke skrip Pickup

    //[SerializeField] GameObject obj;

    void Start()
    {
        unlit = true;
        api.SetActive(false);
        lightText.SetActive(false);
        wet = false;
        lightFire.SetActive(false);
        // currentReachObject = null;

        // Mendapatkan referensi ke skrip Pickup
        pickupScript = FindObjectOfType<Pickup>();
        
    }

    //Mendeteksi pohon 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach" && unlit)
        {
            Debug.Log("inReach");
            inReach = true;
            lightText.SetActive(true);
            //wet = false;
            // currentReachObject = other.gameObject; // Menyimpan referensi ke objek yang sedang di-reach
            
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach" && unlit)
        {
            //Debug.Log("inReach");
            inReach = false;
            lightText.SetActive(false);
            //wet = false;
            // currentReachObject = null; // Menghapus referensi ke objek yang sedang di-reach
        }
    }

    //Membakar objek 
    void OnCollisionEnter(Collision other)
    {
        //Membakar item dengan tag burn seperti item sesembahan
        if (other.gameObject.tag == "Burn" && unlit == false)
        {
             // Mendapatkan posisi dan rotasi dari objek yang terbakar
            Vector3 burnPosition = other.transform.position;
            Quaternion burnRotation = other.transform.rotation;

            Destroy(other.gameObject);
            
            GameObject explosion = Instantiate(api, burnPosition, burnRotation);
            Destroy(explosion, 0.75f);
            Debug.Log("burn");
        }

        //Mengaplikasikan jerigen pada kayu
        // if(other.gameObject.tag == "Jerigen")
        // {
        //     Destroy(other.gameObject);
        //     wet = true;
        // }

        
    }


    void Update()
    {
        //Mendapatkan objek yang sedang dipegang oleh pemain
        GameObject heldObject = pickupScript.GetHeldObject();

        //Mengaplikasikan jerigen pada kayu
         if (inReach && Input.GetKeyDown(KeyCode.C) && unlit && heldObject != null && heldObject.CompareTag("Jerigen"))
        {
            wet = true;
            Destroy(heldObject); //menghancurkan objek jerigen
            pickupScript.ReleaseObject(); // Menghapus referensi dari skrip Pickup
            pickupScript.jerigenIcon.SetActive(false);  //Mematikan icon jerigen ketika pemain telah mengaplikasikan jerigen
        }

        //Apabila korek api menyala dan telah diberi minyak
        if (korekApi != null && korekApi.activeInHierarchy && inReach && unlit && Input.GetKeyDown(KeyCode.C) && wet)      //mendeteksi korek telah menyala, pemain menekan tombol E
        {
            api.SetActive(true);
            lightSFX.Play();
            lightFire.SetActive(true);
            lightText.SetActive(false);
            unlit = false;

            if (korekApiOBJ != null)
            {
                Destroy(korekApiOBJ);
            }
        }


    }

    // Method untuk mengatur korek api dari skrip Pickup
    public void SetKorekApi(GameObject korekObj, GameObject apiObj)
    {
        korekApiOBJ = korekObj;
        korekApi = apiObj;
    }

}
