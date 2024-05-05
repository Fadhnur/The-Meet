using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManequeenGhost : MonoBehaviour
{
    public Camera playerCamera; // Referensi ke kamera pemain
    public float movementSpeed = 3f; // Kecepatan gerak hantu

    private bool isMoving = false; // Apakah hantu sedang bergerak atau tidak

    void Update()
    {
        if (IsPlayerLookingAtGhost()) // Cek apakah pemain sedang melihat hantu
        {
            isMoving = false; // Jika pemain melihat, hantu berhenti bergerak
        }
        else
        {
            isMoving = true; // Jika pemain tidak melihat, hantu bergerak
        }

        if (isMoving)
        {
            // Hantu bergerak menuju pemain
            transform.position = Vector3.MoveTowards(transform.position, playerCamera.transform.position, movementSpeed * Time.deltaTime);
        }
    }

    bool IsPlayerLookingAtGhost()
    {
        // Tentukan arah pandang dari kamera pemain ke posisi hantu
        Vector3 directionToGhost = transform.position - playerCamera.transform.position;

        // Tentukan sudut antara arah pandang kamera dan arah hantu
        float angle = Vector3.Angle(playerCamera.transform.forward, directionToGhost);

        // Jika sudut antara arah pandang dan arah hantu kecil, pemain sedang melihat hantu
        if (angle < playerCamera.fieldOfView / 2f)
        {
            return true;
        }
        return false;
    }
}
