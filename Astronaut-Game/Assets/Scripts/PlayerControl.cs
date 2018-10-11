using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System;
//using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GravityBody))]
public class PlayerControl : MonoBehaviour
{

    private CharacterController myController;
    private Animator anim;
    private Vector3 moveDirection = Vector3.zero;
    public float turnSpeed = 10.0f;


    //private Stopwatch watch = new Stopwatch();
    private Text timerText;
    public bool cubesCollected = false;

    private float health = StatVariables.maxHealth;
    private float fuel = StatVariables.maxFuel;
    private Text HealthText;
    private Text JetText;
    private Text MoneyText;

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


    void Start()
    {
        cameraTransform = Camera.main.transform;
        rigidbody = GetComponent<Rigidbody>();



        anim = GetComponent<Animator>();
        //myController = GetComponent<CharacterController>();


        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        //watch.Start();

        HealthText = GameObject.Find("HealthText").GetComponent<Text>();
        JetText = GameObject.Find("JetText").GetComponent<Text>();
        MoneyText = GameObject.Find("MoneyText").GetComponent<Text>();
    }


    void Update()
    {

        // No control over player if the game is paused
        if (PauseMenu.gamePaused)
        {
            return;
        }

        if (!cubesCollected)
        {
            //timerText.text = "Time Elapsed: " + (watch.Elapsed).ToString();
        } else
        {
            //watch.Stop();
        }

        // Look rotation:
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -25, -25);
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

        float distFromOrigin = Vector3.Distance(new Vector3(0, 0, 0), transform.position);
        if (SceneManager.GetActiveScene().name != "Tutorial")
        {
            cameraTransform.localEulerAngles =
            new Vector3(distFromOrigin / 2, cameraTransform.localEulerAngles.y, cameraTransform.localEulerAngles.z);
        } else
        {
            cameraTransform.localEulerAngles =
            new Vector3(distFromOrigin, cameraTransform.localEulerAngles.y, cameraTransform.localEulerAngles.z);
        }

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
            
            if (fuel > 0)
            {
                if (SceneManager.GetActiveScene().name != "Tutorial")
                {
                    rigidbody.AddForce(transform.up * levForce);
                } else
                {
                    rigidbody.AddForce(transform.up * levForce * 1.2f);
                }

                fuel -= 2;
                
            }
        }
        else
        {
            if (fuel < StatVariables.maxFuel)
            {
                fuel += 3;
            }

            if (fuel > StatVariables.maxFuel)
            {
                fuel = StatVariables.maxFuel;
            }
        }

        if (health <= 0)
        {
            SceneManager.LoadScene("Scenes/MainMenu");
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

        writeHUD();
    }

    public void reduceHealth(int damage)
    {
        health -= damage;
    }

    private void writeHUD()
    {
        HealthText.text = "Health:   " + health.ToString() + " / " + StatVariables.maxHealth.ToString();
        JetText.text = "Fuel:   " + fuel.ToString() + " / " + StatVariables.maxFuel.ToString();
        MoneyText.text = "Money:   " + StatVariables.money.ToString();
    }

    void FixedUpdate()
    {
        // Apply movement to rigidbody
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + localMove);
    }
}