using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private LayerMask PickupLayer;
    [SerializeField] private float ThrowingForce;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private float Pickuprange;
    [SerializeField] private Transform Hand;
    [SerializeField] private GameObject pickUpUI;
    [SerializeField] private GameObject jerigenIcon;

    private GameObject flashlight;

    private Rigidbody CurrentObjectRigidbody;
    private Collider CurrentObjectCollider;

    [SerializeField]
    [Min(1)]
    private float hitRange = 3;

    private RaycastHit hit;

    void Start()
    {
        pickUpUI.SetActive(false);
        flashlight = GameObject.Find("Flashlight");
        jerigenIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Membuat sebuah ray dari posisi kamera pemain ke arah yang dituju dan memeriksa apakah ada tabrakan (collision) dengan objek dalam jarak pengambilan yang ditentukan.
            Ray Pickupray = new Ray(PlayerCamera.transform.position, PlayerCamera.transform.forward);
            if (Physics.Raycast(Pickupray, out RaycastHit hitinfo, Pickuprange, PickupLayer))
            {
                Debug.Log("hitRaycast");
                if (hitinfo.collider.CompareTag("Battery"))
                {
                    Debug.Log("Battery Pressed");

                    // Mengubah jumlah baterai pada flashlight
                    //flashlight.GetComponent<FlashLight>().batteries += 1;

                    // Menghapus objek baterai yang telah diambil
                    Destroy(hitinfo.collider.gameObject);

                    if (CurrentObjectRigidbody)
                    {

                        CurrentObjectRigidbody.isKinematic = false;
                        CurrentObjectCollider.enabled = true;

                        CurrentObjectRigidbody = hitinfo.rigidbody;
                        CurrentObjectCollider = hitinfo.collider;

                        CurrentObjectRigidbody.isKinematic = true;
                        CurrentObjectCollider.enabled = false;

                    }
                    else
                    {
                        CurrentObjectRigidbody = hitinfo.rigidbody;
                        CurrentObjectCollider = hitinfo.collider;

                        CurrentObjectRigidbody.isKinematic = true;
                        CurrentObjectCollider.enabled = false;

                    }
                }

                
                else
                {
                    Debug.Log("Press E Object");
                    if (CurrentObjectRigidbody)
                    {
                        CurrentObjectRigidbody.isKinematic = false;
                        CurrentObjectCollider.enabled = true;

                        CurrentObjectRigidbody = hitinfo.rigidbody;
                        CurrentObjectCollider = hitinfo.collider;

                        CurrentObjectRigidbody.isKinematic = true;
                        CurrentObjectCollider.enabled = false;
                    }
                    else
                    {
                        CurrentObjectRigidbody = hitinfo.rigidbody;
                        CurrentObjectCollider = hitinfo.collider;

                        CurrentObjectRigidbody.isKinematic = true;
                        CurrentObjectCollider.enabled = false;
                    }

                    return;
                }

            }

            if (CurrentObjectRigidbody)
            {
                CurrentObjectRigidbody.isKinematic = false;
                CurrentObjectCollider.enabled = true;

                CurrentObjectRigidbody = null;
                CurrentObjectCollider = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (CurrentObjectRigidbody)
            {
                CurrentObjectRigidbody.isKinematic = false;
                CurrentObjectCollider.enabled = true;

                CurrentObjectRigidbody.AddForce(PlayerCamera.transform.forward * ThrowingForce, ForceMode.Impulse);
                CurrentObjectRigidbody = null;
                CurrentObjectCollider = null;

            }
        }

        if (CurrentObjectRigidbody)
        {
            CurrentObjectRigidbody.position = Hand.position;
            CurrentObjectRigidbody.rotation = Hand.rotation;

        }

        Debug.DrawRay(PlayerCamera.position, PlayerCamera.forward * hitRange, Color.red);
        if (hit.collider != null)
        {
            //hit.collider.GetComponent<Highlight>()?.ToggleOutline(false);
            pickUpUI.SetActive(false);
        }

        // if (inHandItem != null)
        // {
        //     return;
        // }

        //menghighlight object yang diambil
        if (Physics.Raycast(PlayerCamera.position, PlayerCamera.forward, out hit, hitRange, PickupLayer))
        {
            //hit.collider.GetComponent<Highlight>()?.ToggleOutline(true);
            pickUpUI.SetActive(true);
            // Debug.Log("hitRaycast");

        }
    }
}