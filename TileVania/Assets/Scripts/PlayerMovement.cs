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
    Collider2D myCollider;


    [SerializeField] float playerSpeed = 8f;

    [SerializeField] float jumpSpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();

    }


    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

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
}
