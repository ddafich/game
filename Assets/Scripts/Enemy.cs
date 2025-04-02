using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Enemy : MonoBehaviour
{
    
    public float knockbackForce = 200f;
    public float moveSpeed = 500f;
    public float maxSpeed = 3f;
    public float idleFriction = 0.6f;
    bool isMoving = false;
    bool canMove = true;

    public DetectionCircle detectionCircle;
    Rigidbody2D rb;
    Animator animator;
    DamageableChar damagaebleChar;
    SpriteRenderer spriteRenderer;
    Collider2D playerCollider;
    
    public HashSet<Collider2D> playerInRange = new HashSet<Collider2D>();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damagaebleChar = GetComponent<DamageableChar>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (canMove == true && damagaebleChar.Targetable && detectionCircle.detectedObjects.Count > 0)
        {
            Vector2 direction = (detectionCircle.detectedObjects[0].transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else{
                spriteRenderer.flipX = false;   
            }
            IsMoving =true;
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                float limitedSpeed = Mathf.Lerp(rb.linearVelocity.magnitude, maxSpeed, idleFriction);
                rb.linearVelocity = rb.linearVelocity.normalized * limitedSpeed;
            }
        }
        else
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, idleFriction);
            IsMoving = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered attack range.");
            playerInRange.Add(col); 
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player left attack range.");
            playerInRange.Remove(col);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            
            animator.SetTrigger("attack");
        }
    }
    void DealDamage()
    {
        foreach (Collider2D playerCollider in playerInRange)
        {
            if (playerCollider != null)
            {
                IDamagable damageable = playerCollider.GetComponent<IDamagable>();
                if (damageable != null)
                {
                    Vector3 parentPosition = transform.position;
                    Vector2 direction = (playerCollider.transform.position - parentPosition).normalized;
                    Vector2 knockback = direction * knockbackForce;
                    damageable.OnHit(damagaebleChar.damage, knockback);
                }
            }
            else { break; }
        }
    }
    void LockMovement()
    {
        canMove = false;
    }
    void UnlockMovement()
    {
        canMove=true;
    }
    public bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }
}
