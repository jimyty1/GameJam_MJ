using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] float moveSpeed = 3;
    Animator animator;
    [SerializeField] float runMod = 1.45f;

    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    
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
        if (Time.time - latestDirectionChangeTime > directionChangeTime){
            latestDirectionChangeTime = Time.time;
            CalcuateNewMovementVector();
        }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime * (1+runMod));
    }



    void Start(){
        latestDirectionChangeTime = 0f;
        CalcuateNewMovementVector();
    }

    void CalcuateNewMovementVector(){
    //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movement = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
    }

}
