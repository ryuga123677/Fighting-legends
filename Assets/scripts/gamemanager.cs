using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    public static gamemanager instance=null;
    [SerializeField] GameObject player;
    private bool gameover = false;
    public bool Gameover
    {
        get { return gameover; }
    }
    public  GameObject Player
    {
        get { return player; }
    }

    private void Awake()
    {
        if(instance== null)
        {
            instance = this;

        }
        else if(instance!=this)
        {
          Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playerHP(int currenthp)
    {
        if(currenthp>0) {
            gameover = false;
        }
        else
        {
            gameover = true;
        }
    }
}
