using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody2D rb;
    [SerializeField] float moveSpeed = 3f;
    private float run;
    private float runMod;
    [SerializeField] float runMultiplier = 1.45f;

    private Animator animator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
        
    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
        Debug.Log("Movement Input: " + movement);

        if (movement.x!= 0 || movement.y != 0)
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
    private void OnRun(InputValue value)
    {
        Debug.Log("run");
        run = value.Get<float>();
        runMod = run*runMultiplier;
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime * (1+runMod));
    }
}
