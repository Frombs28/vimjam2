using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12.0f;
    public float sprintSpeed = 24.0f;
    //public float jumpHeight = 3.0f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundCheckDistance = 0.4f;
    public LayerMask groundMask;
    public bool canMove = true;
    public float maxStamina = 3.0f;
    public float staminRechargeRate = 1.0f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool grounded;
    private float trueSpeed;
    [SerializeField]
    private float stamina;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        trueSpeed = speed;
        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
        if (grounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }

        if (canMove)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            if (Input.GetKey(KeyCode.LeftShift) && grounded && stamina > 0.0f)
            {
                trueSpeed = sprintSpeed;
            }
            else
            {
                trueSpeed = speed;
            }
            controller.Move(move * trueSpeed * Time.deltaTime);
            if(move.magnitude > 0.0f && trueSpeed == sprintSpeed && stamina > 0)
            {
                stamina -= Time.deltaTime;
            }
            if(move.magnitude == 0 && stamina < maxStamina)
            {
                stamina += staminRechargeRate * Time.deltaTime;
                if(stamina > maxStamina)
                {
                    stamina = maxStamina;
                }
            }

            //if (Input.GetButtonDown("Jump") && grounded)
            //{
            //    velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            //}
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public int GetMoveSpeed()
    {
        if()
    }
}
