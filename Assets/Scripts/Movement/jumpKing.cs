using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpKing : MonoBehaviour
{
    public Animator MovementAnim;
    public float WalkSpeedBoostValue;
    public float MaxSpeed;
    protected float Move;
    protected float walkSpeed;
    protected bool isGrounded;
    private Rigidbody2D rb;
    public LayerMask groundMask;

    public float FallingThreshold = -0.1f;
    public bool Falling = false;

    public KeyCode jumpbutton = KeyCode.Space;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    public PhysicsMaterial2D bounceMat, normalMat;
    protected bool canJump = true;
    protected float jumpValue = 0;
    public float MaxJumpValue = 15f;
    public float MinimumJumpValue = 5f;

    public float IsGroundedCheckOffSet;
    public float IsGroundedCheckSizeX;
    public float IsGroundedCheckSizeY;
    public float RestartXPosition;
    public float RestartYPosition;

    public static int PlayerHealth = 100;
    public static bool Won = false;
    public static bool OutOfBounds = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        jumpValue = MinimumJumpValue;
        PlayerHealth = 100;
        Won = false;
        OutOfBounds = false;
    }

    void Update()
    {
        if (isGrounded && Input.GetKey(right) && PlayerHealth > 0)
        {
            transform.Translate(new Vector2(1, 0) * walkSpeed * Time.deltaTime);
            Move = 1 * walkSpeed;
            walkSpeed += WalkSpeedBoostValue;
            transform.localScale = new Vector2(0.4f, 0.4f);
            MovementAnim.SetBool("IsRunning", true);
        }
        if (isGrounded && Input.GetKey(left) && PlayerHealth > 0)
        {
            transform.Translate(new Vector2(-1, 0) * walkSpeed * Time.deltaTime);
            Move = -1 * walkSpeed;
            walkSpeed += WalkSpeedBoostValue;
            transform.localScale = new Vector2(-0.4f, 0.4f);
            MovementAnim.SetBool("IsRunning", true);
        }
        if(isGrounded && !Input.GetKey(right) && !Input.GetKey(left) && PlayerHealth > 0)
        {
            MovementAnim.SetBool("IsRunning", false);
        }
        if (Move >= 1)
        {
            Move = 1;
        }
        if (Move <= -1)
        {
            Move = -1;
        }
        if (rb.velocity.y > FallingThreshold)
        {
            Falling = false;
            MovementAnim.SetBool("IsFalling", false);
            MovementAnim.SetBool("IsJumping", true);
        }
        if (rb.velocity.y < FallingThreshold)
        {
            Falling = true;
            MovementAnim.SetBool("IsFalling", true);
            MovementAnim.SetBool("IsJumping", false);
        }
        else
        {
            Falling = false;
            MovementAnim.SetBool("IsFalling", false);
        }
        if (OutOfBounds == true)
        {
            gameObject.transform.position = new Vector2(RestartXPosition, RestartYPosition);
            PlayerHealth = 100;
            Won = false;
            OutOfBounds = false;
        }

        WalkSpeedBoost();

        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - IsGroundedCheckOffSet),
        new Vector2(IsGroundedCheckSizeX, IsGroundedCheckSizeY), 0f, groundMask);

        if(jumpValue > 0)
        {
            rb.sharedMaterial = bounceMat;
        }
        else
        {
            rb.sharedMaterial = normalMat;
        }
        if(Input.GetKey(jumpbutton) && isGrounded && canJump && PlayerHealth > 0)
        {
            jumpValue += 0.1f;
        }
        if(Input.GetKeyDown(jumpbutton) && isGrounded && canJump && PlayerHealth > 0)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
        if(jumpValue >= MaxJumpValue && isGrounded && PlayerHealth > 0)
        {
            float tempx = Move * walkSpeed;
            float tempy = jumpValue;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0f);
        }
        if(Input.GetKeyUp(jumpbutton) && PlayerHealth > 0)
        {
            if(isGrounded)
            {
                rb.velocity = new Vector2(Move * walkSpeed, jumpValue);
                Invoke("ResetJump", 0f);
            }
        }
        if (isGrounded && PlayerHealth > 0)
        {
            canJump = true;
            MovementAnim.SetBool("IsJumping", false);
        }
        if (PlayerHealth <= 0)
        {
            MovementAnim.SetBool("Dead", true);
        }
        else
        {
            MovementAnim.SetBool("Dead", false);
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
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - IsGroundedCheckOffSet), new Vector2(IsGroundedCheckSizeX, IsGroundedCheckSizeY));
    }

    void OnTriggerEnter2D(Collider2D enviroment)
    {
        if (enviroment.gameObject.tag == "EnviromentDanger")
        {
            PlayerHealth = 0;
        }
        if (enviroment.gameObject.tag == "Finish")
        {
            Won = true;
        }
        if (enviroment.gameObject.tag == "DeathZone")
        {
            OutOfBounds = true;
        }
    }
}