using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    Vector2 moveInput;

    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving)
            {
                return IsRunning ? runSpeed : walkSpeed;
            }
            else
            {
                return 0f;
            }
        }
    }
    [SerializeField]
    private bool _isMoving = false;
    [SerializeField]
    private bool _isRunning = false;


    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }

        private set
        {
            _isMoving = value;
            animator.SetBool("IsMoving", value);
        }
    }

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool("IsRunning", value);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;

        }
    }

    Rigidbody2D rb;
    Animator animator;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void onRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = context.ReadValueAsButton();

        }

        else if (context.canceled)
        {
            IsRunning = context.ReadValueAsButton();
        }
    }
}
