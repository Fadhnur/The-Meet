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
    public GameObject jerigenIcon;

    private Rigidbody CurrentObjectRigidbody;
    private Collider CurrentObjectCollider;

    [SerializeField]
    [Min(1)]
    private float hitRange = 3;

    private RaycastHit hit;

    // Menyimpan referensi ke objek yang sedang dipegang
    private GameObject heldObject;
    private KayuBakar kayuBakarScript; // Referensi ke skrip KayuBakar
    void Start()
    {
        pickUpUI.SetActive(false);
        jerigenIcon.SetActive(false);
        heldObject = null;

        // Mendapatkan referensi ke skrip KayuBakar
        kayuBakarScript = FindObjectOfType<KayuBakar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Press E Object");
            // Membuat sebuah ray dari posisi kamera pemain ke arah yang dituju dan memeriksa apakah ada tabrakan (collision) dengan objek dalam jarak pengambilan yang ditentukan.
            Ray Pickupray = new Ray(PlayerCamera.transform.position, PlayerCamera.transform.forward);
            if (Physics.Raycast(Pickupray, out RaycastHit hitinfo, Pickuprange, PickupLayer))
            {
                Debug.Log("hitRaycast");
                if (CurrentObjectRigidbody)
                {
                    ReleaseObject();
                }

                PickupObject(hitinfo);
                return;
            }

            if (CurrentObjectRigidbody)
            {
                ReleaseObject();
            }
        }

        // Melempar objek
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (CurrentObjectRigidbody)
            {
                ThrowObject();
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
            hit.collider.GetComponent<Highlight>()?.ToggleOutline(false);
            pickUpUI.SetActive(false);
        }

        // Menghighlight object yang diambil
        if (Physics.Raycast(PlayerCamera.position, PlayerCamera.forward, out hit, hitRange, PickupLayer))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleOutline(true);
            pickUpUI.SetActive(true);
        }
    }

    private void PickupObject(RaycastHit hitinfo)
    {
        CurrentObjectRigidbody = hitinfo.rigidbody;
        CurrentObjectCollider = hitinfo.collider;
        CurrentObjectRigidbody.isKinematic = true;
        CurrentObjectCollider.enabled = false;
        heldObject = hitinfo.collider.gameObject; // Menyimpan referensi ke objek yang diambil
        Debug.Log("Player is holding: " + heldObject.name);

        // Memeriksa apakah objek yang diambil adalah korek api
        if (hitinfo.collider.CompareTag("Fire"))
        {
            GameObject korekObj = hitinfo.collider.gameObject;
            GameObject apiObj = korekObj.transform.Find("Api Korek").gameObject; // Menemukan child dengan nama "apiKorek"
            if (kayuBakarScript != null)
            {
                kayuBakarScript.SetKorekApi(korekObj, apiObj); // Mengatur referensi di skrip KayuBakar
            }
        }

        // Memeriksa apakah objek yang diambil adalah jerigen
        if(hitinfo.collider.CompareTag("Jerigen")){
            jerigenIcon.SetActive(true);
        }
        
    }

    public void ReleaseObject()
    {
        CurrentObjectRigidbody.isKinematic = false;
        CurrentObjectCollider.enabled = true;
        CurrentObjectRigidbody = null;
        CurrentObjectCollider = null;
        heldObject = null; // Menghapus referensi ke objek yang dipegang

        jerigenIcon.SetActive(false);
    }

    private void ThrowObject()
    {
        CurrentObjectRigidbody.isKinematic = false;
        CurrentObjectCollider.enabled = true;
        CurrentObjectRigidbody.AddForce(PlayerCamera.transform.forward * ThrowingForce, ForceMode.Impulse);
        CurrentObjectRigidbody = null;
        CurrentObjectCollider = null;
        heldObject = null; // Menghapus referensi ke objek yang dipegang

        jerigenIcon.SetActive(false);
    }

    // Metode untuk mendapatkan objek yang sedang dipegang
    public GameObject GetHeldObject()
    {
        return heldObject;
    }
}
