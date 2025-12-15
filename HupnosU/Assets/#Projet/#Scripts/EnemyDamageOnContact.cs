using UnityEngine;

public class EnemyDamageOnContact : MonoBehaviour
{
    public int damage = 1;
    public float damageCooldown = 1.5f;
    private float timer;
    private PlayerHealth playerHealth;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            TryDamage();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TryDamage();
        }
    }

    void TryDamage()
    {
        if (playerHealth == null || !playerHealth.isAlive) return;
        if (timer > 0) return;

        playerHealth.TakeDamage(damage);
        timer = damageCooldown;
    }

    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
    }
}

