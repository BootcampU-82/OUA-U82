using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour
{

    NavMeshAgent agent;
    GameObject target;
    [SerializeField] float attactDistance;
    Animator _animator;
    bool canAttack = false;

    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = gameObject.GetComponent<NavMeshAgent>();
        _animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            agent.SetDestination(target.transform.position);
        }

        canAttack = CalculateDistanceWithPlayer(gameObject) < attactDistance ? true : false;
        _animator.SetBool("CanAttack", canAttack);

    }

    private float CalculateDistanceWithPlayer(GameObject gameObject)
    {
        return Vector3.Distance(gameObject.transform.position,target.transform.position);
    }

 
}
