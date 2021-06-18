using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float weakJump;
    [SerializeField] private float strongJump;

    private Rigidbody2D rb;
    private int pushCount;
    private bool isDead;
    private float direction;
    private float lastDirection;
    private Animator anim;
    private static readonly int Direction = Animator.StringToHash("Direction");
    private static readonly int Jump = Animator.StringToHash("Jump");

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            direction = Input.mousePosition.x > Screen.width / 2 ? 1 : -1;
        }
        else
        {
            direction = 0;
        }
        
    }

    private void Move()
    {
        if (isDead) return;
        
        /*direction = Input.GetAxisRaw("Horizontal");*/
        lastDirection = direction != 0 ? direction : lastDirection;

        anim.SetFloat(Direction, lastDirection);
        rb.velocity = new Vector2(moveSpeed * direction, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;
        switch (other.tag)
        {
            case "Bananas":
                rb.velocity = new Vector2(rb.velocity.x, strongJump);
                other.gameObject.SetActive(false);
                pushCount++;
                break;
            
            case "Banana":
                rb.velocity = new Vector2(rb.velocity.x, weakJump);
                other.gameObject.SetActive(false);
                pushCount++;
                break;
            
            case "Bird":
                isDead = true;
                rb.velocity = Vector2.zero;
                GameManager.instance.RestartGame();
                break;
            
            case "FallDetector":
                isDead = true;
                GameManager.instance.RestartGame();
                break;
        }
        
        anim.SetTrigger(Jump);

        if (pushCount == 2)
        {
            pushCount = 0;
            PlatformSpawner.instance.SpawnPlatforms();
        }
    }
}
