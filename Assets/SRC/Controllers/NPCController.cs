using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class NPCController : MonoBehaviour
{
    [SerializeField] private int ChaseRange;
    [SerializeField] private int AttackRange;
    [SerializeField] private int health;
    [SerializeField] private float stunnedTime;
    private Transform playerTransform;
    private UnityEngine.AI.NavMeshAgent agent;
    private bool chase = false;
    private bool canMove = true;
    private bool attack = false;
    private bool idle = true;
    private NameModel names;

    // Start is called before the first frame update
    void Start()
    {
        names = new NameModel();
        GameObject playerGameObject = GameObject.Find(names.Player);
        playerTransform = playerGameObject.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            PlayerFinder();

            if(chase)
            {
                goToPoint(playerTransform.position);
            }
            if (attack)
            {
                //TODO attack
            }
            
            if (!chase && !attack && idle)
            {
                //TODO idle
            }
        }
    }


    private void PlayerFinder()
    {
        float distance = (playerTransform.position-this.transform.position).sqrMagnitude;
        if (distance<ChaseRange*ChaseRange && distance > AttackRange*AttackRange) {
            if (!chase)
            {
                idle = false;
                chase = true;
            }
        }
        else if (distance<= (agent.stoppingDistance * agent.stoppingDistance)+1) {
            if (!attack)
            {
                idle = false;
                attack = true;
            }
            if (chase)
            {
                chase = false;
            }
        }
        else if (distance >= ChaseRange*ChaseRange)
        {
            idle = true;
            chase = false;
            attack = false;
        }
    }


    private void goToPoint(Vector3 point_destination)
    {
        agent.SetDestination(point_destination);
    }




}
