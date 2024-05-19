using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    
    private float horizontal; 
    private float vertical;

    [SerializeField]
    private float speed = 10.0f;

    private Vector3 moveDirection;
    public bool isGrounded;
    public Rigidbody rb;

    public AudioSource footStepSound;
    public float footStepInterval = 0.1f;
    private float footStepTimer;

    void Start()
    {
       rb = GetComponent<Rigidbody>();
       isGrounded = false;

       //inisialisasi audio
       footStepSound = GetComponent<AudioSource>();

       
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontal, 0, vertical);

        //Cek apakah karakter berada di tanah dengan Raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        //transform.Translate(moveDirection * speed * Time.deltaTime); 

        //Berjalan
        if(moveDirection.magnitude > 0 && isGrounded)
        {
            PlayFootStepSound();
        }

        //Melompat
        /*if(Input.GetKeyDown(KeyCode.Space)&& isGrounded){
            rb.velocity = new Vector3(0,5,0);
            //isGrounded = true;
        }*/

        // Melompat
        /*if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }*/

        //Berlari
        if(Input.GetKey(KeyCode.LeftShift)&& isGrounded){
            transform.position += transform.forward * Time.deltaTime * speed;
        }

        // Berlari
        // if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        // {
        //     moveDirection *= 1.5f;
        // }
        

    }

    void FixedUpdate()
    {
        if (isGrounded)
        {
            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
        }
    }

    void PlayFootStepSound()
    {
        footStepTimer -= Time.deltaTime;
        if (!footStepSound.isPlaying && footStepTimer <= 0)
        {
            footStepSound.Play();
            footStepTimer = footStepInterval;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (other.collider.CompareTag("Tembok"))
        {
            rb.velocity = Vector3.zero;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }



}
