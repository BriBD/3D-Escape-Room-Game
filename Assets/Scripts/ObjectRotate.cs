using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    public float rotationSpeed = 0.1f;

    void Update()
    {
        transform.Rotate(0.0f, rotationSpeed, 0.0f, Space.World);
    }
}


