using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Collider2D myBodyCollider;
    Collider2D myFeetCollider;

    [SerializeField] float playerSpeed = 10f;

    [SerializeField] float jumpSpeed = 25f;

    [SerializeField] float climbingSpeed = 10f;
    [SerializeField] Vector2 deathJump = new Vector2(0f, 30f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    bool isAlive = true;

    float startingGravity;

    [SerializeField] CinemachineShake cameraShake;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        startingGravity = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
        
    }


    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

        if (value.isPressed)
        {
             myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }


    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x*playerSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
            myAnimator.SetBool("isRunning", true);
        else
            myAnimator.SetBool("isRunning", false);
    }


    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
    }

    void ClimbLadder()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            Vector2 playerVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbingSpeed);
            myRigidbody.velocity = playerVelocity;
            myRigidbody.gravityScale = 0;

            bool playerClimbing = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("isClimbing", playerClimbing);
        }
        else
        {
            myRigidbody.gravityScale = startingGravity;
            myAnimator.SetBool("isClimbing", false);
        }  
    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            
            myRigidbody.velocity = deathJump;
            cameraShake.ShakeCamera(5, 0.5f);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }


    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
         
        if (value.isPressed)
        {
            
            Instantiate(bullet, gun.position, transform.rotation);

        }
    }
}
