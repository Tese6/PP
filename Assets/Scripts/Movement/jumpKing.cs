using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpKing : MonoBehaviour
{
    public float WalkSpeedBoostValue;
    public float MaxSpeed;
    protected float Move;
    protected float walkSpeed;
    protected bool isGrounded;
    private Rigidbody2D rb;
    public LayerMask groundMask;

    public KeyCode jumpbutton = KeyCode.Space;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    public PhysicsMaterial2D bounceMat, normalMat;
    protected bool canJump = true;
    protected float jumpValue = 0;
    public float MaxJumpValue = 15f;
    public float MinimumJumpValue = 5f;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        jumpValue = MinimumJumpValue;
    }

    void Update()
    {
        if (isGrounded && Input.GetKey(right))
        {
            transform.Translate(new Vector2(1, 0) * walkSpeed * Time.deltaTime);
            Move = 1 * walkSpeed / 10;
            walkSpeed += WalkSpeedBoostValue;
        }
        if (isGrounded && Input.GetKey(left))
        {
            transform.Translate(new Vector2(-1, 0) * walkSpeed * Time.deltaTime);
            Move = -1 * walkSpeed / 10;
            walkSpeed += WalkSpeedBoostValue;
        }
        if (Move >= 1)
        {
            Move = 1;
        }
        if (Move <= -1)
        {
            Move = -1;
        }
        if (Input.GetKeyUp(right) || Input.GetKeyUp(left))
        {
            walkSpeed = 2f;
        }

        WalkSpeedBoost();

        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f),
        new Vector2(0.9f, 0.4f), 0f, groundMask);

        if(jumpValue > 0)
        {
            rb.sharedMaterial = bounceMat;
        }
        else
        {
            rb.sharedMaterial = normalMat;
        }
        if(Input.GetKey(jumpbutton) && isGrounded && canJump)
        {
            jumpValue += 0.1f;
        }
        if(Input.GetKeyDown(jumpbutton) && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
        if(jumpValue >= MaxJumpValue && isGrounded)
        {
            float tempx = Move * walkSpeed;
            float tempy = jumpValue;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0f);
        }
        if(Input.GetKeyUp(jumpbutton))
        {
            if(isGrounded)
            {
                rb.velocity = new Vector2(Move * walkSpeed, jumpValue);
                Invoke("ResetJump", 0f);
            }
        }
        if (isGrounded)
        {
            canJump = true;
        }
    }

    void WalkSpeedBoost()
    {
        if (!isGrounded && Move >= 1 && canJump == true)
        {
            walkSpeed -= WalkSpeedBoostValue / 10;
            transform.Translate(new Vector2(1, 0) * walkSpeed * Time.deltaTime);
        }
        if (!isGrounded && Move <= -1 && canJump == true)
        {
            walkSpeed -= WalkSpeedBoostValue / 10;
            transform.Translate(new Vector2(-1, 0) * walkSpeed * Time.deltaTime);
        }
        if (walkSpeed >= MaxSpeed)
        {
            walkSpeed = MaxSpeed;
        }
    }

    void ResetJump()
    {
        canJump = false;
        jumpValue = MinimumJumpValue;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.9f, 0.2f));
    }
}
