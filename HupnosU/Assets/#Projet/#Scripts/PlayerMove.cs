using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 3f;
    private Vector2 move;
    public Rigidbody2D rb ; 
    public Animator anim;
    public SpriteRenderer sr;

    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal"); 
        move.y = Input.GetAxisRaw("Vertical");
        
        move = move.normalized;

        anim.SetFloat("Speed", move.sqrMagnitude); // sqr plus rapide

        if(move.x != 0)
        {
            sr.flipX = move.x < 0;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = move * speed;
    }
}

