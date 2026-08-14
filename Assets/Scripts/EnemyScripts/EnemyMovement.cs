using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] float moveSpeed;
    Animator animator;
    [SerializeField] float runMod = 1.45f;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void IdleLoop()
    {
        
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime * (1+runMod));
    }
}
