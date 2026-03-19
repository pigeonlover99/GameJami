using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEnemyAI : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb;
    public float AttTimer;
    public float dir;
    [SerializeField] private LayerMask lm;
    [SerializeField] private Animator an;
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

        an.SetBool("Attack", AttTimer > 1f);
        if (hit)
        {
            AttTimer = 0f;
            Debug.Log("RAY HIT");
            return;
        }

        if (Vector3.Distance(player.position, transform.position) < 15f)
        {
            if (AttTimer < 0.5f || AttTimer >= 1f || Vector3.Distance(player.position, transform.position) < 4f) AttTimer += Time.deltaTime;

            bool facingLeft = player.position.x > transform.position.x;

            Vector3 localScale = transform.localScale;
            localScale.x = facingLeft ? -1f : 1f;
            transform.localScale = localScale;

            Vector2 vel = new Vector2(0.875f * (facingLeft ? 1f : -1f), 0f);
            if (AttTimer < 1f) rb.velocity += vel;
            else
            {
                if (AttTimer < 1.25f)
                {
                    rb.velocity -= vel * 1.25f;
                    dir = facingLeft ? 1f : -1f;
                }
                else
                {
                    if (Vector3.Distance(player.position, transform.position) > 0.2f) rb.velocity += new Vector2(dir * 4f, 0);
                    else rb.velocity *= 0.4f;

                    if (AttTimer > 1.5f) AttTimer = 0f;
                }
            }
        }
        else AttTimer = 0f;
    }
}
