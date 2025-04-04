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


    public float speed = 0;
    public TextMeshProUGUI countText; 
    public GameObject winTextObject;



    void Start() {
        rb = GetComponent <Rigidbody>(); 
        count = 0; 
        pickups = GameObject.FindGameObjectsWithTag("PickUp");
        totalCount = pickups.Length;
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
        if (count >= totalCount && Input.GetKeyDown(KeyCode.R)) {
            restart();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            count = count + 1;
            SetCountText();
            other.gameObject.SetActive(false);
            if (count >= totalCount) {
                winTextObject.SetActive(true);
            }
        }
    }

    void restart(){
        count = 0; 
        SetCountText();
        winTextObject.SetActive(false);
        transform.position = new Vector3(0.0f, 0.5f, 0.0f);
        foreach (GameObject pickup in pickups) {
            pickup.SetActive(true);
        }
    }


}
