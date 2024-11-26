using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPortal : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 10f; // Velocità di rotazione dell'oggetto

    void Update()
    {
        // Ruota l'oggetto lungo l'asse Z
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
