using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMovement : MonoBehaviour
{
    public Vector3 exitOnBelt;
    public Vector3 checkPortal;
    public RoboInfo robot;
    public CharacterController characterController;
    public float speed=2.5f;
    public Transform spawningPoint;
    public bool destroy=true;
    public bool partOfBackGround = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = (exitOnBelt-gameObject.transform.position).normalized;
       characterController.Move(move * Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, checkPortal) < .25f)
        {
            if (destroy && !partOfBackGround)
            {
                destroy = false;
                robot.OnReachedFinish(this.gameObject);
                
            }
            else if (destroy && partOfBackGround)           
            {
                Destroy(gameObject);
            }
        }

        if (Vector3.Distance(transform.position, exitOnBelt) < 1)
        {
            if (!destroy && !partOfBackGround)
            {
                Destroy(gameObject);
            }
        }
    }
}
