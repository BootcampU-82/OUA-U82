using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParkurUITrigger : MonoBehaviour
{

    [SerializeField]
    private GameObject infoPanel;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            infoPanel.SetActive(true);
          
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            infoPanel.SetActive(false);
        }
    }
}
