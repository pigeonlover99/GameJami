using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEnemyAI : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb;
    public float AttTimer;
    public float dir;
    [SerializeField] private LayerMask pm;
    [SerializeField] private LayerMask lm;
    [SerializeField] private Animator an;
    private AudioSource audiosource;
    private bool PlayedSound;
    // Start is called before the first frame update
    void Start()
    {
        if (dir == 0f) dir = 1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null) return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.75f, lm);
        Debug.DrawRay(transform.position, Vector2.down);

        if (!hit) {
            dir = -dir;
        }

        Vector3 localScale = transform.localScale;
        localScale.x = -dir;
        transform.localScale = localScale;

        transform.position += new Vector3(dir * (AttTimer > 35f ? 0.175f : 0.066f), 0f, 0f);
        an.SetBool("Attack", AttTimer > 30f);

        Vector3 rayDir = new Vector3(dir, 0f, 0f);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, rayDir, 2f, pm);
        Debug.DrawRay(transform.position, rayDir, Color.red);

        if (hit2 && AttTimer <= 0f)
        {
            audiosource = GetComponent<AudioSource>();
            audiosource.PlayOneShot(audiosource.clip);

            AttTimer = 45f;
        }

        if (AttTimer > 0f) AttTimer -= 1f;
    }
}
