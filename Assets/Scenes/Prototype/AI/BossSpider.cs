using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class BossSpider : MonoBehaviour
{

    [SerializeField] float lowDistance;
    [SerializeField] float highDistance;

    [SerializeField] float fireSpeed;

    [SerializeField] GameObject poisonPref;
    [SerializeField] Transform spawnPoint;

    Animator anim;

    NavMeshAgent agent;

    GameObject player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = gameObject.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        
    }

    private void Update()
    {
        
        if (player != null)
        {
            transform.LookAt(player.transform);

            float distance = GetDistanceFromPlayer();
            if( distance > highDistance)
            {
                Debug.Log(0);
                Move();

            }else if( distance < highDistance && distance > lowDistance)
            {
                Debug.Log(1);
                RangeAttack();

            }else if(distance < lowDistance)
            {
                Debug.Log(2);
                BasicAttack();
            }
        }
    }

    private void Death()
    {
        anim.SetTrigger("Death");
    }

    private void TakeDamage()
    {
        anim.SetTrigger("TakeDamage");
    }

    private void BasicAttack()
    {
        anim.SetTrigger("SimpleAttack");
    }

    private void Move()
    {
        agent.SetDestination(player.transform.position);
        anim.SetFloat("Speed", GetComponent<Rigidbody>().velocity.magnitude);
    }

    private void RangeAttack()
    {
        agent.SetDestination(transform.position);
        GameObject projectile = ObjectPooling.Instance.GetInPool(0,spawnPoint.transform.position);
        projectile.transform.SetParent(null);
        projectile.transform.LookAt(player.transform.position);
        anim.SetTrigger("RangeAttack");
        projectile.GetComponent<Rigidbody>().velocity =  -1 * (transform.position - player.transform.position);
    }

    float GetDistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }
}

