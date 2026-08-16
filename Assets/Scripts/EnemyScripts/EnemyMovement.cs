using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    private Animator animator;
    private GameObject player;

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    [SerializeField] float runMod = 1.45f;
    private Vector2 movement;

    [Header("Wander Settings")]
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private Collider2D collider;

    [Header("Line of Sight Settings")]
    public bool hasLOS = false;
    [SerializeField] private float viewDistance = 10f;
    // IMPORTANT: This mask must include BOTH the Player layer AND the Obstacle/Wall layer
    [SerializeField] private LayerMask targetLayers; 
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        latestDirectionChangeTime = 0f;
        collider = GetComponent<Collider2D>(); 
        CalculateNewMovementVector();
    }

    void Update()
    {
        // 1. Handle AI Logic & Input Vectors in Update
        if (hasLOS && player != null)
        {
            // Normalized so speed remains constant regardless of distance
            movement = ((Vector2)(player.transform.position - transform.position)).normalized;
        }
        else
        {
            if (Time.time - latestDirectionChangeTime > directionChangeTime)
            {
                latestDirectionChangeTime = Time.time;
                CalculateNewMovementVector();
            }
        }

        // 2. Keep animations updated every frame
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        // 3. Handle Physics Movement here
        float currentSpeed = moveSpeed * (hasLOS ? runMod : 1f); 
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);

        // 4. Handle Raycasting
        if (player != null)
        {
            CheckLineOfSight();
        }
    }

    void CalculateNewMovementVector()
    {
        movement = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
    }

    private void UpdateAnimation()
    {
        if (movement.sqrMagnitude > 0.01f) 
        {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void CheckLineOfSight()
    {
        Vector2 origin = transform.position;
        Vector2 direction = (Vector2)(player.transform.position - transform.position);
        float distance = direction.magnitude;

        // Caps the raycast to a max view distance so enemies don't see across the whole map
        if (distance > viewDistance)
        {
            hasLOS = false;
            Debug.DrawRay(origin, direction.normalized * viewDistance, Color.red);
            return;
        }

        RaycastHit2D ray = Physics2D.Raycast(origin, direction.normalized, distance, targetLayers);
        
        if (ray.collider != null)
        {
            // If the first thing the ray hits is the player, we have LOS
            hasLOS = ray.collider.CompareTag("Player");
            
            if (hasLOS)
            {
                Debug.DrawRay(origin, direction, Color.green);
            }
            else
            {
                // Hit a wall or obstacle instead
                Debug.DrawRay(origin, direction, Color.red);
            }
        }
        else
        {
            hasLOS = false;
        }
    }
}
