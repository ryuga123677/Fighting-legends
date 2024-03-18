using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float movespped = 10.0f;
    [SerializeField] private  LayerMask layerMask;
    private CharacterController characterController;
    private Vector3 currentlooktarget = Vector3.zero;
    private Animator anim;
    private BoxCollider[] swordcollider;
    void Start()
    {
        characterController= GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        swordcollider = GetComponentsInChildren<BoxCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!gamemanager.instance.Gameover)

        {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            characterController.SimpleMove(movespped * direction);
            if (direction == Vector3.zero)
            {
                anim.SetBool("iswalking", false);

            }
            else
            {
                anim.SetBool("iswalking", true);
            }
            if (Input.GetMouseButton(0))
            {
                anim.Play("doublechop");
            }
            if (Input.GetMouseButton(1))
            {
                anim.Play("spinattack");
            }
        }
    }
     void FixedUpdate()
    { if(!gamemanager.instance.Gameover)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 500, Color.blue);
            if (Physics.Raycast(ray, out hit, 500, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.point != currentlooktarget)
                {
                    currentlooktarget = hit.point;
                }
                Vector3 targetpos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                Quaternion rotation = Quaternion.LookRotation(targetpos - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 10 * Time.deltaTime);
            }
        }
    }
    public void beginattack()
    {
        foreach(var weapon in swordcollider)
        {
            weapon.enabled = true;
        }
    }
    public void endattack()
    {
        foreach (var weapon in swordcollider)
        {
            weapon.enabled = false;
        }
    }

}
