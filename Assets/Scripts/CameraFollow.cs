using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    float middleCoord;
    Vector3 maxCameraCoord;
    Vector3 minCameraCoord;
    float time;
    Vector3 startCoord;
    Vector3 destinationCoord;
    bool isright;

    void Start()
    {
        middleCoord = 25.0f;
        maxCameraCoord = new Vector3(-22.859f, 18.619f, 24.0f);
        minCameraCoord = new Vector3(-22.859f, 18.619f, 23.0f);
        time = 0.0f;
        isright = true;
        startCoord = gameObject.transform.position;
        destinationCoord = gameObject.transform.position;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (player.transform.position.z > middleCoord && !isright)
        {
            isright = true;
            startCoord = gameObject.transform.position;
            destinationCoord = maxCameraCoord;
            time = 0.0f;
        }
        else if (player.transform.position.z < middleCoord && isright)
        {
            isright = false;
            startCoord = gameObject.transform.position;
            destinationCoord = minCameraCoord;
            time = 0.0f;
        }
        gameObject.transform.position = Vector3.Lerp(startCoord, destinationCoord, time);
    }
}
