using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderTrigger : MonoBehaviour
{

    [SerializeField] bool canAttack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameMenuManager.Instance.spiderCanAttack= canAttack;
        }
    }
}
