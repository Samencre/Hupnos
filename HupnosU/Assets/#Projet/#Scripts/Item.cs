using UnityEngine;


public class Item : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public float itemRange = 1.5f; // Distance pour interagir

    void Update()
    {
        if (player == null) return;
        if (Input.GetKey(KeyCode.E) && Vector2.Distance(transform.position, player.position) <= itemRange)
        {
            if (animator != null)
                animator.SetTrigger("Collect");
            gameObject.SetActive(false);
        }
    }
}

//public enum ItemType { Cushion, Matches, Bracelet }
// public ItemType itemType;
// GameManager.Instance?.CollectItem(itemType);


