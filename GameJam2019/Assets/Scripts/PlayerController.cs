using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 30f;    
    [SerializeField] private float jumpForce = 17f;
    [SerializeField] private Animator Animator;

    private Rigidbody2D myRigidbody2D;
    private bool isGrounded;

    private void Awake()
    {
        myRigidbody2D = this.GetComponent<Rigidbody2D>();
        myRigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isGrounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))
        {
            this.Animator.SetTrigger("Jump");
            myRigidbody2D.velocity = new Vector3(0f, jumpForce, 0f);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (isGrounded)
            {
                this.Animator.SetTrigger("Walk");
                myRigidbody2D.velocity = new Vector3(-speed, myRigidbody2D.velocity.y, 0f);
            }
            else
            {
                myRigidbody2D.velocity = new Vector3(-speed * 0.4f, myRigidbody2D.velocity.y, 0f);
            }
            
            this.transform.rotation = new Quaternion(0f, 180f, 0f, 1f);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (isGrounded)
            {
                this.Animator.SetTrigger("Walk");
                myRigidbody2D.velocity = new Vector3(speed, myRigidbody2D.velocity.y, 0f);
            }
            else
            {
                myRigidbody2D.velocity = new Vector3(speed * 0.4f, myRigidbody2D.velocity.y, 0f);
            }

            this.transform.rotation = new Quaternion(0f, 0f, 0f, 1f);
        }
        else
        {
            myRigidbody2D.velocity = new Vector3(0f, myRigidbody2D.velocity.y, 0f);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            this.Animator.SetTrigger("StopWalk");
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            this.Animator.SetTrigger("StopWalk");
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }    
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }      
    }
}
