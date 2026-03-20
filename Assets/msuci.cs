using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msuci : MonoBehaviour

{
    // Start is called before the first frame update
     [SerializeField] private AudioClip music;
    private AudioSource audiosource;
    void Start()
    {
            audiosource = GetComponent<AudioSource>();
            audiosource.clip = music;
            audiosource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
