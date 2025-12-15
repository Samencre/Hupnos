using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackRange = 1.5f;
    public int damage = 1;
    public SpriteRenderer sr;
    public Animator anim;
    public PlayerHealth playerHealth;

    void Update()
    {
        if (!playerHealth.isAlive) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
            PerformAttack();
    }

    void PerformAttack()
    {
        anim.SetTrigger("Attack");
        Vector2 attackDirection = sr.flipX ? Vector2.left : Vector2.right;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D col in hitColliders)
        {
            if (!col.CompareTag("Nightmare")) continue;
            EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
            if (enemyHealth == null) continue;
            Vector2 dirToEnemy = (col.transform.position - transform.position).normalized;
            if (Vector2.Dot(attackDirection, dirToEnemy) <= 0) continue;
            enemyHealth.TakeDamage(damage);
        }
    }
}

