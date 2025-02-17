using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Enemy : MonoBehaviour
{
    
    public float damage = 1f;
    public float knockbackForce = 200f;
    public float moveSpeed = 500f;
    public float maxSpeed = 3f;
    public float idleFriction = 0.6f;
    bool isMoving = false;
    

    public DetectionCircle detectionCircle;
    Rigidbody2D rb;
    Animator animator;
    DamageableChar damagaebleChar;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damagaebleChar = GetComponent<DamageableChar>();
    }
    private void FixedUpdate()
    {
        if (damagaebleChar.Targetable && detectionCircle.detectedObjects.Count > 0)
        {
            Vector2 direction = (detectionCircle.detectedObjects[0].transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
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
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            IDamagable damageable = col.collider.GetComponent<IDamagable>();
            if (damageable != null)
            {
                Vector3 parentPosition = transform.position;
                Vector2 direction = (col.collider.transform.position - parentPosition).normalized;
                Vector2 knockback = direction * knockbackForce;
                damageable.OnHit(damage, knockback);
            }
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
}
