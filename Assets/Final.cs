using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{

    [SerializeField] GameObject finalMap;
    private void OnTriggerEnter(Collider other)
    {
        if (GameMenuManager.Instance.bossDead)
        {
            finalMap.SetActive(true);
        }
    }
}
