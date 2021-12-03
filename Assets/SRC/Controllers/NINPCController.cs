using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class NINPCController : MonoBehaviour
{ 



    [SerializeField] private int FleeRange;
    [SerializeField] private int FOV;
    [SerializeField] private int health;
    [SerializeField] private float stunnedTime;
    [SerializeField] private float endurance;
    private UnityEngine.AI.NavMeshAgent agent;
    private bool flee = false;
    private bool canMove = true;
    private bool idle = true;
    private bool isHiding = false;
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
    }

    // Update is called once per frame
    void Update()
    {
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

        VisLoc();
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
        Debug.Log("Non-Infected is Fleeing");
        if (!flee)
        {
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
        clr();
        isHiding = true;
        goToPoint(TargetTransform.position);
        float atpTime = getAnticipationTime();
        Action rel = ()=>{
            isHiding = false;
        };
        StartCoroutine(Wait(atpTime, rel));
    }


    private float getAnticipationTime()
    {
        float[] times = new float[]{1f, 2f, 3f, 4f, 5f};
        int index = UnityEngine.Random.Range(0, times.Length + 1);
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
        Transform tInView = indexerUtil.InView(POV, FOV, 10);
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



    private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}
