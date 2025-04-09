using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 3;
    public float knockbackForce = 40f;

    public Collider2D swordCollider;
    
    Vector2 rightAttackOffset;
    
    private void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        rightAttackOffset = transform.localPosition;
        swordCollider.enabled = false;
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x + 0.1f ,rightAttackOffset.y);
    }

    public void AttackLeft()
    {
        
        swordCollider.enabled = true;
        transform.localPosition = new Vector3((rightAttackOffset.x - 1.65f) , rightAttackOffset.y);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        IDamagable damageableObject = collider.GetComponent<IDamagable>();
        if (damageableObject != null)
        {
            Vector3 parentPosition = transform.parent.position;

            Vector2 direction = (Vector2)((collider.gameObject.transform.position - parentPosition)).normalized;
            Vector2 knockback = direction * knockbackForce;
            damageableObject.OnHit(damage, knockback);
        }
    }
    
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.collider.tag == "Enemy"){
    //        Enemy enemy = collision.collider.GetComponent<Enemy>();
    //        if (enemy != null)
    //        {
    //            Debug.Log("dealing dmg");
    //            enemy.Health -= damage;
    //        }
    //    }
    //}
}
