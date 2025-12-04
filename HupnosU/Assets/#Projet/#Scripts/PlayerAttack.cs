using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public int damage = 1;
    public int knockbackForce = 5;
    public SpriteRenderer sr;
    public Animator anim;
    public PlayerHealth playerHealth;



    void Update()
    {
        if (playerHealth.isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PerformAttack();
            }
        }
        
    }

    void PerformAttack()
    {
        anim.SetTrigger("Attack");
        Vector2 attackDirection = sr.flipX? Vector2.left : Vector2.right;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach(Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Nightmare"))
            {
                Vector2 directionToNightmare = (collider.transform.position - transform.position).normalized;
                if(Vector2.Dot(attackDirection, directionToNightmare) > 0)
                {
                    EnemyAI AI = collider.GetComponent<EnemyAI>();
                    AI.TakeDanage(damage);

                    Vector2 knockbackDirection = (collider.transform.position -transform.position).normalized;
                    AI.rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
