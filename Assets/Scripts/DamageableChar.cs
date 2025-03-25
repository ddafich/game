using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

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

    AudioManager audioManager;
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
                setHealhText();
            }
            currentHealth = value;
            if (currentHealth <= 0)
            {
                canMove = false;
                Targetable = false;

                Death();
                if (gameObject.CompareTag("Player"))
                {
                    gameoverPanel.SetActive(true);
                }
                else if (gameObject.CompareTag("Enemy"))
                {
                    OnDeath?.Invoke();
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

    }
    private void setHealhText()
    {
        RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
        textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Canvas canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        textTransform.SetParent(canvas.transform);
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
        }
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        if (!Invincible)
        {
            Health -= damage;
            rb.AddForce(knockback, ForceMode2D.Impulse);
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
        // Ensure proper event cleanup to avoid accessing destroyed objects
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
}
    
