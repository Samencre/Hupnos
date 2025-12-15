using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 8;
    public int currentHealth;
    public bool isAlive = true;

    [Header("Damage Feedback")]
    public float invincibilityTime = 0.4f;
    private bool isInvincible = false;

    [Header("UI")]
    public Transform healthbarUI;
    public GameObject hpIcon;

    [Header("Visuals")]
    public Animator anim;
    public SpriteRenderer sr;

    void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthbarUI();
    }

    public void TakeDamage(int damage)
    {
        if (!isAlive || isInvincible) return;

        currentHealth -= damage;
        UpdateHealthbarUI();

        anim?.SetTrigger("Hit");
        StartCoroutine(DamageFeedback());

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isAlive = false;
            anim?.SetTrigger("Die");
            GameManager.Instance?.GameOver();
        }
    }

    IEnumerator DamageFeedback()
    {
        isInvincible = true;

        if (sr != null)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
        }

        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    public void Heal(int amount)
    {
        if (!isAlive) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthbarUI();
    }

    public void UpdateHealthbarUI()
    {
        if (healthbarUI == null || hpIcon == null) return;

        for (int i = healthbarUI.childCount - 1; i >= 0; i--)
            Destroy(healthbarUI.GetChild(i).gameObject);

        for (int i = 0; i < currentHealth; i++)
            Instantiate(hpIcon, healthbarUI);
    }
}


