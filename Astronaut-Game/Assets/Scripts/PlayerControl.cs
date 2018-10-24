using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{

    private CharacterController myController;
    private Animator anim;
    private Vector3 moveDirection = Vector3.zero;

    private Text timerText;

    private float health = StatVariables.maxHealth;
    private float fuel = StatVariables.maxFuel;
    private Text HealthText;
    private Text JetText;
    private Text MoneyText;


    public float mouseSensitivityX = 1;
    public float mouseSensitivityY = 1;
    public float speed = 6;
    public float shiftMulti = 1.5f;
    public float levForce = 5;
    public LayerMask groundedMask;

    bool grounded;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    float verticalLookRotation;
    Transform cameraTransform;
    Rigidbody rigidbody;

    // projectile objects
    public GameObject rockProjectile;
    public GameObject grenadeProjectile;
    public GameObject pulseProjectile;

    // boolean flags for
    private bool painPlayed = false;
    private bool isJetting = false;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        rigidbody = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();

        timerText = GameObject.Find("TimerText").GetComponent<Text>();

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

        if (Input.GetMouseButtonDown(0))
        {
            GameObject shot;

            if (StatVariables.weapon == 1)
            {
                shot = Instantiate(rockProjectile, this.transform.position + this.transform.forward * 3 + this.transform.up, Quaternion.identity);
            } else if (StatVariables.weapon == 2)
            {
                shot = Instantiate(grenadeProjectile, this.transform.position + this.transform.forward * 3 + this.transform.up, Quaternion.identity);
            } else
            {
                shot = Instantiate(pulseProjectile, this.transform.position + this.transform.forward * 3 + this.transform.up, Quaternion.identity);
            }
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
        HealthText.text = "HEALTH:   " + health.ToString() + " / " + StatVariables.maxHealth.ToString();
        JetText.text = "FUEL:   " + ((int) fuel).ToString() + " / " + StatVariables.maxFuel.ToString();
        MoneyText.text = "MONEY:  $" + StatVariables.money.ToString();
    }

    void FixedUpdate()
    {
        // Apply movement to rigidbody
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + localMove);

        isJetting = false;
        if (Input.GetKey(KeyCode.Space))
        {

            if (fuel > 0)
            {
                if (SceneManager.GetActiveScene().name != "Tutorial")
                {
                    rigidbody.velocity += transform.up * 0.4f;
                }
                else
                {
                    rigidbody.velocity += transform.up * 0.3f;
                }

                isJetting = true;

                fuel -= 1;

                if (fuel < 0)
                {
                    fuel = 0;
                }

            }
        }
        else
        {
            isJetting = false;

            if (fuel < StatVariables.maxFuel)
            {
                fuel += 0.5f;
            }

            if (fuel > StatVariables.maxFuel)
            {
                fuel = StatVariables.maxFuel;
            }
        }


        // Calculate movement:
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");


        if (inputY != 0f || inputX != 0f)
        {
            anim.SetInteger("AnimationPar", 1);
        }
        else
        {
            anim.SetInteger("AnimationPar", 0);
        }

        // sprint movement
        float moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = speed * shiftMulti;
        }
        else
        {
            moveSpeed = speed;
        }

        Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
        Vector3 targetMoveAmount = moveDir * moveSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);


        if ((health / StatVariables.maxHealth) < 0.5f && !painPlayed)
        {
            GameObject.Find("SoundController").GetComponent<PlanetSounds>().playPain();
            painPlayed = true;
        }

        if (health <= 0)
        {
            SceneManager.LoadScene("Scenes/DeathScene");
        }

        if (rigidbody.velocity.y > 0.05f || rigidbody.velocity.y < -0.05f)
        {
            anim.SetInteger("AnimationPar", 2);
            if (Input.GetMouseButtonDown(1))
            {
                anim.SetInteger("AnimationPar", 3);
            }
        }
    }


    public bool getIsJetting()
    {
        return isJetting;
    }
}