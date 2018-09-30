﻿using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(GravityBody))]
public class PlayerControl : MonoBehaviour
{

    // public vars
    public float mouseSensitivityX = 1;
    public float mouseSensitivityY = 1;
    public float speed = 6;
    public float shiftMulti = 2;
    public float levForce = 5;
    public LayerMask groundedMask;

    // System vars
    bool grounded;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    float verticalLookRotation;
    Transform cameraTransform;
    Rigidbody rigidbody;


    public GameObject projectileTemplate;


    public float levitateMana = 50;
    public static event Action<float> LevitateManaChange;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraTransform = Camera.main.transform;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        // Look rotation:
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -25, -25);
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

        // Calculate movement:
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");


        // sprint movement
        float moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = speed * shiftMulti;
        } else
        {
            moveSpeed = speed; 
        }

        Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
        Vector3 targetMoveAmount = moveDir * moveSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

        if (Input.GetKey(KeyCode.Space))
        {
            if (levitateMana > 0)
            {
                rigidbody.AddForce(transform.up * levForce);
                levitateMana -= 1;
            }
        }
        else
        {
            if (levitateMana < 50)
            {
                levitateMana += 0.5f;
            }
        }

        LevitateManaChange(levitateMana);

        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(transform.up * jumpForce);
        }
        */

        if (Input.GetMouseButtonDown(0))
        {
            GameObject shot = Instantiate(projectileTemplate, this.transform.position + this.transform.forward*3, Quaternion.identity);
            Rigidbody rb = shot.GetComponent<Rigidbody>();
            rb.velocity = (transform.forward * 23.5f); // + new Vector3(0, verticalLookRotation, 0);
        }


    }

    void FixedUpdate()
    {
        // Apply movement to rigidbody
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + localMove);
    }
}