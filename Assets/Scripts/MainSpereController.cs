using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpereController : MonoBehaviour
{
    public float rotationSpeed = 45.0f;
    private void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
