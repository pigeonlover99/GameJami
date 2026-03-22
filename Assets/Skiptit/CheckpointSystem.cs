using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    public Vector2 Pos;
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
