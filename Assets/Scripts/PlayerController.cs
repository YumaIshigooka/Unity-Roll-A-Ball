using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UIElements;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; 
    private float movementX;
    private float movementY;
    private int count;
    private int totalCount;
    private GameObject[] pickups;
    private AudioSource audioSource;


    public float speed = 0;
    public TextMeshProUGUI countText; 
    public GameObject countTextObject;
    public GameObject winTextObject;



    void Start() {
        rb = GetComponent <Rigidbody>(); 
        count = 0; 
        pickups = GameObject.FindGameObjectsWithTag("PickUp");
        totalCount = pickups.Length;
        audioSource = GetComponent<AudioSource>();
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove (InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>(); 
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

   void SetCountText() {
       countText.text =  "Count: " + count.ToString();
    }

    void FixedUpdate() {
        if (count < totalCount) {
            Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
            rb.AddForce(movement * speed);
        }
    }

    void Update() {
        rb.velocity *= Mathf.Pow(0.15f, Time.deltaTime);
        if (count >= totalCount && Input.GetKeyDown(KeyCode.R)) {
            restart();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            count = count + 1;
            SetCountText();
            audioSource.mute = false;
            audioSource.Play();
            other.gameObject.SetActive(false);
            if (count >= totalCount) {
                winTextObject.SetActive(true);
                countTextObject.SetActive(false);
            }
        }
    }

    void restart(){
        count = 0; 
        SetCountText();
        winTextObject.SetActive(false);
        countTextObject.SetActive(true);
        transform.position = new Vector3(0.0f, 0.5f, 0.0f);
        foreach (GameObject pickup in pickups) {
            pickup.SetActive(true);
        }
    }


}
