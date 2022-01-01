using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(AnimatorUtil))]
public class NPCController : MonoBehaviour
{
    [SerializeField] private int ChaseRange;
    [SerializeField] private int FOV;
    [SerializeField] private int health;
    [SerializeField] private float stunnedTime;
    [SerializeField] private bool ignorePlayer;
    private float AttackRange;
    private AnimatorUtil animator;
    private Transform playerTransform;
    private UnityEngine.AI.NavMeshAgent agent;
    private bool chase = false;
    private bool canMove = true;
    private bool dead = false;
    private bool attack = false;
    private float h;
    private bool idle = true;
    private IndexerUtil indexerUtil;
    private NameModel names;
    private NINPCController NINPCC;
    private PathUtil pathUtil;
    private Transform POV;
    private bool sleep = false;
    private Transform TargetTransform;
    private TagModel tags;

    // Start is called before the first frame update
    void Start()
    {
        names = new NameModel();
        pathUtil = new PathUtil();
        indexerUtil = new IndexerUtil();
        tags = new TagModel();
        GameObject playerGameObject = GameObject.Find(names.Player);
        POV = transform.Find(names.POV);
        playerTransform = playerGameObject.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<AnimatorUtil>();
        h = GetComponent<BoxCollider>().size.y;
        AttackRange = (agent.stoppingDistance * agent.stoppingDistance)+2;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (!attack)
            {
                if(agent.velocity.magnitude > 0.15f)
                {
                    idle = false;
                }
                else
                {
                    idle = true;
                }
            }


            if (!dead)
            {
                PlayerFinder();
                nrTar();
                Sentry();
            }
            if (chase)
            {
                if (TargetTransform != null)
                {
                    goToPoint(TargetTransform.position);
                }
                else
                {
                    chase = false;
                }
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


        Animate();
    }


    private void Animate()
    {
        if (sleep)
        {
            animator.Sleep();
        }
        else if (dead)
        {
            animator.Burn();
        }
        else
            {
            if (chase && !attack)
            {
                animator.Run();
            }
            else if (!chase && !idle)
            {
                //TODO need walk animation for infected animator.Walk();
                animator.Run();
            }
            else if (idle)
            {
                animator.Idle();
            }
            else if (attack)
            {
                animator.Attack();
            }
        }
    }


    public void Burn()
    {
        Die();
    }


    public void Die()
    {
        dead = true;
        canMove = false;
        agent.isStopped = true;
        agent.enabled = false;
        chase = false;
        attack = false;
        idle = false;
        sleep = false;
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
        Transform tInView = indexerUtil.InView(POV, FOV, h);
        if (tInView == null)
            return;
        if (tInView.tag == tags.Player && !ignorePlayer)
        {
             chase = true;
             TargetTransform = tInView;
        }
        else if (tInView.tag == tags.NPCNonInfected)
        {
            chase = true;
            TargetTransform = tInView;
            NINPCC = TargetTransform.GetComponent<NINPCController>();
            NINPCC.Chase();
        }
    }



    public void Sleep()
    {
        if(!agent.isStopped)
            agent.isStopped = true;
        agent.enabled = false;
        canMove = false;
        chase = false;
        attack = false;
        idle = false;
        sleep = true;
        if (tag == tags.SCP)
        {
            PickUpUtil PU = GetComponent<PickUpUtil>();
            PU.enabled = true;
        }
    }


    public void SwitchIdle()
    {
        chase = false;
        attack = false;
        idle = true;
    }


    private void PlayerFinder()
    {
        if (ignorePlayer)
            return;
        float distance = (playerTransform.position-this.transform.position).sqrMagnitude;
        if (distance<ChaseRange*ChaseRange && distance > AttackRange) {
            if (!chase)
            {
                idle = false;
                chase = true;
                TargetTransform = playerTransform;
            }
        }
    }


    private void nrTar()
    {
        if (TargetTransform == null)
            return;
        float distance = (TargetTransform.position-this.transform.position).sqrMagnitude;
        if (distance<= AttackRange) {
            chase = false;
            if (!attack)
            {
                idle = false;
                attack = true;
            }
        }
        else
        {
            attack = false;
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
