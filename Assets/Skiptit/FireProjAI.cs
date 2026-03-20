using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FireProjAI : MonoBehaviour
{
    public Vector3 dir;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject sprite;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
        
        dir = transform.forward;
        transform.rotation = quaternion.identity;
        sprite.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg, Vector3.forward);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += dir * 0.125f;
        if (Physics2D.BoxCast(transform.position, new Vector2(0.1f, 0.1f), 0, transform.forward, 0.4f, groundLayer)) Destroy(gameObject);
    }
}
