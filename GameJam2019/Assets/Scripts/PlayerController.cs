﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float attackDelay = 5f;
    [SerializeField] private float blockDelay = 5f;
    [SerializeField] private bool ifTest;

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
    private bool attacking;
    private Collider2D myHitObject;

    private string playerID { get; set; }
    public string PlayerID { get { return playerID; } set { this.playerID = value; SetTag(); } }

    private void Awake()
    {
        if (ifTest) PlayerID = Constants.PLAYER_1_TAG;

        myRigidbody2D = this.GetComponent<Rigidbody2D>();
        myRigidbody2D = this.GetComponent<Rigidbody2D>();

        myHitObject = GetComponentsInChildren<BoxCollider2D>().Select(x => x).Where(x => x.name == "HitObjectCollider").FirstOrDefault();
    }

    private void SetTag()
    {
        if (myHitObject != null)
        {
            myHitObject.tag = playerID;
            myHitObject.enabled = false;
        }
    }

    private void Update()
    {
        if (remainingAttackDelay > 0)
            remainingAttackDelay -= Time.deltaTime;
        if (remainingBlockDelay > 0)
            remainingBlockDelay -= Time.deltaTime;
    }
    private void FixedUpdate()
    {

        if (Input.GetKeyUp(PlayerInputs.GetKey(playerID, Constants.LEFTH)) || Input.GetKeyUp(PlayerInputs.GetKey(playerID, Constants.RIGHT)))
        {
            this.Animator.SetBool("Walk", false);

        }

        if (isGrounded && (Input.GetKeyDown(PlayerInputs.GetKey(playerID, Constants.JUMP))) && !bloking && !attacking)
        {
            this.Animator.SetTrigger(Constants.JUMP);
            myRigidbody2D.velocity = new Vector3(0f, jumpForce, 0f);
        }

        if (Input.GetKey(PlayerInputs.GetKey(playerID, Constants.LEFTH)) && !bloking && !attacking)
        {
            if (isGrounded)
            {
                myRigidbody2D.velocity = new Vector3(-speed, myRigidbody2D.velocity.y, 0f);
                this.Animator.SetBool("Walk", true);
            }
            else
            {
                myRigidbody2D.velocity = new Vector3(-speed * 0.4f, myRigidbody2D.velocity.y, 0f);
            }
            
            this.transform.rotation = new Quaternion(0f, 180f, 0f, 1f);
        }
        else if (Input.GetKey(PlayerInputs.GetKey(playerID, Constants.RIGHT)) && !bloking && !attacking)
        {
            if (isGrounded)
            {
                this.Animator.SetBool("Walk", true);
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
            this.Animator.SetBool("Walk", false);

        }



        if (Input.GetKeyDown(PlayerInputs.GetKey(playerID, Constants.ATTACK)) && remainingAttackDelay <= 0)
        {
            this.Animator.SetTrigger("Attack");
            this.remainingAttackDelay = this.attackDelay;

            this.attacking = true;

            if (myHitObject != null)
                myHitObject.enabled = true;

        }

        if (Input.GetKeyDown(PlayerInputs.GetKey(playerID, Constants.BLOCK)) && remainingBlockDelay <= 0)
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
                if (!attacking)
                {
                    FightManager.Instance.OnPlayerHit(playerID == Constants.PLAYER_1_TAG ? Constants.PLAYER_1_TAG : Constants.PLAYER_2_TAG);
                }
            }
        }
    }

    public void InAttackMoment()
    {
        if (bluntObject != null)
        {
            var go = Instantiate(bluntObject, bluntObjectSpawnPosition.position + new Vector3(0f, 0f, 0.01f), this.transform.rotation);
            go.tag = this.gameObject.tag;
            go.GetComponent<BluntObject>()?.Shoot();
        }
    }

    public void InBlockBlock()
    {
    }

    public void EndBlock()
    {
        this.bloking = false;
    }

    public void EndAttack()
    {
        this.attacking = false;
        if (myHitObject != null)
            myHitObject.enabled = false;
    }
}
