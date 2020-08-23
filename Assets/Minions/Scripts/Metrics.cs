using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Metrics 
{
    public const float or = 8f;
    public const float ir = or * 0.866f;

    public static Vector3[] corners =
    {
        new Vector3(0f, 0f, or),
        new Vector3(ir, 0f, 0.5f * or),
        new Vector3(ir, 0f, -0.5f * or),
        new Vector3(0f, 0f, -or),
        new Vector3(-ir, 0f, -0.5f * or),
        new Vector3(-ir, 0f, 0.5f * or),
        new Vector3(0f, 0f, or)
    };
}
