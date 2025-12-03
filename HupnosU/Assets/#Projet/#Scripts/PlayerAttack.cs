using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public int damageAttack = 1;
    public int damageCry = 2;
    public SpriteRenderer sr;
    public Animator anim;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PerformAttack();
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
                    Debug.Log("Player: Attack Nightmare!");
                }
            }
        }
    }
}
