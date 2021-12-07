using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (Animator))]
public class AnimatorUtil : MonoBehaviour
{
    private Animator animator;
    private string[] HideArr = new string[]{"hide1","hide2"};
    // Start is called before the first frame update
    
    
    
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private string GetAnimationPlaying()
    {
        string animation = "";
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            animation = "attack";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            animation = "idle";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
            animation = "walk";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("run"))
            animation = "run";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("dead"))
            animation = "dead";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("sleep"))
            animation = "sleep";



        return animation;
    }


    public void Attack()
    {
        animator.SetTrigger("attack");
    }


    public void Dead()
    {
        animator.SetTrigger("dead");
    }


    public void Hide()
    {
        int index = Random.Range(0, HideArr.Length);
        string hideAnimation = HideArr[index];
        animator.SetTrigger(hideAnimation);
    }


    public void Idle()
    {
        animator.SetTrigger("idle");
    }


    public void Run()
    {
        animator.SetTrigger("run");
    }


    public void Sleep()
    {
        animator.SetTrigger("sleep");
    }


    public void Walk()
    {
        animator.SetTrigger("walk");
    }



}
