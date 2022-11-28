using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpKing : MonoBehaviour
{
    private float Move;
    public float walkSpeed;
    public bool isGrounded;
    private Rigidbody2D rb;
    public LayerMask groundMask;

    public KeyCode jumpbutton = KeyCode.Space;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    public PhysicsMaterial2D bounceMat, normalMat;
    public bool canJump = true;
    public float jumpValue = 0.0f;
    

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isGrounded && Input.GetKey(right))
        {
            transform.Translate(new Vector2(1, 0) * walkSpeed * Time.deltaTime);
            walkSpeed += 0.1f;
            Move = 1 * walkSpeed / 10;
        }
        if (isGrounded && Input.GetKey(left))
        {
            transform.Translate(new Vector2(-1, 0) * walkSpeed * Time.deltaTime);
            walkSpeed += 0.1f;
            Move = -1 * walkSpeed / 10;
        }
        if (!Input.GetKey(right) && !Input.GetKey(left))
        {
            Move = 0;
        }
        if (Move >= 1)
        {
            Move = 1;
        }
        if (Move <= -1)
        {
            Move = -1;
        }
        if (walkSpeed >= 10)
        {
            walkSpeed = 10;
        }
        if (walkSpeed <= -10)
        {
            walkSpeed = -10;
        }
        if (Input.GetKeyUp(right) || Input.GetKeyUp(left))
        {
            walkSpeed = 2f;
        }
        if (!isGrounded && walkSpeed > 0)
        {
            walkSpeed -= 0.01f;
        }
        if (!isGrounded && walkSpeed < 0)
        {
            walkSpeed += 0.01f;
        }

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

        if(jumpValue >= 10f && isGrounded)
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
                jumpValue = 5.0f;
            }
            canJump = true;
        }
    }

    void ResetJump()
    {
        canJump = false;
        jumpValue = 5;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.9f, 0.2f));
    }
}
