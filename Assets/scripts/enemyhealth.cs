using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyhealth : MonoBehaviour

{
    [SerializeField] private int startinghealth = 20;

    [SerializeField] private float timesincelasthit = 0.5f;

    [SerializeField] private float dissapearspeed = 2f;
    private AudioSource audio;
    private float timer = 0f;
    private Animator anim;
    private NavMeshAgent nav;
    private bool isalive;
    private int currenthealth;
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private bool dissapearenemy = false;
    private ParticleSystem blood;

    public bool Isalive
    {
        get { return isalive; }
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody= GetComponent<Rigidbody>();
        capsuleCollider= GetComponent<CapsuleCollider>();           
        nav= GetComponent<NavMeshAgent>();
        anim= GetComponent<Animator>();
        audio= GetComponent<AudioSource>();
        isalive= true;
        currenthealth = startinghealth;
        blood = GetComponentInChildren<ParticleSystem>();


    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(dissapearenemy)
        {
            transform.Translate(-Vector3.up * Time.deltaTime * dissapearspeed);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (timer >= timesincelasthit &&  !gamemanager.instance.Gameover)
        {
            if (other.tag == "playerweapon")
            {
                takehit();
                timer = 0f;
            }
}
    }
    void takehit()
    {
        if(currenthealth>0)
        {
            audio.PlayOneShot(audio.clip);
            anim.Play("hurt");
            currenthealth -= 10;
            blood.Play();
            if (currenthealth <=0)
            {
                isalive= false;
                killenemy();
            }
        }
    }
    void killenemy()
    {
        capsuleCollider.enabled= false;
        nav.enabled= false;
        anim.SetTrigger("enemydie");
        blood.Play();
        rigidbody.isKinematic= true;
        StartCoroutine(removeenemy());
    }
    IEnumerator removeenemy()
    {
        yield return new WaitForSeconds(2f);
        dissapearenemy = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
