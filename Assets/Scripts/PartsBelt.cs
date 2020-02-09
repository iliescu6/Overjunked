using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsBelt : MonoBehaviour
{
    List<Vector3> positions;

    float time;
    float spawnPeriod;
    public static List<GameObject> inactiveList;
    public RobotPiece[] parts;
    Vector3 spawnOffset;
    public AudioSource burn;

    void Start()
    {
        spawnOffset = new Vector3(-11.9f, 12.45f, 21.63f);
        time = 4.0f;
        spawnPeriod = 5.0f;
        //part.SetActive(false);
        inactiveList = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            inactiveList.Add(Instantiate(parts[i].gameObject));
            inactiveList[i].transform.localScale = new Vector3(.01f, .01f, .01f);
        }

        
        //for (int i = 0; i < 20; i++)
        //    inactiveList.Add(Instantiate(parts));

        positions = new List<Vector3>();
        positions.Add(spawnOffset + new Vector3(0.0f, 0.0f, 0.0f));
        positions.Add(spawnOffset + new Vector3(-1.2f, 0.0f, 0.0f));
        positions.Add(spawnOffset + new Vector3(-2.4f, 0.0f, 0.0f));
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time >= spawnPeriod )
        {
            time = 0.0f;
            int random = Random.Range(0, 3);

            GameObject inactivePart = Instantiate(parts[random].gameObject);
            inactivePart.transform.localScale = new Vector3(.01f, .01f, .01f);

            //inactiveList.RemoveAt(0);
            inactivePart.transform.position = positions[Random.Range(0, 3)];
            inactivePart.SetActive(true);
            Part p=inactivePart.GetComponent<Part>();

            

            p.onBelt = false;
        }
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, -time / 2));

    }
}
