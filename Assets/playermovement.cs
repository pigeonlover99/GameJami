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
        
        if(Mathf.Abs(xInput) != 0f) {
            body.velocity += new Vector2(xInput*speed, 0);
        }

        Vector2 direction = new Vector2(xInput, 0).normalized;
        body.velocity = direction * speed;
    }
}
