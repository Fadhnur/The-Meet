using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayuBakar : MonoBehaviour
{
    public GameObject korekApi;
    public GameObject api;
    public GameObject lightText;

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
    }

    //Mendeteksi pohon 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach" && unlit)
        {
            Debug.Log("inReach");
            inReach = true;
            lightText.SetActive(true);
            wet = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach" && unlit)
        {
            //Debug.Log("inReach");
            inReach = false;
            lightText.SetActive(false);
            wet = false;
        }
    }

    //Membakar objek 
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Burn")
        {
            Destroy(gameObject);
            GameObject explision = Instantiate(api, transform.position, transform.rotation);
            Destroy(explision, 0.75f);
        }

        if(other.gameObject.tag == "Jerigen"){
            Destroy(other.gameObject);
            wet = true;
        }
    }


    void Update()
    {
        if (korekApi.activeInHierarchy && inReach && unlit && Input.GetKeyDown(KeyCode.E) && wet)      //mendeteksi korek telah menyala, pemain menekan tombol E
        {
            api.SetActive(true);
            lightText.SetActive(false);
            unlit = false;

        }

    }


}
