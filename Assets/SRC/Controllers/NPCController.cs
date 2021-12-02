using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class NPCController : MonoBehaviour
{
    [SerializeField] private int ChaseRange;
    [SerializeField] private int AttackRange;
    [SerializeField] private int FOV;
    [SerializeField] private int health;
    [SerializeField] private float stunnedTime;
    private Transform playerTransform;
    private UnityEngine.AI.NavMeshAgent agent;
    private bool chase = false;
    private bool canMove = true;
    private bool attack = false;
    private bool idle = true;
    private IndexerUtil indexerUtil;
    private NameModel names;
    private PathUtil pathUtil;
    private Transform POV;
    private Transform TargetTransform;

    // Start is called before the first frame update
    void Start()
    {
        names = new NameModel();
        pathUtil = new PathUtil();
        indexerUtil = new IndexerUtil();
        GameObject playerGameObject = GameObject.Find(names.Player);
        POV = transform.Find(names.POV);
        playerTransform = playerGameObject.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            PlayerFinder();
            Sentry();
            if (chase)
            {
                goToPoint(TargetTransform.position);
            }
            if (attack)
            {
                //TODO attack
            }
            
            if (!chase && !attack && idle)
            {
                Idle();
            }
        }
    }


    private void Idle()
    {
        if (!Navigating())
        {
            goToPoint(pathUtil.GetDestination());
        }
    }


    private void Sentry()
    {
        Transform tInView = indexerUtil.InView(POV, FOV, 10);
        if (tInView == null)
            return;
        if (tInView.tag == "Player")
        {
             chase = true;
             TargetTransform = tInView;
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
                TargetTransform = playerTransform;
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
    }


    private bool Navigating()
    {
        bool navigating = true;

         if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    navigating = false;
                }
            }
        }

        return navigating;
    }


    private void goToPoint(Vector3 point_destination)
    {
        agent.SetDestination(point_destination);
    }




}
