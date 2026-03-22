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
    private AudioSource audiosource;
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
            return;
        }

        if (Vector3.Distance(player.position, transform.position) < 12f)
        {
            AttTimer += Time.deltaTime;

            bool facingLeft = player.position.x > transform.position.x;

            Vector3 localScale = transform.localScale;
            localScale.x = facingLeft ? -1f : 1f;
            transform.localScale = localScale;

            if (AttTimer > 0.5f) {
                audiosource = GetComponent<AudioSource>();
                audiosource.PlayOneShot(audiosource.clip);

                Instantiate(fireball, transform.position, Quaternion.LookRotation(rayDir));
                AttTimer = 0f;
            }
        }
        else AttTimer = 0f;
    }
}
