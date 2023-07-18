using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    [SerializeField] float highDistance;
    [SerializeField] float lowDistance;

    GameObject player;

    Animator animator;

    NavMeshAgent agent;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        float distance = GetDistance();

        if (distance > highDistance)
        {
            animator.SetTrigger("Idle");
        }else if (distance > lowDistance && distance < highDistance)
        {
            agent.SetDestination(player.transform.position);
        }
        else if (distance < lowDistance)
        {
            animator.SetTrigger("Attack");
            animator.SetFloat("Speed", GetComponent<Rigidbody>().velocity.magnitude);
        }
    }

    float GetDistance()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }
}
