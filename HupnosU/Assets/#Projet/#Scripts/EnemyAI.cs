using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D), typeof(Seeker))]

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float detectionRange = 8f;
    public float attackRange = 2.5f;
    public float speed = 120f;
    public float nextWaypointDistance = 1f;
    private Seeker seeker;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private EnemyHealth health;
    private EnemyCombat combat;
    private Path path;
    private int currentWaypoint = 0;

    void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        health = GetComponent<EnemyHealth>();
        combat = GetComponent<EnemyCombat>();
    }

    void Start()
    {
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (!health.IsAlive) return;
        if (!seeker.IsDone()) return;

        float dist = Vector2.Distance(transform.position, target.position);
        if (dist > detectionRange) return;

        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        if (!health.IsAlive) return;
        anim.SetFloat("Speed", rb.linearVelocity.sqrMagnitude);
        FlipTowardsPlayer();
    }

    void FixedUpdate()
    {
        if (!health.IsAlive)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float dist = Vector2.Distance(transform.position, target.position);
        if (dist > attackRange)
        {
            MoveAlongPath();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            combat.TryAttack(attackRange);
        }
    }

    void MoveAlongPath()
    {
        if (path == null || currentWaypoint >= path.vectorPath.Count)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 velocity = direction * speed * Time.fixedDeltaTime;
        rb.linearVelocity = velocity;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
            currentWaypoint++;
    }

    void FlipTowardsPlayer()
    {
        if (target == null) return;
        sr.flipX = target.position.x < transform.position.x;
    }
}

