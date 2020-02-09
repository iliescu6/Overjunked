using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementResponsive : MonoBehaviour, IPlayerInput
{
    public TriggerDetection forwardTrigger;
    public TriggerDetection groundTrigger;

    public bool grounded;
    public RobotPiece[] playerPieces;
    public CharacterController characterController;
    public Transform[] carryingPositions;
    public GameObject rotatingModel;
    float inputX;
    float inputY;
    float mousex;
    public float speed;
    public LayerMask robotLayer;

    public AudioSource audioSource;

    RaycastHit hit;
    public RobotPiece leftItem;
    public RobotPiece rightItem;
    RobotPiece bodyItem;

    // All players currently in-game and enabled
    public static List<PlayerMovementResponsive> allPlayers = new List<PlayerMovementResponsive>();

    public static Action<PlayerMovementResponsive> PlayerAdded; // notify everyone that a player has been spawned
    public static Action<PlayerMovementResponsive> PlayerRemoved; // notifiy everyone that a player has been removed

    // Start is called before the first frame update
    void Start()
    {
        allPlayers.Add(this);
        PlayerAdded(this);
        for (int i = 0; i < 2; i++)
        {
            playerPieces[i] = new RobotPiece();
        }

        //Cursor.lockState = CursorLockMode.Locked;
    }
  

    private void OnDisable()
    {
        allPlayers.Remove(this);
        
            PlayerRemoved(this);
    }
    // Update is called once per frame
    void Update()
    {
        //inputX = Input.GetAxis("Vertical");
        //inputY = Input.GetAxis("Horizontal");
        if (inputY > 0)
        {
            this.transform.Rotate(new Vector3(0, 100 * Time.deltaTime, 0));

        }
        else if(inputY < 0)
        {
            this.transform.Rotate(new Vector3(0, -100 * Time.deltaTime, 0));

        }
        else
        {

        }


        //Movement
        Vector3 move;
        if (!groundTrigger.grounded)
        {
            float gravity = transform.position.y;
            move = Vector3.up * -gravity * Time.deltaTime * 25;
        }
        else
        {
            move = (this.transform.right * -inputX) * speed;
        }
        characterController.Move(move * Time.deltaTime);
       
        if (inputX != 0 || inputY != 0)
        {
            audioSource.enabled = true;
        }
        else
        {
            audioSource.enabled = false;
        }

        if (Input.GetKey(KeyCode.H))
        {
            DropItem();
        }

    }


    void DropItem()
    {
        if (playerPieces[0].owned == true)
        {
            playerPieces[0].owned = false;
            if (leftItem != null)
            {
                leftItem.gameObject.GetComponent<Part>().MakeKickable();
                leftItem.gameObject.transform.parent = null;
            }
            else if (rightItem != null)
            {
                rightItem.gameObject.GetComponent<Part>().MakeKickable();
                rightItem.gameObject.transform.parent = null; }
        }
        else if (playerPieces[1].owned == true)
        {
            playerPieces[1].owned = false;
            if (leftItem != null)
            {
                leftItem.gameObject.GetComponent<Part>().MakeKickable();
                leftItem.gameObject.transform.parent = null;
            }
            else if (rightItem != null)
            {
                rightItem.gameObject.GetComponent<Part>().MakeKickable();
                rightItem.gameObject.transform.parent = null;
            }
        }
    }

    void PickPiece(RobotPiece roboPiece)
    {
        if (playerPieces[0].owned == false && roboPiece.gameObject.transform.parent==null)
        {
            playerPieces[0].owned = true;
            playerPieces[0].piece = roboPiece.piece;
            leftItem= roboPiece;
            roboPiece.transform.parent = carryingPositions[1];
            if (carryingPositions[1].GetChild(0) != null)
            {
                carryingPositions[1].GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
                leftItem.gameObject.GetComponent<Part>().MakeNotKickable();
            }
            roboPiece.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if( playerPieces[1].owned == false && roboPiece.gameObject.transform.parent == null)
        {
            playerPieces[1].owned = true;
            playerPieces[1].piece = roboPiece.piece;
            rightItem= roboPiece; ;
            roboPiece.transform.parent = carryingPositions[2];
            roboPiece.transform.localPosition = new Vector3(0, 0, 0);
            if (carryingPositions[2].GetChild(0) != null)
            {
                carryingPositions[2].GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
                rightItem.gameObject.GetComponent<Part>().MakeNotKickable();
            }
        }
    }

    void PutPiece(RoboInfo robot)
    {
        if (robot.completedPieces != robot.allPieces.Count)
        {
            for (int i = 0; i < robot.allPieces.Count; i++)
            {
                if (robot.allPieces[i].owned == false)
                {
                    for (int j=0;j<playerPieces.Length;j++)
                    {
                        if (robot.allPieces[i].piece == playerPieces[j].piece && playerPieces[j].owned == true )
                        {
                            robot.allPieces[i].owned = true;
                            playerPieces[j].owned = false;
                            robot.UpdatePrefab();
                            if (leftItem!=null && leftItem.piece == playerPieces[j].piece)
                            { Destroy(leftItem.gameObject); } else if (rightItem != null &&rightItem.piece == playerPieces[j].piece)
                            { Destroy(rightItem.gameObject); } 
                            
                            

                            //if (leftItem != null)
                            //{ Destroy(leftItem); }
                            //else if (rightItem != null)
                            //{ Destroy(rightItem); }
                            //else if (bodyItem != null)
                            //{
                            //    Destroy(bodyItem);
                            //}

                            break;
                        }
                    }
                }
            }
        }
    }

    void Action()
    {
        //TODO MAKE SURE TAG EXISTS IN FINAL BUILD

        if (forwardTrigger.robotInTrigger != null)
        {
            if (forwardTrigger.robotInTrigger.gameObject.tag == "Robot")
            {
                PutPiece(forwardTrigger.robotInTrigger);
            }

        }
        else if (forwardTrigger.robotPieceInTrigger != null)
        {
            if (forwardTrigger.robotPieceInTrigger.gameObject.tag == "RobotPiece")
            {
                PickPiece(forwardTrigger.robotPieceInTrigger);
            }
        }
    }

    void DropAction()
    {
        DropItem();
    }

    void IPlayerInput.MoveDirection(float x, float z)
    {
        inputX = z;
        inputY = x;
    }
    void IPlayerInput.Use()
    {
        Action();
    }

    void IPlayerInput.Drop()
    {
        DropAction();
    }
}
