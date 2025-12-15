using UnityEngine;

public class Candles : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Animator animator;
    private PlayerHealth playerHealth;

    [Header("Settings")]
    public float candleRange = 1.5f; 
    public float safeZone = 4f; 
    public int heal = 1; 
    public float maxTime = 2f;

    private bool lightUp = false;
    private float currentTime = 0f;
    public GameObject lightZone; 

// permets aux autres scripts de lire la valeur de lightUp, mais pas de la modifier
    public bool IsLighted
{
    get { return lightUp; }
}
// public bool IsLit => lightUp;


void Start()
{
    if (player != null)
        playerHealth = player.GetComponent<PlayerHealth>();

    if (lightZone != null)
        lightZone.SetActive(false);
}

void Update()
{
    if (player == null || playerHealth == null) return;

    if (!lightUp && Input.GetKey(KeyCode.E) &&
        Vector2.Distance(transform.position, player.position) <= candleRange)
    {
        lightUp = true;
        animator?.SetBool("lightUp", true);

        if (lightZone != null)
            lightZone.SetActive(true);
    }
    ApplyCandleHealing();
}

    void ApplyCandleHealing()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= safeZone && lightUp)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= maxTime)
            {
                playerHealth.Heal(heal);
                currentTime = 0f;
            }
        }
        else
        {
            currentTime = 0f;
        }

    }
}



