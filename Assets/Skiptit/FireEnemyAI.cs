using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FireEnemyAI : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb;
    public float AttTimer;
    public float dir;
    [SerializeField] private LayerMask lm;
    [SerializeField] private Animator an;
    [SerializeField] private GameObject fireball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity *= 0.7f;
        if (player == null) return;

        Vector2 rayDir = player.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, Vector2.Distance(player.position, transform.position), lm);
        Debug.DrawRay(transform.position, rayDir);

        if (hit)
        {
            AttTimer = 0f;
            Debug.Log("RAY HIT");
            return;
        }

        if (Vector3.Distance(player.position, transform.position) < 8f)
        {
            AttTimer += Time.deltaTime;

            bool facingLeft = player.position.x > transform.position.x;

            Vector3 localScale = transform.localScale;
            localScale.x = facingLeft ? -1f : 1f;
            transform.localScale = localScale;

            Vector2 vel = new Vector2(0.875f * (facingLeft ? 1f : -1f), 0f);
            if (AttTimer < 1f) rb.velocity += vel;
            else
            {
                rb.velocity -= vel * 0.375f;
                if (AttTimer > 1.25f) {
                    Instantiate(fireball, transform.position, Quaternion.LookRotation(rayDir));
                    AttTimer = 0f;
                }
            }
        }
        else AttTimer = 0f;
    }
}
