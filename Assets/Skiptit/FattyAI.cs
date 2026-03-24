using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FattyAI : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;
    private float Lerper;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && player.position.x > 263f)
        {
            if (Lerper < 7f) Lerper += Time.deltaTime;

            transform.position += new Vector3(Lerper * Time.deltaTime, 0f, 0f);
            animator.SetFloat("Walk", Lerper / 5f);
        }
    }
}
