using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 8;
    [SerializeField]private int currentHealth;
    public bool isAlive = true;
    public Transform healthbarUI;
    public GameObject hp;
    public Animator anim;
    public SpriteRenderer sr;
    public bool healthUp = false;

    void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthbarUI();
    }

    public void TakeDanage(int damage)
    {
        if (isAlive)
        {
            currentHealth -= damage;
            UpdateHealthbarUI();

            if(currentHealth <= 0)
            {
                isAlive = false;
                anim.SetTrigger("Die");
            }
        }
    }

    public void UpdateHealthbarUI()
    {
        foreach (Transform child in healthbarUI)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentHealth; i++)
        {
            Instantiate(hp, healthbarUI);
        }
    }

    public void DisablePlayerVisual()
    {
        sr.enabled = false;
    }

        public void Heal(int heal)
    {
        if(currentHealth < maxHealth)
        {
            healthUp= true;
            currentHealth += heal;
            anim.SetTrigger("Heal");
            UpdateHealthbarUI();
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            
        }
        healthUp = false;
    }


}
