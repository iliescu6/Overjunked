using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    public RobotPiece robotPieceInTrigger;
    public RoboInfo robotInTrigger;

    public bool grounded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Robot")// || )
        {
            robotInTrigger = other.gameObject.GetComponent<RoboInfo>();
        }
        else if (other.gameObject.tag == "RobotPiece")
        {
            robotPieceInTrigger = other.gameObject.GetComponent<RobotPiece>();
            // robotPieceInTrigger.highlight.SetActive(true);
        }

        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Robot" || other.gameObject.tag == "RobotPiece")
        {
            robotInTrigger = null;
            //robotPieceInTrigger.highlight.SetActive(false);
            robotPieceInTrigger = null;          
        }

        if (other.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}
