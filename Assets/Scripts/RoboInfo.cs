using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoboInfo : MonoBehaviour
{
    public float bonusEnergyOnFinish;

    public List<RobotPiece> allPieces = new List<RobotPiece>();
    public List<RobotPiece> prefabPieces = new List<RobotPiece>();

    public CharacterController characterController;
    GameplayManager timeManager;
    
    public int completedPieces=0;
    public float speed;
    public bool completed = false;

    //List<Piece>
    // Start is called before the first frame update
    void Start()
    {
        //TODO CHECK NAME IF CHANGED
        timeManager = GameObject.Find("Clock").GetComponent<GameplayManager>();
        characterController = gameObject.GetComponent<CharacterController>();

        for (int i = 0; i < prefabPieces.Count; i++)
        {
            allPieces.Add(prefabPieces[i]);
            allPieces[i].gameObject.SetActive(true);
        }

        UpdatePrefab();
    }

    public void OnReachedFinish(GameObject claw)
    {

        if (timeManager != null)
        {
            StartCoroutine(timeManager.RobotCheckUp(completed, claw));
        }
    }

    public void RepairedRobot()
    {
        EnergyManager energyManager=GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        EnergyManager manager = GameObject.Find("Energy Manager").GetComponent<EnergyManager>();
        manager.currentEnergy += bonusEnergyOnFinish;
    }

    public void UpdatePrefab()
    {
        if (completed)
        {
            return;
        }
        completedPieces = 0;
        for (int i = 0; i < prefabPieces.Count; i++)
        {
            for (int j = 0; j < allPieces.Count; j++)
            {
                if (allPieces[j].piece == prefabPieces[i].piece && allPieces[j].owned)
                {
                    prefabPieces[i].gameObject.SetActive(true);
                    completedPieces++;
                }
                else if(allPieces[j].piece == prefabPieces[i].piece && !allPieces[j].owned)
                {
                    prefabPieces[i].gameObject.SetActive(false);
                    completedPieces--;
                }
            }
        }

        if (completedPieces == prefabPieces.Count)
        {
            completed = true;
        }
        else { completedPieces = 0; }
    }

    }

