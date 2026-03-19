using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FireProjAI : MonoBehaviour
{
    public Vector3 dir;
    [SerializeField] GameObject sprite;
    // Start is called before the first frame update
    void Start()
    {
        dir = transform.forward;
        transform.rotation = quaternion.identity;
        sprite.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg, Vector3.forward);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += dir * 0.125f;
    }
}
