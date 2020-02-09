using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{

    static float speed = 1.2f;
    public bool onBelt = false;
    public AudioSource source;
    public PartsBelt belt;
    public GameObject explosion;
    public BoxCollider boxCollider;
    public Rigidbody body;

    List<GameObject> inactiveList = PartsBelt.inactiveList;

    void Start()
    {
        source = GameObject.Find("conveyor_belt").GetComponent<AudioSource>();
        boxCollider = gameObject.GetComponent<BoxCollider>();
        body = gameObject.GetComponent<Rigidbody>();
    }

    public void MakeKickable()
    {
        onBelt = false;
        boxCollider.isTrigger = false;
        body.isKinematic = false;
    }

    public void MakeNotKickable()
    {
        onBelt = true;
        boxCollider.isTrigger = true;
        body.isKinematic = true;
    }

    void Update()
    {
        if (transform.parent == null && boxCollider.isTrigger)
        {

            Vector3 oldPosition = gameObject.transform.position;
            float gravity = oldPosition.y;
            if (!onBelt)
            {
                gravity = oldPosition.y - Time.deltaTime;
            }

            /*if (oldPosition.y > 12.59f)
                gameObject.transform.position = new Vector3(oldPosition.x , oldPosition.y - Time.deltaTime * speed, oldPosition.z);
            else*/
            if (onBelt)
            {
                gameObject.transform.position = new Vector3(oldPosition.x += Time.deltaTime * speed / 2, gravity, oldPosition.z);
                if (gameObject.transform.position.x > -6.0f)
                {
                    inactiveList.Add(gameObject);
                    Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
                    gameObject.SetActive(false);
                }
            }
            else {
                gameObject.transform.position= new Vector3(oldPosition.x , gravity, oldPosition.z);
            }
        }
    }

    void PickUp()
    {
        inactiveList.Add(gameObject);
        gameObject.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Belt")
        {
            onBelt = true;
        }
        else if (other.gameObject.tag == "Ground")
        { 
        
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Belt" && transform.parent==null)
        {
            onBelt = false;
        }
        else if (other.gameObject.tag == "Ground")
        {

        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Belt")
    //    {
    //        onBelt = false;
    //    }
    //}
}
