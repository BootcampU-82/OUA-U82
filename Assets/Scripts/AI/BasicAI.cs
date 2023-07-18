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
    [SerializeField] GameObject bloodVfx;

    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = gameObject.GetComponent<NavMeshAgent>();
        _animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMenuManager.Instance.spiderCanAttack)
        {
            if (target)
            {
                agent.SetDestination(target.transform.position);
            }

            canAttack = CalculateDistanceWithPlayer(gameObject) < attactDistance ? true : false;
            _animator.SetBool("CanAttack", canAttack);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target.GetComponent<PlayerHealth>().TakeDamage();
        }
    }

    private float CalculateDistanceWithPlayer(GameObject gameObject)
    {
        return Vector3.Distance(gameObject.transform.position,target.transform.position);
    }

}
