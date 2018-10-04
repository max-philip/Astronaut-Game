using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine.UI;


[RequireComponent(typeof(GravityBody))]
public class PlayerControl : MonoBehaviour
{

    private CharacterController myController;
    private Animator anim;
    private Vector3 moveDirection = Vector3.zero;
    public float turnSpeed = 10.0f;


    private Stopwatch watch = new Stopwatch();
    private Text timerText;
    public bool cubesCollected = false;



    // public vars
    public float mouseSensitivityX = 1;
    public float mouseSensitivityY = 1;
    public float speed = 6;
    public float shiftMulti = 1.5f;
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


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraTransform = Camera.main.transform;
        rigidbody = GetComponent<Rigidbody>();



        anim = GetComponent<Animator>();
        //myController = GetComponent<CharacterController>();


        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        watch.Start();
    }


    void Update()
    {
        if (!cubesCollected)
        {
            timerText.text = "Time Elapsed: " + (watch.Elapsed).ToString();
        } else
        {
            watch.Stop();
        }

        // Look rotation:
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -25, -25);
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

        // Calculate movement:
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        
        if (inputY != 0f || inputX != 0f)
        {
            anim.SetInteger("AnimationPar", 1);
        } else
        {
            anim.SetInteger("AnimationPar", 0);
        }

        //transform.Rotate(0, inputX * turnSpeed * Time.deltaTime, 0);

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
            
            if (global.fuel > 0)
            {

                rigidbody.AddForce(transform.up * levForce);
                global.fuel -= 1;
                
            }
        }
        else
        {
            if (global.fuel < 100)
            {
                global.fuel += 0.8f;
            }
        }

        if (rigidbody.velocity.y > 0.05f || rigidbody.velocity.y < -0.05f)
        {
            anim.SetInteger("AnimationPar", 2);
            if (Input.GetMouseButtonDown(1))
            {
                anim.SetInteger("AnimationPar", 3);
            }
        }

        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(transform.up * jumpForce);
        }
        */

        if (Input.GetMouseButtonDown(0))
        {
            GameObject shot = Instantiate(projectileTemplate, this.transform.position + this.transform.forward*3 + this.transform.up, Quaternion.identity);
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