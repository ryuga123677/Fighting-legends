using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyattack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float range = 3f;
    [SerializeField] private float timebetweenattack = 1f;
    private Animator anim;
    private GameObject player;
    private bool playerinrange;
    private BoxCollider[] weaponcollider;
    private enemyhealth enemhelt;
    
    void Start()
    {
        weaponcollider = GetComponentsInChildren<BoxCollider>();
        player = gamemanager.instance.Player;
        anim= GetComponent<Animator>();
        enemhelt=GetComponent<enemyhealth>();
        StartCoroutine(attack());
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,player.transform.position)<range && enemhelt.Isalive)
            {
            playerinrange = true;
        }
        else
        {
            playerinrange= false;
        }
        
    }
    IEnumerator attack()
    {
        if(playerinrange && !gamemanager.instance.Gameover)
        {
            anim.Play("attack");
            yield return new WaitForSeconds(timebetweenattack);
        }
        yield return null;
        StartCoroutine(attack());
    }
    public void attackstart()
    {
        foreach(var weapon in weaponcollider)
        {
            weapon.enabled = true;
        }
    }
    public void attackend()
    {
        foreach (var weapon in weaponcollider)
        {
            weapon.enabled = false;
        }
    }
}
