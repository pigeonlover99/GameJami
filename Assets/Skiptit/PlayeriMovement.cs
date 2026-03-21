using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public AudioSource audiosource2;

    public int FireHealth;
    public int IceHealth;
    public int MaxHealth;
    public int CoyoteTime;
    
    private AudioSource audiosource;
    
    private void Start()
    {
        FireHealth = MaxHealth;
        IceHealth = MaxHealth;
       
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetBool("isWalking", horizontal != 0f);

        if (horizontal == 0f) rb.velocity = new Vector2(0f, rb.velocity.y);
        else
        {
            if (horizontal > 0f) rb.velocity = new Vector2(speed, rb.velocity.y);
            else rb.velocity = new Vector2(-speed, rb.velocity.y);
        }

        rb.gravityScale = 3f;
        if (IFrames < (MaxIframes * 0.66))
        {
            if (Input.GetButton("Jump"))
            {
                if (CoyoteTime > 0) {
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                    audiosource = GetComponent<AudioSource>();
                    audiosource.Play();
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
        if (IceHealth <= 0 || FireHealth <= 0) Destroy(gameObject);

        if (fireBar != null) {
            fireBar.value = MaxHealth - FireHealth;
            fireBar.maxValue = MaxHealth;
        }
        if (iceBar != null) {
            iceBar.value = MaxHealth - IceHealth;
            iceBar.maxValue = MaxHealth;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        DamageBeh(collision.collider);
    }

    private void DamageBeh(Collider2D collision)
    {
        if ((collision.tag == "HostileFire" || collision.tag == "HostileIce") && IFrames <= 0f)
        {
            Debug.Log("!!!!!! HIT !!!!!!");

            bool facingLeft = transform.position.x > collision.transform.position.x;
            Vector2 vel = facingLeft ? new Vector2(6f, 3f) : new Vector2(-6f, 3f);
            rb.AddForce(vel, ForceMode2D.Impulse);

            if (collision.tag == "HostileFire") FireHealth--;
            else IceHealth--;

            IFrames = MaxIframes;
            audiosource2.PlayOneShot(audiosource2.clip);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        DamageBeh(collision);
    }

    private bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, size, 0, -transform.up, 0.4f, groundLayer))
        {
            if (CoyoteTime < 5) CoyoteTime++;
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
