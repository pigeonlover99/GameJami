using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = pos();
    }

    public Vector3 pos()
    {
        if (player != null) return new Vector3(player.position.x, player.position.y, -10);
        else return transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, pos(), 1f / 30f);
    }
}
