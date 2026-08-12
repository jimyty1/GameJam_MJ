using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody2D rb;
    public float moveSpeed = 3f;
    private float run;
    private float runMod;
    public float runMultiplier = 1.45f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
        
    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
        Debug.Log("Movement Input: " + movement);
    }
    private void OnRun(InputValue value)
    {
        run = value.Get<float>();
        runMod = run*runMultiplier;
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime * (1+runMod));
    }
}
