
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{

    private Rigidbody rb;
    public float speed = 5f;
    public float sprint = 2f;
    public float rotationSpeed = 5f;
    public float jumpForce = 2.0f;
    public Vector3 jump;
    private bool isGrounded;


    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        jump = new Vector3(0.0f, 2.0f, 0.0f);


////check if you are grounded with console
        if (isGrounded == false)
        {
            Debug.Log("you're not touching the grounde");
        }
        if (isGrounded == true)
        {
            Debug.Log("you're touching the ground!");
        }
    }

        void OnCollisionStay()
    {
        isGrounded = true;
    }
    void OnCollisionExit()
    {
        isGrounded = false;
    }
    void FixedUpdate()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xMove + transform.forward * zMove;
    
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * sprint : speed;


        rb.MovePosition(rb.position + move * currentSpeed * Time.deltaTime);


                // jump code
 

 //check with console is spacebar was pressed
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("spacebar pressed!");
        }
//code for jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("you jumped");
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        }

        if (!isGrounded)
        {
            rb.AddForce(Vector3.down*10f);
        }

    }


}