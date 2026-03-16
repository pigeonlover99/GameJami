using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public Rigidbody2D body;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Horizontal");

if(Mathf.Abs(xInput) > 0)
        {
            body.velocity = new Vector2(xInput*speed, body.velocity.y);
        }
if(Mathf.Abs(yInput) > 0)
        {
            body.velocity = new Vector2(yInput*speed, body.velocity.x);
        }

        Vector2 direction = new Vector2(xInput, yInput).normalized;
        body.velocity = direction * speed;
    }
}
