using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof (AnimatorUtil))]
public class NINPCController : MonoBehaviour
{ 



    [SerializeField] private int FleeRange;
    [SerializeField] private int FOV;
    [SerializeField] private int health;
    [SerializeField] private float stunnedTime;
    [SerializeField] private float endurance;
    private AnimatorUtil animator;
    private UnityEngine.AI.NavMeshAgent agent;
    private bool flee = false;
    private bool canMove = true;
    private float h;
    private bool idle = true;
    private bool isHiding = false;
    private bool ishidingCooldown = false;
    private IndexerUtil indexerUtil;
    private NameModel names;
    private PathUtil pathUtil;
    private Transform POV;
    private Transform TargetTransform;
    private TagModel tags;



    // Start is called before the first frame update
    void Start()
    {
        names = new NameModel();
        pathUtil = new PathUtil();
        indexerUtil = new IndexerUtil();
        tags = new TagModel();
        POV = transform.Find(names.POV);
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<AnimatorUtil>();
        h = GetComponent<BoxCollider>().size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.velocity.magnitude > 0.15f)
        {
            idle = false;
        }
        else
        {
            idle = true;
        }



        if (canMove)
        {
            if (flee)
            {
                Flee();
            }
            else if (!isHiding && !Navigating())
            {
                Escape();
            }
        }

        if (isHiding)
        {
            if (!Navigating())
                animator.Hide();
        }

        VisLoc();
        Animate();
    }


    private void Animate()
    {
        if (flee)
        {
            animator.Run();
        }
        else if (!flee && !idle)
        {
            animator.Walk();
        }
        else if (idle)
        {
            animator.Idle();
        }
    }


    public void Chase()
    {
        Flee();
    }

    private void Escape()
    {
            Vector3 destination = GetTarget();
            goToPoint(destination);
    }


    private void Flee()
    {
        
        if (!flee)
        {
            isHiding = false;
            clr();
            Vector3 destination = GetTarget();
            goToPoint(destination);
            flee = true;
            Action rstFl = ()=>
            {
                flee = false;
            };
            StartCoroutine(Wait(endurance, rstFl));
        }
    }



    private Vector3 GetTarget()
    {
        Vector3 destination = pathUtil.GetDestination();
        return destination;
    }


    private void Hide()
    {
        if (flee || ishidingCooldown)
            return;
        clr();
        agent.isStopped = true;
        isHiding = true;
        goToPoint(TargetTransform.position);
        float atpTime = getAnticipationTime();
        Action rel = ()=>{
            agent.isStopped= false;
            isHiding = false;
            HidingCooldown();
        };
        StartCoroutine(Wait(atpTime, rel));
    }


    private void HidingCooldown()
    {
        ishidingCooldown = true;
        Action rel = ()=>
        {
            ishidingCooldown = false;
        };
        float atpTime = getAnticipationTime();
        StartCoroutine(Wait(atpTime, rel));
    }


    private float getAnticipationTime()
    {
        float[] times = new float[]{1f, 2f, 3f, 4f, 5f};
        int index = UnityEngine.Random.Range(0, times.Length );
        return times[index];
    }


    private void goToPoint(Vector3 point_destination)
    {
        agent.SetDestination(point_destination);
    }

    private void clr()
    {
        agent.isStopped = true;
        agent.ResetPath();
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


    private void VisLoc()
    {
        Transform tInView = indexerUtil.InView(POV, FOV, h);
        if (tInView == null)
            return;
        if (tInView.tag == tags.Env)
        {
             TargetTransform = tInView;
             Hide();
        }
        else if (tInView.tag == tags.NpcInfected)
        {
            Flee();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        string _tag = other.tag;
        if (_tag == tags.Env)
        {
            TargetTransform = other.transform;
            Hide();
        }
        else if (_tag == tags.NpcInfected)
        {
            Flee();
        }
    }



    private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}
