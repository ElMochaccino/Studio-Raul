using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController2D controller;

    public float speed = 10.0f;
    public Animator animator;
    public GameManager gameManager;
    public float walkSpeed = 10f;
    public float runSpeed = 50f;
    private bool isSprinting = false; // Para controlar si el personaje est� corriendo

    public float fallingSpeed = 0.0f;

    private int maxJumps = 1; // N�mero m�ximo de saltos
    private int jumpCount = 0; // Contador de saltos
    private bool isGrounded; // Para saber si el jugador est� en el suelo

    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (gameManager.isGameActive == true)
        {
            float inputH = Input.GetAxis("Horizontal");


            bool jump = false;

            // Si el jugador toca el suelo, reinicia el contador de saltos
            isGrounded = controller.IsGrounded();
            if (isGrounded)
            {
                jumpCount = 0;
                fallingSpeed = 0;
            }

            // Salto: verifica si el jugador puede saltar (en el suelo o en el aire si no ha alcanzado el l�mite)
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
            {
                jump = true;
                jumpCount++; // Aumenta el contador de saltos
            }

            // Velocidad de carrera
            float currentSpeed = isSprinting ? runSpeed : walkSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isSprinting = true;
            }
            else
            {
                isSprinting = false;
            }

            // Animaci�n del movimiento
            if (Mathf.Abs(inputH) > 0)
            {
                animator.SetFloat("Speed", currentSpeed);
            }
            else
            {
                animator.SetFloat("Speed", 0);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(rb.velocity.y); //Cosas de Debug
            }

            if (rb.velocity.y <= -0.2 && rb.velocity.y >= -20)
            {
                rb.AddForce(new Vector2(0, -2));
            }

            // Movimiento del personaje
            controller.Move(inputH * (currentSpeed / 10), false, jump);

            animator.SetBool("Move_Bool", Input.GetAxis("Horizontal") != 0);
        }

        
    }
}
