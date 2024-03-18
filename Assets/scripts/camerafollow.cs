using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

public class camerafollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform target;
    [SerializeField] float smoothing=5f;
    Vector3 offset;
    private void Awake()
    {
        Assert.IsNotNull(target);
    }
    void Start()
    {
        offset = transform.position-target.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tarcam=target.position+offset;
        transform.position=Vector3.Lerp(transform.position,tarcam,smoothing*Time.deltaTime);
    }
}
