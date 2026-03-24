using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefAI : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb;
    public float AttTimer;
    public float dir;
    [SerializeField] private LayerMask lm;
    [SerializeField] private Animator an;
    [SerializeField] private GameObject fireball;
    private AudioSource audiosource;
    private bool Attacked;
    private bool facingLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity *= 0.8f;
        if (player == null) return;

        Vector2 rayDir = player.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, Vector2.Distance(player.position, transform.position), lm);
        Debug.DrawRay(transform.position, rayDir);

        an.SetBool("Act", AttTimer > 0.5f && AttTimer < 1f);

        if (AttTimer < 1f) facingLeft = player.position.x > transform.position.x;
        Vector3 localScale = transform.localScale;
        localScale.x = facingLeft ? -1f : 1f;
        transform.localScale = localScale;

        Vector2 vel = new Vector2(facingLeft ? 1f : -1f, 0f);

        if (Vector3.Distance(player.position, transform.position) < 10f)
        {
            an.SetBool("Walk", false);

            AttTimer += Time.deltaTime;

            if (AttTimer > 0.75f && AttTimer < 1f) rb.velocity -= vel * 0.75f;

            if (AttTimer > 1f) {
                if (AttTimer < 1.05f) rb.velocity += vel * 2f;

                if (!Attacked)
                {
                    audiosource = GetComponent<AudioSource>();
                    audiosource.PlayOneShot(audiosource.clip);
                    
                    for (int i = 0; i < 3; i++)
                    {
                        GameObject go = Instantiate(fireball, transform.position, Quaternion.LookRotation(rayDir + new Vector2(rayDir.x * Random.Range(0.5f, 1.5f), 12f + (i * 4f))));
                        go.GetComponent<FireProjAI>().Phasethrough = true;
                    }
                    Attacked = true;
                }
                
                if (AttTimer > 1.25f) {
                    Attacked = false;
                    AttTimer = 0f;
                }
            }
        }
        else
        {
            if (!hit) rb.velocity += vel;
            an.SetBool("Walk", true);

            Attacked = false;
            AttTimer = 0f;
        }
    }
}
