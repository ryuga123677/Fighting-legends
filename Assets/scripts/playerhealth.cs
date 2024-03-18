using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class playerhealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int startinghealth = 100;
    [SerializeField] float timesincelasthit = 2f;
    private float timer = 0f;
    private CharacterController characterController;
    private Animator anim;
    private int currenthealth;
    private AudioSource audio;
    private ParticleSystem blood;
    [SerializeField] Slider slider;
     void Awake()
    {
        Assert.IsNotNull(slider);
    }
    void Start()
    {
        anim= GetComponent<Animator>();
        characterController= GetComponent<CharacterController>();
        currenthealth = startinghealth;
        audio= GetComponent<AudioSource>();
        blood=GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(timer>=timesincelasthit && !gamemanager.instance.Gameover)
        {
            if(other.tag=="weapon")
            {
                takehit();
                timer = 0;
            }
        }
    }
    void takehit()
    {
        if(currenthealth>0)
        {
            gamemanager.instance.playerHP(currenthealth);
            anim.Play("hurt");
            currenthealth -= 10;
            slider.value= currenthealth;
            audio.PlayOneShot(audio.clip);
            blood.Play();
        }
        if(currenthealth<=0)
        {
            killplayer();
        }
    }
    void killplayer()
    {
        gamemanager.instance.playerHP(currenthealth);
        anim.SetTrigger("playerdie");
        audio.PlayOneShot(audio.clip);
        characterController.enabled= false;
        blood.Play();
    }
}
