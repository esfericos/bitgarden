using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    // public Vector3 startPoint;
    public Vector3[] path;

    private void Awake()
    {
        main = this;
    }
}
