using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageableChar : MonoBehaviour, IDamagable
{
    Animator animator;
    Rigidbody2D rb;
    Collider2D col;

    bool isAlive = true;
    bool canMove = true;
    public bool disableSimulation;

    public float invincibleTime = 1f;
    public bool isInvincibleEnabled = false;
    private float invincibleTimeElapsed = 0f;


    bool targetable = true;
    bool invincible = false;
    

    [Header("stats")]
    public float maxHealth = 5f;
    public float currentHealth = 2f;
    public float damage = 1f;
    

    [Header("loot")]
    public List<LootItem> lootTable = new List<LootItem>();

    public HealthUI healthUI;
    public GameObject healthText;
    public GameObject gameoverPanel;
    public GameObject Ui;

    AudioManager audioManager;
    killcount killcount;

    public LayerMask wallLayerMask;
    public float maxKnockbackSpeed = 200f;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public float Health
    {
        set
        {
            if (value < currentHealth)
            {
                animator.SetTrigger("hit");
                
            }
            currentHealth = value;
            if (currentHealth <= 0)
            {
                canMove = false;
                Targetable = false;

                Death();
                if (gameObject.CompareTag("Enemy"))
                {
                    OnDeath?.Invoke();
                    killcount.AddKill();
                }
            }
        }
        get { return currentHealth; }
    }
    public bool Targetable
    {
        get
        {
            return targetable;
        }
        set
        {
            targetable = value;
            if (disableSimulation)
            {
                rb.simulated = false;
            }
            col.enabled = value;
        }
    }
    public bool Invincible
    {
        get { return invincible; }
        set
        {
            invincible = value;
            if (invincible == true)
            {
                invincibleTimeElapsed = 0f;
            }
        }
    }
    public float Damage
    {
        get { return damage; }
    }
    public void FixedUpdate()
    {
        if (Invincible)
        {
            invincibleTimeElapsed += Time.deltaTime;
            if (invincibleTimeElapsed > invincibleTime)
            {
                Invincible = false;
            }
        }
    }
    private void Start()
    {
        if (CompareTag("Player"))
        {
            if (healthUI != null)
            {
                healthUI.SetMaxHeart(maxHealth);
            }
            else
            {
                Debug.LogWarning("HealthUI not found for player.");
            }
        }
        ResetHealth();

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator.SetBool("isAlive", isAlive);

        HealthItem.OnHealthCollect += Heal;

        killcount = GameObject.Find("kill count").GetComponent<killcount>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        
    }
    private void setHealhText(float damage)
    {
        RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
        textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Canvas canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        textTransform.SetParent(canvas.transform);
        TextMeshProUGUI textComponent = textTransform.GetComponent<TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.text = "-"+damage.ToString(); // Set the text to display the damage
        }
    }
    private void Death()
    {
        animator.SetBool("isAlive", false);
        audioManager.PlaySFX(audioManager.slime_death);
        foreach (LootItem lootItem in lootTable)
        {
            if (UnityEngine.Random.Range(0f, 100f) <= lootItem.dropChance)
            {
                InstantiateLoot(lootItem.itemPrefab);
            }  
        }
    }
    private void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            GameObject droppedLoot = Instantiate(loot, transform.position, Quaternion.identity);
            rb = droppedLoot.GetComponent<Rigidbody2D>();
            if (rb)
            {
                Vector2 randomForce = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f));
                rb.linearDamping = 4f;
            }
        }
    }
    public void OnHit(float damage, Vector2 knockback)
    {
        if (!Invincible)
        {
            Health -= damage;
            setHealhText(damage);
            Vector2 limitedKnockback = Vector2.ClampMagnitude(knockback, maxKnockbackSpeed);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, limitedKnockback.normalized, 0.5f, wallLayerMask);
            if (!hit)
            {
                rb.linearVelocity = limitedKnockback;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
            audioManager.PlaySFX(audioManager.player_damaged);
            if (CompareTag("Player") && healthUI != null)
            {
                healthUI.UpdateHeart(currentHealth);
            }
            if (isInvincibleEnabled)
            {
                Invincible = true;
            }
        }
    }
    public void OnHit(float damage)
    {
        if (!Invincible)
        {
            Health -= damage;
            if (CompareTag("Player") && healthUI != null)
            {
                healthUI.UpdateHeart(currentHealth);
            }
            if (isInvincibleEnabled)
            {
                Invincible = true;
            }
        }
    }
    private void OnEnable()
    {
        HealthItem.OnHealthCollect += Heal;
    }
    private void OnDisable()
    {
        HealthItem.OnHealthCollect -= Heal;
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (CompareTag("Player"))
        {
            if (healthUI != null)
            {
                healthUI.UpdateHeart(currentHealth);
            }
            else
            {
                Debug.LogWarning("HealthUI is not assigned for the player.");
            }
        }
    }
    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth += amount;
        healthUI.SetMaxHeart(maxHealth);
        healthUI.UpdateHeart(currentHealth);
    }
    public void IncreaseDamage(int amount)
    {
        damage += amount;
    }
    void ResetHealth()
    {
        currentHealth = maxHealth;
    }
    public void DestroyOnDeath()
    {
        Destroy(gameObject, 3f);
    }
    public delegate void DeathAction();
    public event DeathAction OnDeath;
    public void SetGameoverPanel() 
    {
        gameoverPanel.SetActive(true);
        Ui.SetActive(false);
    }
}
    
