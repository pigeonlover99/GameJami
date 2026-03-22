using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSetter : MonoBehaviour
{
    private CheckpointSystem check;

    void Start()
    {
        check = GameObject.FindGameObjectWithTag("Respawn").GetComponent<CheckpointSystem>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            check.Pos = transform.position;
            
            AudioSource ass = GetComponent<AudioSource>();
            ass.Play();
        }
    }
}
