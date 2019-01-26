using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 30f;    
    [SerializeField] private float jumpForce = 17f;
    private Rigidbody2D rigidbody2D;
    private bool isGrounded;

    void Awake()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isGrounded && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)))
        {
            rigidbody2D.velocity = new Vector3(0f, jumpForce, 0f);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (isGrounded)
            {
                rigidbody2D.velocity = new Vector3(-speed, rigidbody2D.velocity.y, 0f);
            }
            else
            {
                rigidbody2D.velocity = new Vector3(-speed * .4f, rigidbody2D.velocity.y, 0f);
            }
            
            this.transform.rotation = new Quaternion(-180f, 0f, 0f, 1f);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (isGrounded)
            {
                rigidbody2D.velocity = new Vector3(speed, rigidbody2D.velocity.y, 0f);
            }
            else
            {
                rigidbody2D.velocity = new Vector3(speed * .4f, rigidbody2D.velocity.y, 0f);
            }

            this.transform.rotation = new Quaternion(180f, 0f, 0f, 1f);
        }else
        {
            rigidbody2D.velocity = new Vector3(0f, rigidbody2D.velocity.y, 0f);
        }

        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }    
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }      
    }
}
