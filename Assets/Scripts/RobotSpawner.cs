using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject startPositionBelt;
    [SerializeField]
    GameObject checkPoint;
    [SerializeField]
    GameObject endPositionBelt;

    [SerializeField]
    RoboInfo robotPrefab;
    [SerializeField]
    ClawMovement clawPrefab;

    public bool partOfBackground = false;


    public float timePassed;
    public float spawningTime;
    public int CurrentDifficulty=1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DifficultyOverTime();
        spawningTime += Time.deltaTime;
        if (spawningTime / 5 >= 1)
        {
            spawningTime = 0;
            ClawMovement claw = Instantiate(clawPrefab, startPositionBelt.transform.position, Quaternion.identity);
            CreateBot(claw);
        }
    }

    void CreateBot(ClawMovement parent)
    {
             
        RoboInfo robot = Instantiate(robotPrefab, startPositionBelt.transform.position, Quaternion.identity);
        robot.transform.parent = parent.spawningPoint.transform;
        robot.transform.localPosition = new Vector3(0f,0f,0f);
        parent.exitOnBelt = endPositionBelt.transform.position;
        parent.checkPortal = checkPoint.transform.position;

        if (partOfBackground)
        {
            parent.partOfBackGround = true;
        }
        else
        if (!partOfBackground)
        {
            parent.partOfBackGround = false;
            int missingParts = Random.Range(1, CurrentDifficulty + 1);
            List<RobotPiece> pieces = new List<RobotPiece>(robot.prefabPieces);
            for (int i = 0; i < missingParts; i++)
            {
                int random = Random.Range(0, pieces.Count);
                pieces[random].owned = false;
                pieces.Remove(pieces[random]);
            }
            
            parent.robot = robot;
            robot.UpdatePrefab();
        }
    }

    void DifficultyOverTime()
    {
        timePassed += Time.deltaTime;
        //if (timePassed / 60 >= 3)
        //{
        //    CurrentDifficulty = 4;
        //}
        if (timePassed / 60 >= 2)
        {
            CurrentDifficulty = 3;
        }
        else if (timePassed / 60 >= 1)
        {
            CurrentDifficulty = 2;
        }
    }
}
