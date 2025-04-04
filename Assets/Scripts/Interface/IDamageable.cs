using UnityEngine;

public interface IDamagable
{
    public float Health
    {
        get; set;
    }
    public bool Targetable
    {
        get; set;
    }
    public bool Invincible { get; set; }
    public void OnHit(float damage, Vector2 knockback);
    public void OnHit(float damage);
    
    public void DestroyOnDeath();
}
