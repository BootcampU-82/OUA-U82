using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipSwing : MonoBehaviour
{
    public float swingSpeed = 1.0f; // Sallanma h�z�
    public float swingAmount = 10.0f; // Sallanma miktar�

    private Quaternion startRotation;

    private void Start()
    {
        startRotation = transform.rotation;
    }

    private void Update()
    {
        // Z ekseninde sallanma efekti ekleyin, diledi�iniz eksende veya y�nde de�i�iklik yapabilirsiniz.
        float swing = Mathf.Sin(Time.time * swingSpeed) * swingAmount;
        transform.rotation = startRotation * Quaternion.Euler(0f, 0f, swing);
    }
}
