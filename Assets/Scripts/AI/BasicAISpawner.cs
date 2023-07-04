using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAISpawner : MonoBehaviour
{
    [SerializeField] GameObject basicAI;
    [SerializeField] float startTime = 1f;
    [SerializeField] float spawnRate = 1f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnAI),startTime,spawnRate);
    }

    void SpawnAI()
    {
        Instantiate(basicAI,gameObject.transform);
    }
}
