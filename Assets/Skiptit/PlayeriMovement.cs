using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 13f;
    private bool isFacingRight = true;

    public float IFrames;
    [SerializeField] float MaxIframes;
    [SerializeField] private Vector2 size;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Animator animator;
    [SerializeField] private Slider fireBar;
    [SerializeField] private Slider iceBar;
    [SerializeField] private AudioSource JumpSource;
    [SerializeField] private AudioSource DamageSource;
    [SerializeField] private AudioSource WalkSource;

    public int FireHealth;
    public int IceHealth;
    public int MaxHealth;
    public int CoyoteTime;
    public float WalkSoundDelay;

    public bool GodMode; 
    public Vector3 CheckpointPos;
    
    private CheckpointSystem check;
    private AudioSource audiosource;
    
    private void Start()
    {
        FireHealth = MaxHealth;
        IceHealth = MaxHealth;

        check = GameObject.FindGameObjectWithTag("Respawn").GetComponent<CheckpointSystem>();
        transform.position = check.Pos;
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetBool("isWalking", horizontal != 0f);
        WalkSoundDelay++;

        if (horizontal == 0f) rb.velocity = new Vector2(0f, rb.velocity.y);
        else
        {
            if (WalkSoundDelay > 25f && IsGrounded())
            {
                WalkSource.PlayOneShot(WalkSource.clip);
                WalkSoundDelay = 0f;
            }

            float spd = speed;
            if (GodMode) spd *= 2.5f;

            if (horizontal > 0f) rb.velocity = new Vector2(spd, rb.velocity.y);
            else rb.velocity = new Vector2(-spd, rb.velocity.y);
        }

        rb.gravityScale = 3f;
        if (IFrames < (MaxIframes * 0.66))
        {
            if (Input.GetButton("Jump"))
            {
                if (CoyoteTime > 0) {
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                    JumpSource.Play();
                }
                else if (rb.velocity.y > 0f)
                {
                    rb.gravityScale = 2.5f;
                }
                CoyoteTime = 0;
            }
            else if (!IsGrounded()) rb.gravityScale = 5f;
            
            Flip();
            //rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            rb.AddForce(new Vector2(horizontal * speed, 0f));
        }

        if (IFrames > 0f) IFrames -= 1f;
        if (IceHealth <= 0 || FireHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (fireBar != null) {
            fireBar.value = MaxHealth - FireHealth;
            if (FireHealth == MaxHealth) fireBar.value += 0.375f;

            fireBar.maxValue = MaxHealth;
        }
        if (iceBar != null) {
            iceBar.value = MaxHealth - IceHealth;
            if (IceHealth == MaxHealth) iceBar.value += 0.375f;

            iceBar.maxValue = MaxHealth;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        DamageBeh(collision.collider);
    }

    private void DamageBeh(Collider2D collision)
    {
        if ((collision.tag == "HostileFire" || collision.tag == "HostileIce") && IFrames <= 0f && !GodMode)
        {
            Debug.Log("!!!!!! HIT !!!!!!");

            bool facingLeft = transform.position.x > collision.transform.position.x;
            Vector2 vel = facingLeft ? new Vector2(6f, 3f) : new Vector2(-6f, 3f);
            rb.AddForce(vel, ForceMode2D.Impulse);

            if (collision.tag == "HostileFire") FireHealth--;
            else IceHealth--;

            IFrames = MaxIframes;
            DamageSource.PlayOneShot(DamageSource.clip);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        DamageBeh(collision);
    }

    private bool IsGrounded()
    {
        if (GodMode) {
            CoyoteTime = 10;
            return true;
        }

        if (Physics2D.BoxCast(transform.position, size, 0, -transform.up, 0.4f, groundLayer))
        {
            if (CoyoteTime < 10) CoyoteTime++;
            return true;
        }
        else {
            if (CoyoteTime > 0) CoyoteTime--;
            return false;
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
}
    }
}
