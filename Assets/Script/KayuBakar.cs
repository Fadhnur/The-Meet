using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayuBakar : MonoBehaviour
{
    public GameObject korekApi;
    public GameObject api;
    public GameObject lightText;
    public GameObject lightFire;
    public AudioSource lightSFX;

    public bool unlit;
    public bool inReach;
    public bool wet;

    //[SerializeField] GameObject obj;

    void Start()
    {
        unlit = true;
        api.SetActive(false);
        lightText.SetActive(false);
        wet = false;
        lightFire.SetActive(false);
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
            //tidak terdeteksi (error)
            GameObject explosion = Instantiate(api, burnPosition, burnRotation);
            Destroy(explosion, 0.75f);
            Debug.Log("burn");
        }

        //Mengaplikasikan jerigen pada kayu
        if(other.gameObject.tag == "Jerigen"){
            Destroy(other.gameObject);
            wet = true;
        }
    }


    void Update()
    {
        //Apabila korek api menyala dan telah diberi minyak
        if (korekApi.activeInHierarchy && inReach && unlit && Input.GetKeyDown(KeyCode.E) && wet)      //mendeteksi korek telah menyala, pemain menekan tombol E
        {
            api.SetActive(true);
            lightSFX.Play();
            lightFire.SetActive(true);
            lightText.SetActive(false);
            unlit = false;
            Destroy(korekApi);
        }

    }


}
