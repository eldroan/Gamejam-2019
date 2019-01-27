using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float attackDelay = 5f;
    [SerializeField] private float blockDelay = 5f;

    [SerializeField] private float speed = 30f;    
    [SerializeField] private float jumpForce = 17f;
    [SerializeField] private Animator Animator;
    [Tooltip("(Opcional) - Objeto contundente para arrojar.")]
    [SerializeField] private GameObject bluntObject;
    [Tooltip("(Opcional) - Si hay objeto contundente hay que setear de donde sale.")]
    [SerializeField] private Transform bluntObjectSpawnPosition;

    private Rigidbody2D myRigidbody2D;
    private bool isGrounded;

    private float remainingAttackDelay;
    private float remainingBlockDelay;

    private bool bloking;

    public string PlayerID { get; set; }

    private void Awake()
    {
        myRigidbody2D = this.GetComponent<Rigidbody2D>();
        myRigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (remainingAttackDelay > 0)
            remainingBlockDelay -= Time.deltaTime;
        if (remainingBlockDelay > 0)
                remainingBlockDelay -= Time.deltaTime;

        if (isGrounded && (Input.GetKeyDown(PlayerInputs.GetKey(PlayerID, Constants.JUMP))))
        {
            this.Animator.SetTrigger(Constants.JUMP);
            myRigidbody2D.velocity = new Vector3(0f, jumpForce, 0f);
        }
        if (Input.GetKey(PlayerInputs.GetKey(PlayerID, Constants.LEFTH)))
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
        else if (Input.GetKey(PlayerInputs.GetKey(PlayerID, Constants.RIGHT)))
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

        if (Input.GetKeyUp(PlayerInputs.GetKey(PlayerID, Constants.RIGHT)))
        {
            this.Animator.SetTrigger("StopWalk");
        }
        if (Input.GetKeyUp(PlayerInputs.GetKey(PlayerID, Constants.LEFTH)))
        {
            this.Animator.SetTrigger("StopWalk");
        }

        if (Input.GetKeyDown(PlayerInputs.GetKey(PlayerID, Constants.ATTACK)) && remainingAttackDelay <= 0)
        {
            this.Animator.SetTrigger("Attack");
            this.remainingAttackDelay = this.attackDelay;

            if (bluntObject != null)
            {
                var go = Instantiate(bluntObject, bluntObjectSpawnPosition.position + new Vector3(0f,0f,0.01f), this.transform.rotation);
                go.tag = this.gameObject.tag;
                go.GetComponent<BluntObject>()?.Shoot();
            }
        }

        if (Input.GetKeyDown(PlayerInputs.GetKey(PlayerID, Constants.BLOCK)) && remainingBlockDelay <= 0)
        {
            this.Animator.SetTrigger("Block");
            this.bloking = true;
            this.remainingBlockDelay = this.blockDelay;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != this.gameObject.tag)
        {
            if (bloking)
            {
                //TODO ruido bloquear
            }
            else
            {
                FightManager.Instance.OnPlayerHit(PlayerID == Constants.PLAYER_1_TAG ? Constants.PLAYER_1_TAG : Constants.PLAYER_2_TAG);
            }
        }
    }
}
