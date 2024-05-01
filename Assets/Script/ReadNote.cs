using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadNote : MonoBehaviour
{
    public GameObject player;
    public GameObject note;
    //public GameObject HUD;  //tampilan pada layar pemain
    public bool inReach;

    public GameObject pickUpText;

    //public GameObject pickUpSound;

    // Start is called before the first frame update
    void Start()
    {
        //Menonaktifkan semua ui dan objek
        pickUpText.SetActive(false);
        note.SetActive(false);
        //HUD.SetActive(true);

        inReach = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Reach")
        {
            inReach = true;
            pickUpText.SetActive(true);
            Debug.Log("Reach");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Reach")
        {
            inReach = false;
            pickUpText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Pemain menekan tombol E dan berada pada jangkauan
        if(Input.GetKeyDown(KeyCode.E) && inReach)
        {
            Debug.Log("Note pressed");

            note.SetActive(true);       //Note muncul pada layar
            //pickUpSound.SetActive(true);      //suara pickUp aktif
            //HUD.SetActive(false);

            player.GetComponent<player>().enabled = false;      //karakter tidak bisa bergerak / script player dimatikan
            //Cursor.visible = true;      //memunculkan kursor
            //Cursor.lockState = CursorLockMode.None;     //kursor dapat digerakkan
        }

        else if(Input.GetKeyDown(KeyCode.Escape) && inReach)
        {
            note.SetActive(false);
            player.GetComponent<player>().enabled = true;       //karakter dapat bergerak kembali
            //HUD.SetActive(true);

        }
    }
}
