using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpikeTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.DOMoveY(1.40f,0.5f);
            other.gameObject.GetComponent<PlayerHealth>().Die();
        }
    }
}
