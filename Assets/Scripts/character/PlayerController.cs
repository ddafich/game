using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 500f;
    public float maxSpeed = 1f;
    public float idleFriction = 0.9f;

    private bool isMoving = false;

    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;
    public bool canAttack = true;

    AudioManager audioManager;

    public ParticleSystem dust;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
      
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void FixedUpdate()
    {
        if (canMove == true && movementInput != Vector2.zero)
        {
            rb.AddForce(movementInput* moveSpeed * Time.deltaTime);
            dust.Play();
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                float limitedSpeed = Mathf.Lerp(rb.linearVelocity.magnitude, maxSpeed, idleFriction);
                rb.linearVelocity = rb.linearVelocity.normalized * limitedSpeed;
            }
            if(movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            IsMoving = true;
        }
        else
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, idleFriction);
            IsMoving = false;
        }
    }
    
    public bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
    void OnFire()
    {
        if (canAttack)
        {
            animator.SetTrigger("attack");
            animator.SetInteger("attackIndex", 0);
        }    
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!canAttack) return;
            animator.SetTrigger("attack");
            animator.SetInteger("attackIndex", 1);
        }
    }
    public void SwordAttack()
    {
        LockMovement();
        audioManager.PlaySFX(audioManager.player_attack);
    }
    public void SwordAttackDealDmg() 
    { 
        if (spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
            //Debug.Log("attacking left");
        }
        else
        {
            //Debug.Log("attacking right");
            swordAttack.AttackRight();
        }
    }
    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }
    public void LockMovement()
    {
        canMove = false;
    }
    public void UnlockMovement()
    {
        canMove = true;
    }
    
}
