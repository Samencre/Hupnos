using UnityEngine;
using System; 

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 4;
    private int currentHealth;
    public bool IsAlive { get; private set; } = true; //lisible par les autres scripts, mais modifiable uniquement ici
    private Animator anim;
    private Rigidbody2D rb;
    private EnemyAI ai;
    private EnemyCombat combat;
    public event Action OnDeath;

    void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ai = GetComponent<EnemyAI>();
        combat = GetComponent<EnemyCombat>();
    }

    public void TakeDamage(int damage)
    {
        if (!IsAlive) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
        else
            anim?.SetTrigger("Hit"); 
    }

    private void Die()
    {
        IsAlive = false;

        if (ai != null) ai.enabled = false;
        if (combat != null) combat.enabled = false;
        if (rb != null) rb.linearVelocity = Vector2.zero;

        if (anim != null)
        {
            anim.SetTrigger("Die");
            anim.SetLayerWeight(anim.GetLayerIndex("Base Layer"), 1f);
        }

        OnDeath?.Invoke();

        Destroy(gameObject, 2.5f);
    }
}

