using UnityEngine;
//Ne fonctionne malheureusement pas, trop de conditions
public class EnemyCombat : MonoBehaviour
{
    public int damage = 1;
    public float attackCooldown = 2f;
    public Transform target;
    private float cooldownTimer;
    private bool attackHit = false;
    private Animator anim;
    private EnemyHealth health;

    void Awake()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (!health.IsAlive) return;

        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
    }

    public void TryAttack(float attackRange)
    {
        if (cooldownTimer > 0 || !health.IsAlive) return;
        float dist = Vector2.Distance(transform.position, target.position);
        attackHit = dist <= attackRange;
        anim.SetTrigger("Attack");
        cooldownTimer = attackCooldown;
    }

    public void EndAttack()
    {
        if (!attackHit) return;
        PlayerHealth ph = target.GetComponent<PlayerHealth>();
        if (ph != null)
            ph.TakeDamage(damage);
        attackHit = false;
    }
}







