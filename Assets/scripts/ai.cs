using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ai : MonoBehaviour
{
   [SerializeField] public Transform player;
    private Animator anim;
    private NavMeshAgent agent;
    private enemyhealth enemhelt;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim= GetComponent<Animator>();
        enemhelt = GetComponent<enemyhealth>();
        
    }

    // Update is called once per frame
    void Update()
    {   if(!gamemanager.instance.Gameover && enemhelt.Isalive)
        { agent.destination = player.position; }
        else if((!gamemanager.instance.Gameover || gamemanager.instance.Gameover) && !enemhelt.Isalive)
        {
            agent.enabled= false;
            anim.Play("idle");
            
        }
    else
        {
            agent.enabled = false;
            anim.Play("idle");
        }
       
    }
}
