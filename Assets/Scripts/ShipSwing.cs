using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipSwing : MonoBehaviour
{
    public float swingSpeed = 1.0f; // Sallanma hýzý
    public float swingAmount = 10.0f; // Sallanma miktarý

    private Quaternion startRotation;

    private void Start()
    {
        startRotation = transform.rotation;
    }

    private void Update()
    {
        // Z ekseninde sallanma efekti ekleyin, dilediðiniz eksende veya yönde deðiþiklik yapabilirsiniz.
        float swing = Mathf.Sin(Time.time * swingSpeed) * swingAmount;
        transform.rotation = startRotation * Quaternion.Euler(0f, 0f, swing);
    }
}
