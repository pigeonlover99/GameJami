using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FireProjAI : MonoBehaviour
{
    public bool Phasethrough = false;
    public Vector3 dir;
    public Vector3 vel;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject sprite;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
        
        dir = transform.forward;
        vel = new Vector3(0f, 0f, 0f);

        transform.rotation = quaternion.identity;
        sprite.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg, Vector3.forward);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += dir * 0.125f + vel;
        if (Phasethrough)
        {
            vel.y -= 0.0025f;
            return;
        }

        if (Physics2D.BoxCast(transform.position, new Vector2(0.1f, 0.1f), 0, transform.forward, 0.4f, groundLayer)) Destroy(gameObject);
    }
}
