using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class FlashLight : MonoBehaviour
{
    public GameObject lightSource;

    public Image percent;

    public TMP_Text batteryText;

    public float lifetime,batteries, MaxLifetime = 100;

    public AudioSource flashON;
    public AudioSource flashOFF;

    public bool on;
    public bool off;


    void Start()
    {
        /*light = GetComponent<Light>();

        off = true;
        light.enabled = false;
        */

        /*if (on == false)
        {
            lightSource.SetActive(false);
        }*/

        off = true;
        lightSource.SetActive(false);

        lifetime = MaxLifetime;

    }



    void Update()
    {
        //Indikator jumlah baterai
        batteryText.text = batteries.ToString();

        //Menyalakan senter menekan tombol F dalam keadaan mati
        if (Input.GetKeyDown(KeyCode.F) && off)
        {
            Debug.Log("F Pressed");
            flashON.Play();
            lightSource.SetActive(true);
            on = true;
            off = false;
        }

        //Menyalakan senter menekan tombol F dalam keadaan hidup
        else if (Input.GetKeyDown(KeyCode.F) && on)
        {
            Debug.Log("F Pressed");
            flashOFF.Play();
            lightSource.SetActive(false);
            on = false;
            off = true;
        }

        if (on)
        {
            lifetime -= 1 * Time.deltaTime;
        }

        //jika persentase baterai dibawah 0 maka dianggap 0
        if (lifetime <= 0)
        {
            lightSource.SetActive(false);
            on = false;
            off = true;
            lifetime = 0;
        }

        //jika persentase baterai diatas 100 maka dianggap 100
        if (lifetime >= 100)
        {
            lifetime = 100;
        }

        //Recharge baterai
        if (Input.GetKeyDown(KeyCode.R) && batteries >= 1)    //&& batteries >= 1
        {
            Debug.Log("R Pressed");
            batteries -= 1;
            lifetime += 100;
        }

        if (Input.GetKeyDown(KeyCode.R) && batteries == 0)    //&& batteries == 0
        {
            Debug.Log("R Pressed");
            return;
            //notif ui baterai tidak ada
        }

        //Jika banyak baterai dibawah 0 maka dianggap 0
        if (batteries <= 0)
        {
            batteries = 0;
        }
        

        FlashlightFiller();


    }

    void FlashlightFiller()
    {
        percent.fillAmount = lifetime / MaxLifetime;
    }

}
