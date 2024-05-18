using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareTrigger : MonoBehaviour
{
    public Transform player; // Referensi ke pemain
    public float jumpScareDistance = 2f; // Jarak di mana jumpscare terjadi

    public GameObject jumpScareObject; // Objek atau efek yang memicu jumpscare

    private bool jumpScareActivated = false; // Apakah jumpscare sudah diaktifkan
    public AudioSource jumpscareSFX;

    void Update()
    {
        // Hitung jarak antara hantu dan pemain
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Jika jarak kurang dari jarak jumpscare dan jumpscare belum diaktifkan
        if (distanceToPlayer <= jumpScareDistance && !jumpScareActivated)
        {
            ActivateJumpScare(); // Aktifkan jumpscare
        }
    }

    void ActivateJumpScare()
    {
        // Aktifkan efek jumpscare
        jumpScareObject.SetActive(true);

        // Tambahkan logika lain sesuai kebutuhan, misalnya suara, getaran, atau animasi
        jumpscareSFX.Play();
        jumpScareActivated = true; // Setel jumpscare sebagai sudah diaktifkan
    }
}
