using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileController : MonoBehaviour
{


    public float m_Speed = 10f;
    public float m_Lifespan = 3f;
    private TagModel tags;
    private Rigidbody m_Rigidbody;

     void Awake()
     {
         m_Rigidbody = GetComponent<Rigidbody>();
         tags = new TagModel();
     }

     void Start()
     {
         m_Rigidbody.isKinematic = false;
         m_Rigidbody.AddForce(m_Rigidbody.transform.forward * m_Speed);
         Destroy(gameObject, m_Lifespan);
     }


    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        string wtag = collision.gameObject.tag;
        if (wtag == tags.NpcInfected || wtag == tags.SCP)
        {
            //EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            //enemyController.Hit();
            m_Rigidbody.isKinematic = true;
            //Destroy(gameObject, 1f);
            NPCController n11r = collision.gameObject.GetComponent<NPCController>();
            n11r.Sleep();
        }
    }
}
