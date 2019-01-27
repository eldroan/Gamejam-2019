using System.Linq;
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
    public string playerIDTest;
    public string PlayerID { get { return playerID; } set { this.playerID = value; SetTag(); } }

	// Audio objects
	private AudioSource audioSource;
	private AudioClip clipJumpPlayerA;
	private AudioClip clipAttackPlayerA1;
	private AudioClip clipAttackPlayerA2;
	private AudioClip clipBlockPlayerA;

	private AudioClip clipJumpPlayerB;
	private AudioClip clipAttackPlayerB1;
	private AudioClip clipAttackPlayerB2;
	private AudioClip clipBlockPlayerB;

	private void Awake()
    {
		audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();

		if (ifTest) PlayerID = playerIDTest;

        myRigidbody2D = this.GetComponent<Rigidbody2D>();
        myRigidbody2D = this.GetComponent<Rigidbody2D>();

        myHitObject = GetComponentsInChildren<BoxCollider2D>().Select(x => x).Where(x => x.name == "HitObjectCollider").FirstOrDefault();

		// The audio thing
		var playerA = CharacterBundle.Instance.GetCharacter(Session.Instance.PlayerA);
		var playerB = CharacterBundle.Instance.GetCharacter(Session.Instance.PlayerB);

		clipJumpPlayerA = playerA.jump;
		clipJumpPlayerA = playerA.attack1;
		clipJumpPlayerA = playerA.attack2;
		clipJumpPlayerA = playerA.block;

		clipJumpPlayerB = playerB.jump;
		clipJumpPlayerB = playerB.attack1;
		clipJumpPlayerB = playerB.attack2;
		clipJumpPlayerB = playerB.block;
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

        if (PlayerInputs.GetAxisOrKey(playerID, Constants.LEFTH) || PlayerInputs.GetAxisOrKey(playerID, Constants.RIGHT))
        {
            this.Animator.SetBool("Walk", false);

        }

        if (isGrounded && PlayerInputs.GetKeyDown(playerID, Constants.JUMP) && !bloking && !attacking)
        {
			if (playerID == Constants.PLAYER_1_TAG)
			{
				audioSource.PlayOneShot(clipJumpPlayerA);
			}
			else
			{
				audioSource.PlayOneShot(clipJumpPlayerB);
			}

			this.Animator.SetTrigger(Constants.JUMP);
            myRigidbody2D.velocity = new Vector3(0f, jumpForce, 0f);
        }

        if (PlayerInputs.GetAxisOrKey(playerID, Constants.LEFTH) && !bloking && !attacking)
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
        else if (PlayerInputs.GetAxisOrKey(playerID, Constants.RIGHT) && !bloking && !attacking)
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

        if (PlayerInputs.GetKeyDown(playerID, Constants.ATTACK) && remainingAttackDelay <= 0)
        {
			int randomNumber = Random.Range(0, 2);
			if (playerID == Constants.PLAYER_1_TAG)
			{
				if (randomNumber >= 1f)
				{
					audioSource.PlayOneShot(clipAttackPlayerA1);
				}
				else
				{
					audioSource.PlayOneShot(clipAttackPlayerA2);
				}
			}
			else
			{
				if (randomNumber >= 1f)
				{
					audioSource.PlayOneShot(clipAttackPlayerB1);
				}
				else
				{
					audioSource.PlayOneShot(clipAttackPlayerB2);
				}
			}
			this.Animator.SetTrigger("Attack");
            this.remainingAttackDelay = this.attackDelay;

            this.attacking = true;

        }

        if (PlayerInputs.GetKeyDown(playerID, Constants.BLOCK) && remainingBlockDelay <= 0)
        {
			if (playerID == Constants.PLAYER_1_TAG)
			{
				audioSource.PlayOneShot(clipBlockPlayerA);
			}
			else
			{
				audioSource.PlayOneShot(clipBlockPlayerB);
			}

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
        if (collision.tag != this.gameObject.tag && (collision.tag == Constants.PLAYER_1_TAG || collision.tag == Constants.PLAYER_2_TAG))
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
                    if (collision.gameObject.name == "HitObjectCollider")
                        collision.enabled = false;
                    else if (this.gameObject.name == "HitObjectCollider")
                        collision.enabled = false;
                }
            }
            var chancleta = collision.gameObject.GetComponent<BluntObject>();

            if (chancleta != null)
            {
                Debug.Log("Le pego una chancleta");
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.Log("Le pego otra cosa");
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

    public void Attack()
    {
        if (myHitObject != null)
            myHitObject.enabled = true;
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
